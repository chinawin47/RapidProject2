using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxController : MonoBehaviour
{
    public float moveSpeed = 5f;     // Speed of the player
    public float jumpForce = 10f;    // Jump force
    private Rigidbody2D rb;          // Reference to the Rigidbody2D component
    private bool isGrounded;         // Check if player is grounded

    public Transform groundCheck;    // Ground check position
    public float groundCheckRadius = 0.2f; // Radius for detecting ground
    public LayerMask groundLayer;    // LayerMask to identify what is ground

    private bool isFacingRight = true; // To track which direction the player is facing

    void Start()
    {
        // Automatically assign the Rigidbody2D component
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Horizontal movement input (A/D or left/right arrows)
        float moveX = Input.GetAxis("Horizontal") * moveSpeed;

        // Apply horizontal movement to the Rigidbody2D
        rb.velocity = new Vector2(moveX, rb.velocity.y);

        // Flip player sprite based on movement direction
        if (moveX > 0 && !isFacingRight)
        {
            Flip();
        }
        else if (moveX < 0 && isFacingRight)
        {
            Flip();
        }

        // Jumping (Space key)
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    void FixedUpdate()
    {
        // Check if the player is grounded by using Physics2D.OverlapCircle
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
    }

    // Method to flip the player's facing direction
    void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 scaler = transform.localScale;
        scaler.x *= -1;  // Flip the x scale to change direction
        transform.localScale = scaler;
    }

    // Optional: Visualize the ground check radius in the editor
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }
}
