using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnOnDeath : MonoBehaviour
{
    public string objectToSpawn;
    public void DeathSpawn() //Spawns the typed object from the ObjectPooler
    {
        ObjectPooler.Instance.SpawnFromPool(objectToSpawn, transform.position, transform.rotation);
    }
}
