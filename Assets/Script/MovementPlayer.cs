using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementPlayer : MonoBehaviour
{
    public float moveSpeed = 5f;     // Speed of the player
    public float jumpForce = 5f;     // Jump force
    private Rigidbody2D rb;          // Reference to the Rigidbody2D component
    private Vector2 movement;        // Stores the movement input
    private bool isGrounded;         // Check if player is grounded

    void Start()
    {
        // Automatically assign the Rigidbody2D component
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Horizontal movement (A/D or left/right arrows)
        movement.x = Input.GetAxisRaw("Horizontal") * moveSpeed;

        // Retain the current vertical velocity (gravity, jumping, etc.)
        movement.y = rb.velocity.y;

        // Jumping (Space key)
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }

        // Optional: Move down faster when falling (S key)
        if (Input.GetKey(KeyCode.S) && !isGrounded)
        {
            rb.AddForce(Vector2.down * moveSpeed, ForceMode2D.Force);
        }
    }

    void FixedUpdate()
    {
        // Apply horizontal movement (only X axis is considered in 2D)
        Vector2 newPosition = new Vector2(movement.x, rb.velocity.y) * Time.fixedDeltaTime;
        rb.velocity = new Vector2(movement.x, rb.velocity.y);
    }

    // Ground detection for jumping (you can replace this with a more accurate ground check like raycasting)
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}

