using UnityEngine;

public class Playerkill : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(other.gameObject);
            Time.timeScale = 0;
        }
    }
}
