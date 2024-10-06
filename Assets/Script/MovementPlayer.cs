using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementPlayer : MonoBehaviour
{
    float h;
    public float speed;
    Rigidbody2D rb;

    public float jumpForce;

    bool isGrounded;
    bool doubleJump;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }


    private void Update()
    {
        h = Input.GetAxisRaw("Horizontal");
        if (Input.GetKeyDown(KeyCode.W))
        {
            if (isGrounded)
            {
                rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
                doubleJump = true;
            }
            else if (doubleJump)
            {
                rb.AddForce(new Vector2(0, jumpForce * 0.7f), ForceMode2D.Impulse);
                doubleJump = false;
            }
        }

        // Optional: Move down faster when falling (S key)
        if (Input.GetKey(KeyCode.S) && !isGrounded)
        {
            rb.AddForce(Vector2.down * speed, ForceMode2D.Force);
        }
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector2(h * speed, rb.velocity.y);

    }



    void flip()
    {
        if (h < -0.01f) transform.localScale = new Vector3(-1, 1, 1);
        if (h > 0.01f) transform.localScale = new Vector3(1, 1, 1);

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

