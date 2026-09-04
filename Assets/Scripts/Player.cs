using UnityEngine;
using UnityEngine.InputSystem; 

public class PlayerMovement : MonoBehaviour
{
    [Header("Speed Settings")]
    public float forwardSpeed = 5f;
    public float sideSpeed = 7f;

    [Header("Platform Limits")]
    public float limitX = 4f;

    void Start()
    {
        
        transform.position = new Vector3(0, transform.position.y, transform.position.z);
    }

    void Update()
    {
        
        transform.Translate(Vector3.forward * forwardSpeed * Time.deltaTime, Space.World);

        
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

        
        newPosition.x = Mathf.Clamp(newPosition.x, -limitX, limitX);

        transform.position = newPosition;
    }
}