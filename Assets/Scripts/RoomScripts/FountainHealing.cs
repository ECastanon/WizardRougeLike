using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FountainHealing : MonoBehaviour
{
    private bool used = false, playerIn = false;
    private GameObject player;
    private GameObject infoPanel;
    private bool infoIn = false;

    void Start()
    {
        infoPanel = GameObject.Find("InfoPanel");
    }
    void Update()
    {
        if (playerIn = true && Input.GetKeyDown("e") && used == false)
        {
            player.GetComponent<Player>().hp = player.GetComponent<Player>().currentMaxHP;
            Debug.Log("Im healed!");
            player.GetComponent<Player>().UpdateHPBar();
            infoPanel.GetComponent<Animator>().Play("InfoSlideOut");
            infoIn = false;
            used = true;
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player" && used == false)
        {
            player = col.gameObject;
            Debug.Log("[E] to Heal");
            playerIn = true;
            infoPanel.GetComponent<Animator>().Play("InfoSlideIn");
            infoPanel.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "Press [E] to Heal!";
            infoIn = true;
        }
    }
    void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            playerIn = false;
            if(infoIn == true)
            {
                infoPanel.GetComponent<Animator>().Play("InfoSlideOut");
                infoIn = false;
            }
        }
    }
}
