using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaCircle : MonoBehaviour
{
    public bool enabled = false;
    public float opacity = 0f;
    [HideInInspector] public GameObject player;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
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
            if(player != null){player.GetComponent<StaffAttacks>().ManaCircle = false;}
        }
    }

    public void Enable()
    {
        enabled = true;
        opacity = 1f;
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (player != null && col.gameObject.tag == "Player" && enabled)
        {
            player.GetComponent<StaffAttacks>().ManaCircle = true;
        }
    }
    void OnTriggerExit2D(Collider2D col)
    {
        if (player != null && col.gameObject.tag == "Player")
        {
            player.GetComponent<StaffAttacks>().ManaCircle = false;
        }
    }
}
