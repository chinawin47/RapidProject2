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
    bool isTouchingWall;
    bool isSliding;
    public float wallSlideSpeed = 0.5f;
    public float wallJumpCooldown = 0.2f;  // เวลาหน่วงสำหรับการกระโดดกำแพง
    public Vector2 wallJumpForce;// กำหนดทิศทางในการกระโดดออกจากกำแพง
    bool isWallJumping;

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
            else if (isTouchingWall)
            {
                WallJump();
            }
            else if (doubleJump)
            {
                rb.AddForce(new Vector2(0, jumpForce * 0.7f), ForceMode2D.Impulse);
                doubleJump = false;
            }
        }
        if (isTouchingWall && !isGrounded && rb.velocity.y < 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, -wallSlideSpeed);
        }
        
    }

    private void FixedUpdate()
    {
        
        if (isWallJumping)
        {
            rb.velocity = new Vector2(wallJumpForce.x * -Mathf.Sign(h), wallJumpForce.y);
        }
        else
        {
            rb.velocity = new Vector2(h * speed, rb.velocity.y);
        }
    }

    private void WallJump()
    {
        isWallJumping = true;
        rb.velocity = new Vector2(wallJumpForce.x * -Mathf.Sign(h), wallJumpForce.y); // กระโดดออกจากกำแพงในทิศทางตรงข้าม
        Invoke("ResetWallJump", wallJumpCooldown);  // รอเวลาหน่วงก่อนให้ตัวละครเคลื่อนไหวได้อีกครั้ง
    }

    private void ResetWallJump()
    {
        isWallJumping = false;
    }

    void flip()
    {
        if (h < -0.01f) transform.localScale = new Vector3(-1, 1, 1);
        if (h > 0.01f) transform.localScale = new Vector3(1, 1, 1);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }

        if (collision.gameObject.CompareTag("Wall") && !isGrounded)
        {
            isTouchingWall = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }

        if (collision.gameObject.CompareTag("Wall"))
        {
            isTouchingWall = false;
        }
    }
}

