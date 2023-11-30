using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Ladder : MonoBehaviour
{
    public bool playerIn = false;
    public bool useded = false;
    public GameObject player;
    public GameObject victoryPanel, infoPanel;
    public float distance;

    void Start()
    {
        victoryPanel = GameObject.Find("VictoryPanel");
        infoPanel = GameObject.Find("InfoPanel");
    }
    void Update()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        distance = Vector3.Distance(transform.position, player.transform.position);
        if (playerIn = true && Input.GetKeyDown("e") && distance < 3) //Without Distance this for some reason is being called by FountainHealing???
        {
            infoPanel.GetComponent<Animator>().Play("InfoPanelSlideOut", 0, 0);
            victoryPanel.GetComponent<Animator>().Play("GameOverSlideIn");
            useded = true;
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            if(useded == false){infoPanel.GetComponent<Animator>().Play("InfoSlideIn", 0, 0);}
            infoPanel.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "Press [E] to Escape!";
            playerIn = true;
        }
    }
    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            infoPanel.GetComponent<Animator>().Play("InfoPanelSlideOut", 0, 0);
            playerIn = false;
        }
    }
}
