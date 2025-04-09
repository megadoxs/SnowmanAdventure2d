using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float rotationSpeed = 100.0f;
    [SerializeField] private float maxSpeed = 10f;
    private Rigidbody2D rb;
    private Vector2 movementInput;
    private float rotationInput;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        movementInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        rotationInput = Input.GetKey(KeyCode.Space) ? 1f : 0f;
    }

    void FixedUpdate()
    {
        Vector2 desiredVelocity = movementInput.normalized * speed;
        Vector2 velocityChange = desiredVelocity - rb.linearVelocity;
        velocityChange = Vector2.ClampMagnitude(velocityChange, 20 * Time.fixedDeltaTime);
        rb.AddForce(velocityChange, ForceMode2D.Impulse);

        if (rb.linearVelocity.magnitude > maxSpeed)
            rb.linearVelocity = rb.linearVelocity.normalized * maxSpeed;

        if (rotationInput > 0)
            transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
    }

    public void SavePlayer(GameData data)
    {
        data.playerData.position = transform.position;
        data.playerData.rotation = transform.rotation;
        data.playerData.velocity = rb.linearVelocity;
    }

    public void LoadPlayer(ref GameData data)
    {
        transform.position = data.playerData.position;
        transform.rotation = data.playerData.rotation;
        rb.linearVelocity = data.playerData.velocity;
    }
}
