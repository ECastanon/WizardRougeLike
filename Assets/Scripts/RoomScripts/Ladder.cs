using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Ladder : MonoBehaviour
{
    private bool playerIn = false, used = false;
    private GameObject player;
    private GameObject victoryPanel, infoPanel;

    void Start()
    {
        victoryPanel = GameObject.Find("VictoryPanel");
        infoPanel = GameObject.Find("InfoPanel");
    }
    void Update()
    {
        if (playerIn = true && Input.GetKeyDown("e"))
        {
            infoPanel.GetComponent<Animator>().Play("InfoSlideOut");
            victoryPanel.GetComponent<Animator>().Play("GameOverSlideIn");
            used = true;
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            if(used == false){infoPanel.GetComponent<Animator>().Play("InfoSlideIn");}
            infoPanel.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "Press [E] to Escape!";
            playerIn = true;
        }
    }
    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            infoPanel.GetComponent<Animator>().Play("InfoSlideOut");
            playerIn = false;
        }
    }
}
