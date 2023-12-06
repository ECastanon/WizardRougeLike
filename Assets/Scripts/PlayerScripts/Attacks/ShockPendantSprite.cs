using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShockPendantSprite : MonoBehaviour
{
    public int damage;
    public int damageMod = 0;

    public void Damage(Collider2D col)
    {
        Debug.Log("Attack has collided with the Enemy");
        Enemy enemy = col.GetComponent<Enemy>();
        enemy.TakeDamage(damage + damageMod);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            Damage(col);
        }
    }
}
