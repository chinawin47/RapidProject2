using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlObjectSwitcher : MonoBehaviour
{
    public List<GameObject> controllableObjects;  // List of objects to control
    private GameObject currentControlledObject;   // The object currently under control
    private int currentIndex = 0;                 // The index of the currently controlled object

    public float moveSpeed = 5f;                  // Speed at which the objects will move
    public float jumpForce = 5f;                  // Jump force if the object can jump
    public float jumpCooldown = 1f;               // Cooldown time for jumping

    private Rigidbody2D rb;                       // Reference to the Rigidbody2D of the currently controlled object
    private bool isGrounded = false;              // To check if the controlled object is grounded
    private bool onBlock = false;                 // To check if the controlled object is on a block
    private bool canJump = true;                  // Check if the object is allowed to jump
    private float jumpTimer = 0f;                 // Timer to track the cooldown

    public Camera mainCamera;                     // Reference to the main camera
    public Vector3 cameraOffset = new Vector3(0, 2, -10); // Offset for the camera from the controlled object

    void Start()
    {
        // Initialize the first controlled object
        if (controllableObjects.Count > 0)
        {
            currentControlledObject = controllableObjects[currentIndex];
            rb = currentControlledObject.GetComponent<Rigidbody2D>();

            // Set the camera to follow the first controlled object
            if (mainCamera == null)
                mainCamera = Camera.main;

            UpdateCameraPosition();
        }
    }

    void Update()
    {
        // Switch between controllable objects (using Q for example)
        if (Input.GetKeyDown(KeyCode.Q))
        {
            SwitchToNextObject();
        }

        // Handle movement of the currently controlled object
        HandleMovement();

        // Update the camera position to follow the current object
        UpdateCameraPosition();

        // Handle jump cooldown timer
        if (!canJump)
        {
            jumpTimer += Time.deltaTime;
            if (jumpTimer >= jumpCooldown)
            {
                canJump = true;
                jumpTimer = 0f;  // Reset timer
            }
        }
    }

    // Switch to the next object in the list
    private void SwitchToNextObject()
    {
        currentIndex = (currentIndex + 1) % controllableObjects.Count; // Cycle through the list
        SetCurrentControlledObject(currentIndex);
    }

    // Set the current controlled object
    private void SetCurrentControlledObject(int index)
    {
        currentControlledObject = controllableObjects[index];
        rb = currentControlledObject.GetComponent<Rigidbody2D>();

        // Immediately update the camera position when switching objects
        UpdateCameraPosition();
    }

    // Handle movement of the currently controlled object
    private void HandleMovement()
    {
        if (currentControlledObject == null)
            return;

        float h = Input.GetAxisRaw("Horizontal");

        // Move the object horizontally
        rb.velocity = new Vector2(h * moveSpeed, rb.velocity.y);

        // Jump when pressing space, object is grounded or on a block, and the jump cooldown allows it
        if (Input.GetKeyDown(KeyCode.Space) && (isGrounded || onBlock) && canJump)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            canJump = false;  // Disable jump until cooldown finishes
        }
    }

    // Update camera position to follow the currently controlled object
    private void UpdateCameraPosition()
    {
        if (mainCamera != null && currentControlledObject != null)
        {
            Vector3 targetPosition = currentControlledObject.transform.position + cameraOffset;
            mainCamera.transform.position = targetPosition;
        }
    }

    // Check for ground detection for jumping
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }

        // Check if the player is standing on a block
        if (collision.gameObject.CompareTag("Block"))
        {
            onBlock = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }

        // Check if the player is no longer on a block
        if (collision.gameObject.CompareTag("Block"))
        {
            onBlock = false;
        }
    }
}
