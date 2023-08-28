using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Enemy : MonoBehaviour
{
    private float hpFromSoulJars = 0;

    [Header("Enemy Data")]
    public float startHealth;
    public float health;
    public int experience;
    private Color oldColor;

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

        oldColor = gameObject.GetComponent<Renderer>().material.color;
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
            gameObject.GetComponent<Renderer>().material.color = oldColor;
            yield return new WaitForSeconds(flashLength - flashspeed);
            gameObject.GetComponent<Renderer>().material.color = new Color(0.5f, 0.5f, 0.5f, 1f);
            yield return new WaitForSeconds(flashLength - flashspeed);
            flashspeed += Time.deltaTime;
        }
        gameObject.GetComponent<Renderer>().material.color = oldColor;
    }

    void RewardEXP()
    {
        //ADDS 5% ADDITIONAL EXP FOR EACH MONOCLE LEVEL
        gameManager.GetComponent<GameManager>().earnedEXP += (int)(experience + (experience * (gameManager.GetComponent<RelicEffects>().MonocleLvl * .05f)));
    }
    void VampireTooth()
    {
        if (gameManager.GetComponent<RelicEffects>().vToothLvl > 0)
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
        if(gameManager.GetComponent<RelicEffects>().oldSJLevel != gameManager.GetComponent<RelicEffects>().sJarLvl){LevelSoulJar();}

        if (gameManager.GetComponent<RelicEffects>().sJarLvl > 0)
        {
            gameManager.GetComponent<GameManager>().EnemyKillsforSoulJar++;
            if (gameManager.GetComponent<GameManager>().EnemyKillsforSoulJar % 5 == 0)
            {
                gameManager.GetComponent<GameManager>().soulJarStacks++;
                player.GetComponent<Player>().currentMaxHP = player.GetComponent<Player>().MaxHp + hpFromSoulJars + (gameManager.GetComponent<GameManager>().soulJarStacks * gameManager.GetComponent<RelicEffects>().sJarLvl);
                player.GetComponent<Player>().hp = player.GetComponent<Player>().hp + gameManager.GetComponent<RelicEffects>().sJarLvl;
                player.GetComponent<Player>().UpdateHPBar();
            }
        }
    }
    void LevelSoulJar()
    {
        gameManager.GetComponent<GameManager>().EnemyKillsforSoulJar = gameManager.GetComponent<GameManager>().EnemyKillsforSoulJar % 5;
        hpFromSoulJars = player.GetComponent<Player>().currentMaxHP - player.GetComponent<Player>().MaxHp;
        gameManager.GetComponent<GameManager>().soulJarStacks = 0;
    }

    void Die()
    {
        gameObject.GetComponent<Renderer>().material.color = oldColor;
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
