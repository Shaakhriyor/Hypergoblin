using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float forwardSpeed = 8f;
    public float steeringSpeed = 10f;
    public float xLimit = 4.5f; // Half-width of your platform track

    void Update()
    {
        // 1. Automatic constant forward motion
        transform.Translate(Vector3.forward * forwardSpeed * Time.deltaTime, Space.World);

        // 2. Controlled horizontal movement
        float hInput = Input.GetAxis("Horizontal");
        Vector3 currentPos = transform.position;
        currentPos.x += hInput * steeringSpeed * Time.deltaTime;

        // Clamp position so player stays on the platform
        currentPos.x = Mathf.Clamp(currentPos.x, -xLimit, xLimit);
        transform.position = currentPos;
    }
}