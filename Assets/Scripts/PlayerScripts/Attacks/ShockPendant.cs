using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShockPendant : MonoBehaviour
{
    private GameObject enemyManager;
    public GameObject sprite;
    public GameObject enemyTarget;
    public GameObject player;
    public int rechargeTime;
    public float timer;
    public float range;
    public int damage;
    public int damageMod = 0;
    public AudioSource ShockSFX;

    void Start()
    {
        enemyManager = GameObject.FindGameObjectWithTag("EnemyManager");
        ShockSFX = GameObject.Find("ShockSFX").GetComponent<AudioSource>();
        sprite.SetActive(false);
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= rechargeTime)
        {
            //Find an enemy to strike
            GetRandomEnemyInRange();
            timer = 0;
        }

        if (enemyTarget)
        {
            FaceEnemy();
            StartCoroutine(DisableAfterHit());
        }
    }

    //Gets all enemies within range and adds them to a list
    //Then selects one of those enemies randomly to target
    public void GetRandomEnemyInRange()
    {
        float distance;
        List<GameObject> enemiesInRange = new List<GameObject>();
        for (int i = 0; i < enemyManager.GetComponent<EnemyCounter>().Enemies.Count; i++)
        {
            distance = Vector2.Distance(transform.position, enemyManager.GetComponent<EnemyCounter>().Enemies[i].transform.position);
            if (distance <= range)
            {
                enemiesInRange.Add(enemyManager.GetComponent<EnemyCounter>().Enemies[i]);
            }
        }
        if(enemiesInRange.Count > 0)
        {
            int rand = Random.Range(0, enemiesInRange.Count);
            enemyTarget = enemiesInRange[rand];
        }
    }

    public void FaceEnemy()
    {
        //Enables the lightning immediately before finding a new target
        sprite.SetActive(true);
        ShockSFX.Play();
        //Set Damage Values
        sprite.GetComponent<ShockPendantSprite>().damage = damage;
        sprite.GetComponent<ShockPendantSprite>().damageMod = damageMod;

        //If player is facing right
        if (player.GetComponent<PlayerMovement>().flip == false)
        {
            Vector2 direction = enemyTarget.transform.position - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = rotation;
        }
        //If player is facing left
        if (player.GetComponent<PlayerMovement>().flip == true)
        {
            Vector2 direction = transform.position - enemyTarget.transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Quaternion rotation = (Quaternion.AngleAxis(angle, Vector3.forward));
            transform.rotation = rotation;
        }
    }

    //Disables the Lightning shortly after attacking
    IEnumerator DisableAfterHit()
    {
        yield return new WaitForSeconds(.3f);
        enemyTarget = null;
        sprite.SetActive(false);
        ShockSFX.Stop();
    }
}
