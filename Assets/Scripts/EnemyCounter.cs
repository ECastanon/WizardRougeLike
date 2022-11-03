using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCounter : MonoBehaviour
{
    private GameObject player;

    public List<GameObject> Doors = new List<GameObject>();

    public int enemyCount;

    void OpenDoors()
    {
        if(enemyCount <= 0)
        {
            //Get Doors
            for (int i = 0; i < Doors.Count; i++)
            {
                Doors[i].SetActive(false);
            }
            //Open Doors
            enemyCount = 0;
        }
    }

    void Update()
    {
        OpenDoors();
    }
}
