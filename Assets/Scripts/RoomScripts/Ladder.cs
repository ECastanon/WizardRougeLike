using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Ladder : MonoBehaviour
{
    public bool playerIn = false;
    public GameObject player;
    public GameObject victoryPanel, infoPanel;
    private LevelGeneration lg;
    private GameObject gm;
    public float distance;

    void Start()
    {
        victoryPanel = GameObject.Find("VictoryPanel");
        infoPanel = GameObject.Find("InfoPanel");
        lg = GameObject.Find("DungeonGenerator").GetComponent<LevelGeneration>();
        gm = GameObject.Find("GameManager");
    }
    void Update()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        distance = Vector3.Distance(transform.position, player.transform.position);
        if (playerIn = true && Input.GetKeyDown("e") && distance < 3) //Without Distance this for some reason is being called by FountainHealing???
        {
            //Store GameManager Data
            gm.GetComponent<GameManager>().StoreData();
            //Store collected relics
            gm.GetComponent<RelicEffects>().StoreRelicData();
            GameObject.Find("RelicPanel").GetComponent<RelicIcons>().StoreData();

            //Loads next scene
            StaticData.sceneNumber += 1;
            SceneManager.LoadScene(StaticData.sceneNumber);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            infoPanel.GetComponent<Animator>().Play("InfoSlideIn", 0, 0);
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
