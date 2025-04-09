using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class SnowLauncher : MonoBehaviour
{
    public GameObject prefabToSpawn;
    public Transform spawnPoint;
    public float spawnInterval = 20f;
    public float spawnDelay = 0;
    public float speed = 6.5f;

    public float cooldown;
    private void Awake()
    {
        cooldown = spawnDelay;
    }

    private void Update()
    {
        if (cooldown > 0)
            cooldown = Math.Max(cooldown - Time.deltaTime, 0);
        else
        {
            cooldown = spawnInterval;
            SpawnObject();
        }
    }

    private void SpawnObject()
    {
        Rigidbody2D rb = Instantiate(prefabToSpawn, spawnPoint.position, Quaternion.identity).GetComponent<Rigidbody2D>();
        float angle = Random.Range(180f, 270f);
        rb.linearVelocity = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad)).normalized * speed;
    }
}
