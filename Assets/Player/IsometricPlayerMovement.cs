using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsometricPlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;  // Speed of movement
    [SerializeField] private float jumpForce = 7f;  // Force of jump
    [SerializeField] private LayerMask groundLayer; // Layer for ground detection

    private Rigidbody rb;
    private Vector3 forward, right;
    private bool isGrounded = false;

    // Awake is called when the script instance is being loaded
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        // Calculate directions based on the camera angle
        forward = Camera.main.transform.forward;
        right = Camera.main.transform.right;

        // Flatten the directions so movement is along the ground plane
        forward.y = 0f;
        right.y = 0f;

        forward = forward.normalized;
        right = right.normalized;
    }

    private void Update()
    {
        HandleMovement();

        // Handle jumping
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Jump();
        }
    }

    private void HandleMovement()
    {
        // Get input for movement along the x and z axes
        Vector3 direction = Vector3.zero;
        if (Input.GetKey(KeyCode.W))
        {
            direction += forward;
        }
        if (Input.GetKey(KeyCode.S))
        {
            direction -= forward;
        }
        if (Input.GetKey(KeyCode.D))
        {
            direction += right;
        }
        if (Input.GetKey(KeyCode.A))
        {
            direction -= right;
        }

        // Normalize direction to prevent faster diagonal movement
        direction.Normalize();

        // Apply movement based on the direction and speed
        Vector3 move = direction * moveSpeed * Time.deltaTime;
        rb.MovePosition(transform.position + move);

        // Optionally, rotate the player to face the movement direction
        if (direction != Vector3.zero)
        {
            transform.forward = direction;
        }
    }

    private void Jump()
    {
        // Apply an upward force to the Rigidbody for jumping
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        isGrounded = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Check if the player is grounded by detecting collisions with the ground
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        // When the player leaves the ground, set grounded to false
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            isGrounded = false;
        }
    }
}
