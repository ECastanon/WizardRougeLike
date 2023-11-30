using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FountainHealing : MonoBehaviour
{
    public bool used = false;
    public bool playerIn = false;
    public GameObject player;
    public GameObject infoPanel;
    public bool infoIn = false;

    void Start()
    {
        infoPanel = GameObject.Find("InfoPanel");
    }
    void Update()
    {
        if (playerIn = true && Input.GetKeyDown("e") && used == false)
        {
            player = GameObject.FindGameObjectWithTag("Player");
            player.GetComponent<Player>().hp = player.GetComponent<Player>().currentMaxHP;
            Debug.Log("Im healed!");
            player.GetComponent<Player>().UpdateHPBar();
            infoPanel.GetComponent<Animator>().Play("InfoPanelSlideOut", 0, 0);
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
            infoPanel.GetComponent<Animator>().Play("InfoSlideIn", 0, 0);
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
                infoPanel.GetComponent<Animator>().Play("InfoPanelSlideOut", 0, 0);
                infoIn = false;
            }
        }
    }
}
