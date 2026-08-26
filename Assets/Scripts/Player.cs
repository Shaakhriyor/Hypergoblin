using UnityEngine;
using UnityEngine.InputSystem; // This line fixes your error!

public class PlayerMovement : MonoBehaviour
{
    [Header("Speed Settings")]
    public float forwardSpeed = 5f;
    public float sideSpeed = 7f;

    [Header("Platform Limits")]
    public float limitX = 4f;

    void Start()
    {
        // Forces the player to start exactly in the middle
        transform.position = new Vector3(0, transform.position.y, transform.position.z);
    }

    void Update()
    {
        // 1. Automatic forward movement
        transform.Translate(Vector3.forward * forwardSpeed * Time.deltaTime, Space.World);

        // 2. Left and Right steering (New Input System)
        float horizontalInput = 0f;

        if (Keyboard.current != null)
        {
            if (Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed)
            {
                horizontalInput = 1f;
            }
            else if (Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed)
            {
                horizontalInput = -1f;
            }
        }

        Vector3 newPosition = transform.position;
        newPosition.x += horizontalInput * sideSpeed * Time.deltaTime;

        // 3. Keep the player on the platform
        newPosition.x = Mathf.Clamp(newPosition.x, -limitX, limitX);

        transform.position = newPosition;
    }
}