using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementPlayer : MonoBehaviour
{
    public float moveSpeed = 5f;     // Speed of the player
    public float jumpForce = 5f;     // Jump force
    private Rigidbody rb;            // Reference to the Rigidbody component
    private Vector3 movement;        // Stores the movement input
    private bool isGrounded;         // Check if player is grounded

    void Start()
    {
        // Automatically assign the Rigidbody component
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Horizontal movement (A/D or left/right arrows)
        movement.x = Input.GetAxisRaw("Horizontal") * moveSpeed;
        movement.z = 0f; // Disable forward/backward movement on Z-axis
        movement.y = rb.velocity.y; // Retain current Y velocity (gravity, jumping, etc.)

        // Jumping (W key)
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }

        // Move down (S key - optional: this could be crouch or faster fall)
        if (Input.GetKey(KeyCode.S) && !isGrounded)
        {
            rb.AddForce(Vector3.down * moveSpeed, ForceMode.Force); // Move down faster when falling
        }
    }

    void FixedUpdate()
    {
        // Horizontal movement
        Vector3 newPosition = rb.position + new Vector3(movement.x, 0, 0) * Time.fixedDeltaTime;
        rb.MovePosition(newPosition);
    }

    // Ground detection for jumping (you can replace this with a more accurate ground check like raycasting)
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}

