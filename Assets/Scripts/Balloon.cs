using UnityEngine;

public class Balloon : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Score.instance.AddScore();
            Destroy(gameObject);
        }
    }
}
