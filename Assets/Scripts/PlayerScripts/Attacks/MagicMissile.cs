using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicMissile : MonoBehaviour, IPooledObject
{

    public float spd;
    Vector2 moveDirection;
    Rigidbody2D rb;
    private GameObject player;

    public int timeAlive;
    public float timer; //Time until the bullet is deactivated

    public int damage;
    public int damageMod = 0;
    public int soMightDamage = 0;

    Vector3 v3 = new Vector3(.75f, .75f, 1.0f);

    public void OnObjectSpawn()
    {
        rb = GetComponent<Rigidbody2D>();
        ResetTimer();
        FaceMouse();

        player = GameObject.FindGameObjectWithTag("Player");
        damageMod = player.GetComponent<Player>().damageMod;
        soMightDamage = player.GetComponent<Player>().soMightDamage;

        GetSize();
    }

    void Update()
    {
        rb.velocity = transform.right * spd;
        Timer();
    }
    public void ResetTimer()
    {
        timer = 0;
    }
    public void FaceMouse()
    {
        Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = rotation;
    }
    public void Timer()
    {
        timer += Time.deltaTime;
        if (timer >= timeAlive) gameObject.SetActive(false); //Disables bullets to be reused
    }
    private void GetSize()
    {
        int gsCount = player.GetComponent<Player>().GrowthSerum;
        float newScale = 1 + (0.1f * gsCount);
        transform.localScale = Vector3.Scale(v3, new Vector3(newScale, newScale, 1f));
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            Enemy enemy = col.GetComponent<Enemy>();

            int newDamage = (damage + damageMod + soMightDamage);
            if (player.GetComponent<StaffAttacks>().ManaCircle)
            {
                enemy.TakeDamage(player.GetComponent<Player>().mcBoost * newDamage);
            }
            else
            {
                enemy.TakeDamage(newDamage);
            }
        }
    }
}
