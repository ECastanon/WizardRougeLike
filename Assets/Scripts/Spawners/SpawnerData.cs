using UnityEngine;

[CreateAssetMenu(fileName = "Spawner.asset", menuName = "Spawners/Spawner")]

public class SpawnerData : ScriptableObject
{
    public GameObject objectToSpawn;
    public int minSpawn;
    public int maxSpawn;
}
