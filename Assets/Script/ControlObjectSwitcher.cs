using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlObjectSwitcher : MonoBehaviour
{
    public List<GameObject> controllableObjects;  // List of objects to control
    private GameObject currentControlledObject;   // The object currently under control
    private int currentIndex = 0;                 // The index of the currently controlled object

    public float moveSpeed = 5f;                   // Speed at which the objects will move
    public float jumpHeight = 2f;                  // Height of the jump
    public float jumpDuration = 0.5f;              // Duration of the jump

    public Camera mainCamera;                      // Reference to the main camera
    public Vector3 cameraOffset = new Vector3(0, 2, -10); // Offset for the camera from the controlled object

    private bool isJumping = false;                // To check if the object is currently jumping
    private float jumpStartY;                      // Y position where the jump starts
    private float jumpEndY;                        // Y position where the jump ends
    private float jumpTime;                        // Timer to track the jump progress

    [Header("Jump Cooldown")]
    public float jumpCooldown = 1f;                // Cooldown time between jumps
    private float jumpCooldownTimer = 0f;          // Timer to track cooldown

    void Start()
    {
        // Initialize the first controlled object
        if (controllableObjects.Count > 0)
        {
            currentControlledObject = controllableObjects[currentIndex];

            // Set the camera to follow the first controlled object
            if (mainCamera == null)
                mainCamera = Camera.main;

            UpdateCameraPosition();
        }
    }

    void Update()
    {
        // Switch between controllable objects (using Q and E for example)
        if (Input.GetKeyDown(KeyCode.Q))
        {
            SwitchToPreviousObject();
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            SwitchToNextObject();
        }

        // Handle horizontal movement
        HandleMovement();

        // Decrease the jump cooldown timer
        if (jumpCooldownTimer > 0)
        {
            jumpCooldownTimer -= Time.deltaTime;
        }

        // Handle jumping if not currently jumping and cooldown is finished
        if (Input.GetKeyDown(KeyCode.Space) && !isJumping && jumpCooldownTimer <= 0f)
        {
            StartJump();
        }

        // If the object is jumping, handle the jump progression
        if (isJumping)
        {
            HandleJump();
        }

        // Update the camera position to follow the current object
        UpdateCameraPosition();
    }

    // Switch to the next object in the list
    private void SwitchToNextObject()
    {
        currentIndex = (currentIndex + 1) % controllableObjects.Count; // Cycle through the list
        SetCurrentControlledObject(currentIndex);
    }

    // Switch to the previous object in the list
    private void SwitchToPreviousObject()
    {
        currentIndex = (currentIndex - 1 + controllableObjects.Count) % controllableObjects.Count; // Cycle through the list backward
        SetCurrentControlledObject(currentIndex);
    }

    // Set the current controlled object
    private void SetCurrentControlledObject(int index)
    {
        currentControlledObject = controllableObjects[index];

        // Immediately update the camera position when switching objects
        UpdateCameraPosition();
    }

    // Handle movement of the currently controlled object
    private void HandleMovement()
    {
        if (currentControlledObject == null)
            return;

        float horizontalInput = Input.GetAxisRaw("Horizontal");
        Vector3 currentPosition = currentControlledObject.transform.position;

        // Move the object horizontally based on input
        currentPosition.x += horizontalInput * moveSpeed * Time.deltaTime;
        currentControlledObject.transform.position = currentPosition;
    }

    // Start the jump process
    private void StartJump()
    {
        isJumping = true;
        jumpStartY = currentControlledObject.transform.position.y;
        jumpEndY = jumpStartY + jumpHeight;
        jumpTime = 0f; // Reset the jump timer
        jumpCooldownTimer = jumpCooldown; // Reset the cooldown timer
    }

    // Handle the jump logic
    private void HandleJump()
    {
        jumpTime += Time.deltaTime;

        // Calculate the percentage of the jump completion
        float jumpProgress = jumpTime / jumpDuration;

        // If the jump is complete, stop jumping
        if (jumpProgress >= 1f)
        {
            jumpProgress = 1f;
            isJumping = false; // Reset jump state
        }

        // Calculate the new Y position using a simple interpolation
        float newY = Mathf.Lerp(jumpStartY, jumpEndY, jumpProgress);
        currentControlledObject.transform.position = new Vector3(currentControlledObject.transform.position.x, newY, currentControlledObject.transform.position.z);
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
}
