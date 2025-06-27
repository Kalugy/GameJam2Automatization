using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform player;  // Assign the player object in the Inspector
    public Vector3 offset;

    void Start()
    {
        // Calculate and store the offset at the start
        offset = transform.position - player.position;
    }

    void LateUpdate()
    {
        // Keep the camera at the same offset from the player
        transform.position = player.position + offset;
    }
}
