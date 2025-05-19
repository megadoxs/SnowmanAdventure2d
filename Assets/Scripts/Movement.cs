using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Movement : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private float maxSpeed = 10f;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckRadius = 0.2f;
    
    [SerializeField] private AudioClip jumpSound;
    [SerializeField] private AudioClip landSound;
    [SerializeField] private AudioClip deathSound;

    private Rigidbody2D rb;
    private Animator animator;
    private AudioSource audioSource;
    private float horizontalInput;
    private bool isJumpPressed;
    private bool isGrounded;
    private ParticleSystem particleSystem;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        particleSystem = GetComponentInChildren<ParticleSystem>();
    }

    void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            isJumpPressed = true;
            audioSource.clip = jumpSound;
            audioSource.Play();
        }
    }

    void FixedUpdate()
    {

        if (!isGrounded && Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer))
        {
            particleSystem.Play();
            audioSource.clip = landSound;
            audioSource.Play();
        }
        
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        
        rb.linearVelocity = new Vector2(horizontalInput * moveSpeed, rb.linearVelocity.y);
        
        if (isJumpPressed)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            isJumpPressed = false;
            animator.SetTrigger("Jump");
        }
        
        if (Mathf.Abs(rb.linearVelocity.x) > maxSpeed)
        {
            rb.linearVelocity = new Vector2(Mathf.Sign(rb.linearVelocity.x) * maxSpeed, rb.linearVelocity.y);
        }
        animator.SetFloat("X", rb.linearVelocity.x);
    }

    public void Die()
    {
        audioSource.clip = deathSound;
        audioSource.Play();
    }
}
