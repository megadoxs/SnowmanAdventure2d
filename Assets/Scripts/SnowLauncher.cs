using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class SnowLauncher : MonoBehaviour
{
    public GameObject prefabToSpawn;
    public Transform player;
    public Transform spawnPoint;
    public float speed = 6.5f;
    public void Shot()
    {
        Rigidbody2D rb = Instantiate(prefabToSpawn, spawnPoint.position, Quaternion.identity).GetComponent<Rigidbody2D>();
        
        Vector2 directionToPlayer = (player.position - spawnPoint.position).normalized;
        
        float angleOffset = Random.Range(-15f, 15f);
        float baseAngle = Mathf.Atan2(directionToPlayer.y, directionToPlayer.x) * Mathf.Rad2Deg;
        float finalAngle = baseAngle + angleOffset;
        
        Vector2 shotDirection = new Vector2(Mathf.Cos(finalAngle * Mathf.Deg2Rad), Mathf.Sin(finalAngle * Mathf.Deg2Rad)).normalized;
        
        rb.linearVelocity = shotDirection * speed;
    }
}
