using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody rb;
    private Vector2 moveDirection;
    private Vector3 movementVector;
    private Vector2 lookDirection;
    private float rotationX = 0f;

    public InputActionReference move;
    public InputActionReference look; // Add an InputActionReference for the mouse look
    public float moveSpeed;
    public float maxMoveSpeed;
    public float mouseSensitivity = 100f; // Sensitivity for mouse look
    public Transform playerCamera; // Reference to the camera object

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        // Lock the cursor to the center of the screen and make it invisible
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        MosuseLook();

        // Read movement input
        moveDirection = move.action.ReadValue<Vector2>();

        // Read mouse look input
        lookDirection = look.action.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        Movement();
    }
    private void Movement()
    {
        movementVector = new Vector3(moveDirection.x, 0, moveDirection.y);

        // Clamp movement speed
        moveSpeed = Mathf.Clamp(moveSpeed, 0, maxMoveSpeed);
        Vector3 moveForce = transform.TransformDirection(movementVector) * moveSpeed;
        rb.AddForce(moveForce, ForceMode.Force);
    }
    private void MosuseLook()
    {
        // Calculate rotation
        float mouseX = lookDirection.x * mouseSensitivity * Time.deltaTime;
        float mouseY = lookDirection.y * mouseSensitivity * Time.deltaTime;

        // Apply rotation to the player
        rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, -90f, 90f); // Limit vertical rotation

        playerCamera.localRotation = Quaternion.Euler(rotationX, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
    }
}
