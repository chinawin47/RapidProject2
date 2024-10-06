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
    public float wallSlideSpeed = 0.5f;
    public float wallJumpForce = 10f;  // กำหนดแรงสำหรับการกระโดดออกจากกำแพง
    public Vector2 wallJumpDirection = new Vector2(1, 1);  // กำหนดทิศทางในการกระโดดออกจากกำแพง
    private bool isWallJumping;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Start()
    {
        wallJumpDirection.Normalize();
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
            else if (isTouchingWall && !isGrounded)
            {
                WallJump();
            }
            else if (doubleJump)
            {
                rb.AddForce(new Vector2(0, jumpForce * 0.7f), ForceMode2D.Impulse);
                doubleJump = false;
            }
        }

        // Wall sliding logic
        if (isTouchingWall && !isGrounded && rb.velocity.y < 0 && !isWallJumping)
        {
            rb.velocity = new Vector2(rb.velocity.x, -wallSlideSpeed);
        }
    }

    private void WallJump()
    {
        // ป้องกันการกระโดดซ้ำทันทีหลังจาก Wall Jump
        isWallJumping = true;

        // กระโดดในทิศทางตรงข้ามกับกำแพง (ถ้า h เป็นลบ หมายความว่าเคลื่อนไปทางซ้าย, ถ้าเป็นบวกคือเคลื่อนไปทางขวา)
        Vector2 jumpDirection = new Vector2(-h, 1).normalized;

        rb.velocity = new Vector2(0, 0);  // รีเซ็ตความเร็ว
        rb.AddForce(jumpDirection * wallJumpForce, ForceMode2D.Impulse);

        // หน่วงเวลาเล็กน้อยก่อนให้ตัวละครเคลื่อนที่ได้หลังจากกระโดดออกจากกำแพง
        Invoke(nameof(ResetWallJump), 0.2f);
    }

    private void ResetWallJump()
    {
        isWallJumping = false;
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

