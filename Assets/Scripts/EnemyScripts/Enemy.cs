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

    void Start()
    {
        health = startHealth;
        enemyManager = GameObject.FindGameObjectWithTag("EnemyManager");
        enemyManager.GetComponent<EnemyCounter>().enemyCount++;
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
    void Die()
    {
        //GameObject deathIns = (GameObject)Instantiate(enemyDeathEffect, transform.position, transform.rotation);
        //Destroy(deathIns, 1.5f);
        enemyManager.GetComponent<EnemyCounter>().enemyCount--;
        enemyManager.GetComponent<EnemyCounter>().OpenDoors();
        gameObject.SetActive(false);
    }
}
