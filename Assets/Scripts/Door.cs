using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    private GameObject enemyManager;

    void Start()
    {
        enemyManager = GameObject.FindGameObjectWithTag("EnemyManager");
        enemyManager.GetComponent<EnemyCounter>().Doors.Add(this.gameObject);
    }

    public void RemoveDoor()
    {
        gameObject.SetActive(false); //Disables objects to be reused
    }
}
