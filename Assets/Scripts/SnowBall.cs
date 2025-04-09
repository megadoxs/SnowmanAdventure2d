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

    public void SaveSnowBall(GameData gameData)
    {
        gameData.snowBallData.position = gameObject.transform.position;
        gameData.snowBallData.velocity = gameObject.GetComponent<Rigidbody2D>().linearVelocity;
        gameData.snowBallData.lifeTime = lifetime;
    }

    public void LoadSnowBall(ref GameData gameData)
    {
        gameObject.GetComponent<Rigidbody2D>().linearVelocity = gameData.snowBallData.velocity;
        gameObject.transform.position = gameData.snowBallData.position;
        lifetime = gameData.snowBallData.lifeTime;
    }
}
