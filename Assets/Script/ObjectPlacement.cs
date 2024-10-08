using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ObjectPlacement : MonoBehaviour
{
    public GameObject indicatorObject; // วัตถุที่แสดงว่าประตูเปิด
    private Color originalColor; // เก็บค่าเริ่มต้นของสี
    private bool canOpenDoor = false; // เช็คว่าสามารถเปิดประตูได้ไหม

    void Start()
    {
        if (indicatorObject != null)
        {
            originalColor = indicatorObject.GetComponent<SpriteRenderer>().color;
            indicatorObject.SetActive(false); // ซ่อนวัตถุแจ้งเตือนตอนเริ่มต้น
        }
    }

    void Update()
    {
        if (canOpenDoor && Input.GetKeyDown(KeyCode.E)) // เช็คว่ากด E เพื่อเปิดประตู
        {
            LoadNextLevel(); // ฟังก์ชันโหลดด่านถัดไป
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("MovableObject"))
        {
            ActivateIndicator(); // แสดงว่าวัตถุเปิดแล้ว
            canOpenDoor = true; // สามารถเปิดประตูได้
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("MovableObject"))
        {
            DeactivateIndicator(); // ซ่อนวัตถุแจ้งเตือนเมื่อวัตถุออกจากพื้นที่
            canOpenDoor = false; // ไม่สามารถเปิดประตูได้
        }
    }

    void ActivateIndicator()
    {
        if (indicatorObject != null)
        {
            indicatorObject.SetActive(true); // แสดงวัตถุแจ้งเตือน
            SpriteRenderer sr = indicatorObject.GetComponent<SpriteRenderer>();
            if (sr != null)
            {
                sr.color = Color.white; // เปลี่ยนสีเป็นสีขาว
            }
        }
    }

    void DeactivateIndicator()
    {
        if (indicatorObject != null)
        {
            SpriteRenderer sr = indicatorObject.GetComponent<SpriteRenderer>();
            if (sr != null)
            {
                sr.color = originalColor; // เปลี่ยนกลับเป็นสีเดิม
            }
            indicatorObject.SetActive(false); // ซ่อนวัตถุแจ้งเตือน
        }
    }

    void LoadNextLevel()
    {
        // เปลี่ยนชื่อ "NextLevelScene" เป็นชื่อของฉากถัดไปของคุณ
        SceneManager.LoadScene("Scene2");
    }

}
