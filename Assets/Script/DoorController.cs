using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorController : MonoBehaviour
{
    public string nextSceneName; // ชื่อของฉากถัดไปที่ต้องการไป
    public int requiredItemCount = 3; // จำนวนไอเท็มที่ต้องเก็บเพื่อเปิดประตู

    private void Update()
    {
        // ตรวจสอบว่าเกิดการกดปุ่ม E ขณะอยู่ใกล้ประตู
        if (Input.GetKeyDown(KeyCode.E) && GameManager.instance.HasRequiredItems())
        {
            // ไปยังด่านถัดไป
            SceneManager.LoadScene(nextSceneName);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // ตรวจสอบว่าผู้เล่นอยู่ใกล้ประตูหรือไม่
        if (collision.CompareTag("Player"))
        {
            // ให้แสดงข้อความบอกให้ผู้เล่นกด E ถ้ามีไอเท็มครบ
            Debug.Log("Press E to open the door.");
        }
    }
}