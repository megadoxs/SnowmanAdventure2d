using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class Balloon : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            gameObject.SetActive(false);
            Score.instance.AddScore();
        }
    }
}
