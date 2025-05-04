using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField] private Transform player;  // Reference to the player
    [SerializeField] private Vector3 offset;    // The relative offset from the player (in world space)
    [SerializeField] private float smoothSpeed = 2f;

    private Camera mainCamera;  // The camera that renders the scene

    void Start()
    {
        mainCamera = Camera.main; // Get the main camera
    }

    void Update()
    {
        if (player == null)
            return;

        // Get the player's screen position
        Vector3 screenPosition = mainCamera.WorldToScreenPoint(player.position);

        // Apply the desired offset in screen space (this could be adjusted to change where the object appears)
        Vector3 targetScreenPosition = screenPosition + new Vector3(offset.x, offset.y, 0);

        // Convert back to world position
        Vector3 targetWorldPosition = mainCamera.ScreenToWorldPoint(targetScreenPosition);

        // Use lerp to smooth the movement to the target position
        Vector3 smoothPosition = Vector3.Lerp(transform.position, targetWorldPosition, smoothSpeed * Time.deltaTime);

        // Set the game object position with the z component set to the player's z (to keep it on the same depth)
        transform.position = new Vector3(smoothPosition.x, smoothPosition.y, 0);
    }
}
