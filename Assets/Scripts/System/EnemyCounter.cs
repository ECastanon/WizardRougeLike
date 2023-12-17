using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCounter : MonoBehaviour
{
    public GameObject RelicMenu;

    public List<GameObject> Doors = new List<GameObject>();
    public List<GameObject> Enemies = new List<GameObject>();

    private GameObject gameManager;
    private GameObject objectPooler;
    private GameObject player;

    public void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager");
        objectPooler = GameObject.Find("ObjectPooler");
    }

    public void OpenDoors()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if(Enemies.Count <= 0)
        {
            //Disable all Projectiles
            foreach(Transform item in objectPooler.transform)
            {
                item.gameObject.SetActive(false);
            }
            //Get Doors
            for (int i = 0; i < Doors.Count; i++)
            {
                Doors[i].SetActive(false);
            }
            //Open Doors

            //Resets speed to fix all debuffs
            player.GetComponent<PlayerMovement>().ResetSpeed();

            //ONLY USED IF THE MEDAL OF VALOR IS ACTIVE
            if (gameManager.GetComponent<RelicEffects>().moValorLvl > 0)
            {
                if(player.GetComponent<Player>().hp + ((gameManager.GetComponent<RelicEffects>().moValorLvl * 2) - 1) < player.GetComponent<Player>().currentMaxHP)
                {
                    player.GetComponent<Player>().hp += (gameManager.GetComponent<RelicEffects>().moValorLvl * 2);
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
