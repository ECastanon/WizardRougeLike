using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FountainHealing : MonoBehaviour
{
    private bool used = false;
    private bool playerIn = false;

    private GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if (playerIn = true && Input.GetKeyDown("e") && used == false)
        {
            player.GetComponent<Player>().hp = player.GetComponent<Player>().MaxHp;
            Debug.Log("Im healed!");
            player.GetComponent<Player>().UpdateHPBar();
            used = true;
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            Debug.Log("[E] to Heal");
            playerIn = true;
        }
    }    void OnTriggerExit2D(Collider2D col)    {        if (col.CompareTag("Player"))        {
            //gm.InputText.text = (" ");
            playerIn = false;        }    }
}
