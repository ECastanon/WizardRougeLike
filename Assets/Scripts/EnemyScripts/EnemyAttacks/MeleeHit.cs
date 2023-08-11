using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeHit : MonoBehaviour
{
    public int damage;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            //Debug.Log("Attack has collided with the Player");
            Player player = col.GetComponent<Player>();
            player.TakeDamage(damage);
        }
    }
}
