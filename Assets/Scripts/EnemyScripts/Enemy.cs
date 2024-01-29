using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Enemy : MonoBehaviour
{
    public bool noDeathRelics = false;
    public List<SpriteRenderer> sr = new List<SpriteRenderer>();
    public List<Color> oldColor = new List<Color>();

    [Header("Enemy Data")]
    public float startHealth;
    public float health;
    public int experience;

    public GameObject enemyDeathEffect;

    [Header("Enemy Headers")]
    public Image healthbar;

    private GameObject enemyManager;
    private GameObject gameManager;
    private GameObject player;

    void Start()
    {
        health = startHealth;
        enemyManager = GameObject.FindGameObjectWithTag("EnemyManager");
        enemyManager.GetComponent<EnemyCounter>().Enemies.Add(this.gameObject);

        gameManager = GameObject.FindGameObjectWithTag("GameManager");
        player = GameObject.FindGameObjectWithTag("Player");
    }
    public void OnActive()
    {
        AddDescendantsWithSprite(transform, sr);
        oldColor.RemoveAt(0); oldColor.RemoveAt(0); oldColor.RemoveAt(0); oldColor.RemoveAt(0);
    }
    private void AddDescendantsWithSprite(Transform parent, List<SpriteRenderer> list)
    {
        foreach (Transform child in parent)
        {
            if (child.gameObject.GetComponent<SpriteRenderer>() != null && child.gameObject.name != "Shadow")
            {
                list.Add(child.gameObject.GetComponent<SpriteRenderer>());
                oldColor.Add(child.gameObject.GetComponent<SpriteRenderer>().color);
            }
            AddDescendantsWithSprite(child, list);
        }
    }
    public void TakeDamage(float amount)
    {
        health -= amount;
        //healthbar.fillAmount = health / startHealth;
        if (health <= 0)
        {
            Die();
        } else {
            StartCoroutine(FlashOnHit());
        }
    }
    private IEnumerator FlashOnHit()
    {
        float flashspeed = 0f;
        float flashLength = .02f;
        while(flashspeed <= flashLength)
        {
            for (int i = 0; i < sr.Count; i++)
            {
                sr[i].color = new Color(oldColor[i].r, oldColor[i].g, oldColor[i].b);
            }
            yield return new WaitForSeconds(flashLength - flashspeed);
            for (int i = 0; i < sr.Count; i++)
            {
                sr[i].color = new Color(0.5f, 0.5f, 0.5f);
            }
            yield return new WaitForSeconds(flashLength - flashspeed);
            flashspeed += Time.deltaTime;
        }
        for (int i = 0; i < sr.Count; i++)
        {
            sr[i].color = new Color(oldColor[i].r, oldColor[i].g, oldColor[i].b);
        }
    }
    void RewardEXP()
    {
        //ADDS 5% ADDITIONAL EXP FOR EACH MONOCLE LEVEL
        gameManager.GetComponent<GameManager>().earnedEXP += (int)(experience + (experience * (gameManager.GetComponent<RelicEffects>().MonocleLvl * .05f)));
    }
    void VampireTooth()
    {
        if (gameManager.GetComponent<RelicEffects>().vToothLvl > 0 && !noDeathRelics)
        {
            float rand = Random.Range(0f, 1f);
            if (rand < .30)
            {
                if (player.GetComponent<Player>().hp + gameManager.GetComponent<RelicEffects>().vToothLvl < player.GetComponent<Player>().currentMaxHP)
                {
                    player.GetComponent<Player>().hp += gameManager.GetComponent<RelicEffects>().vToothLvl;
                }
                else
                {
                    player.GetComponent<Player>().hp = player.GetComponent<Player>().currentMaxHP;
                }
            }
        }
    }
    void SoulJar()
    {
        //Check if SJ is active
        if (gameManager.GetComponent<RelicEffects>().sJarLvl > 0 && !noDeathRelics)
        {
            //Add a kill
            gameManager.GetComponent<GameManager>().EnemyKillsforSoulJar++;
            //Check if kill are divisible by 5
            if (gameManager.GetComponent<GameManager>().EnemyKillsforSoulJar % 5 == 0)
            {
                //Add to the earned HP by the SJ level
                gameManager.GetComponent<GameManager>().earnedSJHP += gameManager.GetComponent<RelicEffects>().sJarLvl;
                //Update current max HP
                player.GetComponent<Player>().currentMaxHP = player.GetComponent<Player>().MaxHp + gameManager.GetComponent<GameManager>().earnedSJHP;
                //Adds hp equal to the SJ level
                player.GetComponent<Player>().hp = player.GetComponent<Player>().hp + gameManager.GetComponent<RelicEffects>().sJarLvl;
                player.GetComponent<Player>().UpdateHPBar();
            }
        }
    }
    void Die()
    {
        //foreach (SpriteRenderer sprite in sr){sprite.gameObject.GetComponent<Renderer>().material.color = oldColor[sprite];}
        enemyManager.GetComponent<EnemyCounter>().Enemies.Remove(this.gameObject);
        enemyManager.GetComponent<EnemyCounter>().OpenDoors();

        //ONLY USED IF THE VAMPIRE TOOTH IS ACTIVE
        VampireTooth();
        //ONLY USED IF THE SOUL JAR IS ACTIVE
        SoulJar();

        RewardEXP();

        gameObject.SetActive(false);
    }
}
