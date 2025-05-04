using System;
using UnityEngine;

public class SnowBall : MonoBehaviour
{
    public float lifetime = 10f;

    void Update()
    {
        if(lifetime == 0)
            Destroy(gameObject);
        else
            lifetime = Math.Max(0, lifetime - Time.deltaTime);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.CompareTag("Player"))
        {
            Destroy(other.gameObject);
            Time.timeScale = 0;
        }
    }
}
