using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonBlast : MonoBehaviour
{
    public Vector2 playerLoc;
    public float speed;
    void Start()
    {
        playerLoc = GameObject.FindGameObjectWithTag("Player").transform.position;
    }
    void Update()
    {
        float distance = Vector2.Distance(transform.position, playerLoc);

        if(distance > .01f)
        {
            transform.position = Vector2.MoveTowards(transform.position, playerLoc, speed * Time.deltaTime);
        } else {
            //If SpawnOnDeath is attached, the object will spawn a new object when destroyed
            if(gameObject.GetComponent<SpawnOnDeath>())
            {
                gameObject.GetComponent<SpawnOnDeath>().DeathSpawn();
            }
            Destroy(gameObject);
        }
    }
}
