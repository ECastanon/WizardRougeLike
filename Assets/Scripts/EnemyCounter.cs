using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCounter : MonoBehaviour
{
    public GameObject RelicMenu;

    public List<GameObject> Doors = new List<GameObject>();
    public List<GameObject> Enemies = new List<GameObject>();

    private GameObject gameManager;
    private GameObject player;

    public void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager");
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public void OpenDoors()
    {
        if(Enemies.Count <= 0)
        {
            //Get Doors
            for (int i = 0; i < Doors.Count; i++)
            {
                Doors[i].SetActive(false);
            }
            //Open Doors

            //ONLY USED IF THE MEDAL OF VALOR IS ACTIVE
            if (gameManager.GetComponent<RelicEffects>().moValor > 0)
            {
                if(player.GetComponent<Player>().hp + (gameManager.GetComponent<GameManager>().healBy - 1) < player.GetComponent<Player>().currentMaxHP)
                {
                    player.GetComponent<Player>().hp += gameManager.GetComponent<GameManager>().healBy;
                } else
                {
                    player.GetComponent<Player>().hp = player.GetComponent<Player>().currentMaxHP;
                }
                player.GetComponent<Player>().UpdateHPBar();
            }
                RelicMenu.GetComponent<RelicPanel>().SlideInMenu();
        }
    }
}
