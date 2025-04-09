using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class Balloon : MonoBehaviour
{
    public BalloonPosition balloonPosition;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            SpawnBalloons.instance.SpawnBalloon(gameObject);
            gameObject.SetActive(false);
            Score.instance.AddScore();
        }
    }
}
