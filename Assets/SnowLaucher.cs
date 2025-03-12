using UnityEngine;

public class SnowLaucher : MonoBehaviour
{
    public GameObject prefabToSpawn;
    public Transform spawnPoint;
    public float spawnInterval = 20f;

    void Start()
    {
        InvokeRepeating(nameof(SpawnObject), 0f, spawnInterval); // Spawns every 10 seconds
    }

    void SpawnObject()
    {
        Instantiate(prefabToSpawn, spawnPoint.position, Quaternion.identity);
    }
}
