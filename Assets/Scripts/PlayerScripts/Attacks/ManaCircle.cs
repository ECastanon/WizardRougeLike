using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaCircle : MonoBehaviour
{
    public bool enabled = false;
    public float opacity = 0f;
    private GameObject player;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (opacity > 0)
        {
            GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, opacity);
            opacity = opacity - (Time.deltaTime / 5);
        } else
        {
            enabled = false;
        }
    }

    public void Enable()
    {
        enabled = true;
        opacity = 1f;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player" && enabled)
        {
            player.GetComponent<StaffAttacks>().ManaCircle = true;
        }
    }
    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player" && enabled)
        {
            player.GetComponent<StaffAttacks>().ManaCircle = false;
        }
    }
}
