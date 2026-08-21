using UnityEngine;

public class RunnerController : MonoBehaviour
{
    [Header("Movement Speeds")]
    public float forwardSpeed = 10f; // Automatic constant forward movement
    public float horizontalSpeed = 12f; // Player controlled left/right speed

    [Header("Track Limits")]
    public float trackWidthLimit = 4f; // Max distance allowed left/right from center

    void Update()
    {
        // 1. Automatic constant forward movement (Player cannot control this)
        transform.Translate(Vector3.forward * forwardSpeed * Time.deltaTime, Space.World);

        // 2. Read side-to-side input only (A/D, Left/Right arrows)
        float sideInput = Input.GetAxis("Horizontal");

        // Calculate target side movement
        Vector3 position = transform.position;
        position.x += sideInput * horizontalSpeed * Time.deltaTime;

        // Clamp side movement so player cannot walk off track edge
        position.x = Mathf.Clamp(position.x, -trackWidthLimit, trackWidthLimit);

        // Apply constrained position
        transform.position = position;
    }
}