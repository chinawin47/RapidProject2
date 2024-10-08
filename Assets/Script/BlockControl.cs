using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockControl : MonoBehaviour
{
    public float moveSpeed = 5f;      // Block movement speed
    public float jumpForce = 7f;      // Jump force for the block
    public Rigidbody2D rb;            // Reference to the Rigidbody2D component
    private bool isGrounded;          // Check if block is grounded

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Block movement controls (A/D or left/right arrows)
        float moveX = Input.GetAxisRaw("Horizontal") * moveSpeed;

        // Move the block horizontally
        rb.velocity = new Vector2(moveX, rb.velocity.y);

        // Jumping (Space key)
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        }
    }

    // Ground detection for jumping (using collision with objects tagged "Ground")
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
