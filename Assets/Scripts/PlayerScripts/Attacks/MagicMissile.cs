using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicMissile : MonoBehaviour, IPooledObject
{

    public float spd;
    Vector2 moveDirection;
    Rigidbody2D rb;

    public int timeAlive;
    public float timer; //Time until the bullet is deactivated

    public int damage;
    public int damageMod = 0;

    public void OnObjectSpawn()
    {
        rb = GetComponent<Rigidbody2D>();
        ResetTimer();
        FaceMouse();
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
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            //Debug.Log("Attack has collided with the Enemy");
            Enemy enemy = col.GetComponent<Enemy>();
            enemy.TakeDamage(damage + damageMod);
        }
    }
}
