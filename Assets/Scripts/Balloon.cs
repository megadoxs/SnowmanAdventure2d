using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public abstract class Balloon : MonoBehaviour
{
    [SerializeField] private AudioClip sound;
    
    private AudioSource audioSource;
    private Collider2D collider;
    private SpriteRenderer spriteRenderer;
    
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        collider = GetComponent<Collider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            OnBallonCollision();
            PlaySound();
            collider.enabled = false;
            spriteRenderer.enabled = false;
            StartCoroutine(Destroy());
        }
    }

    protected abstract void OnBallonCollision();

    private void PlaySound()
    {
        audioSource.clip = sound;
        audioSource.Play();
    }
    
    private IEnumerator Destroy()
    {
        yield return new WaitForSeconds(audioSource.clip.length);
        Destroy(gameObject);
    }
}