using UnityEngine;

public class Playerkill : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.gameObject.GetComponent<Movement>().Die();
            Time.timeScale = 0;
        }
    }
}
