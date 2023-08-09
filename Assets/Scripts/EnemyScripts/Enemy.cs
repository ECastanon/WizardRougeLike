using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
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

    public void TakeDamage(float amount)
    {
        health -= amount;
        //healthbar.fillAmount = health / startHealth;
        if (health <= 0)
        {
            Die();
        }
    }

    void RewardEXP()
    {
        //ADDS 5% ADDITIONAL EXP FOR EACH MONOCLE
        gameManager.GetComponent<GameManager>().earnedEXP += (int)(experience + (experience * (gameManager.GetComponent<RelicEffects>().Monocle * .05f)));
    }

    void Die()
    {
        enemyManager.GetComponent<EnemyCounter>().Enemies.Remove(this.gameObject);
        enemyManager.GetComponent<EnemyCounter>().OpenDoors();

        //ONLY USED IF THE VAMPIRE TOOTH IS ACTIVE
        if (gameManager.GetComponent<RelicEffects>().vTooth > 0)
        {
            float rand = Random.Range(0f, 1f);
            if (rand < .30)
            {
                if (player.GetComponent<Player>().hp + (gameManager.GetComponent<GameManager>().toothHeal) < player.GetComponent<Player>().currentMaxHP)
                {
                    player.GetComponent<Player>().hp += gameManager.GetComponent<GameManager>().toothHeal;
                }
                else
                {
                    player.GetComponent<Player>().hp = player.GetComponent<Player>().currentMaxHP;
                }
            }
        }

        //ONLY USED IF THE SOUL JAR IS ACTIVE
        if (gameManager.GetComponent<RelicEffects>().sJar > 0)
        {
            gameManager.GetComponent<GameManager>().EnemyKillsforSoulJar++;
            if (gameManager.GetComponent<GameManager>().EnemyKillsforSoulJar % 5 == 0)
            {
                gameManager.GetComponent<GameManager>().soulJarStacks++;
                player.GetComponent<Player>().currentMaxHP = player.GetComponent<Player>().MaxHp + gameManager.GetComponent<GameManager>().soulJarStacks;
                player.GetComponent<Player>().hp++;
                player.GetComponent<Player>().UpdateHPBar();
            }
        }

        RewardEXP();

        gameObject.SetActive(false);
    }
}
