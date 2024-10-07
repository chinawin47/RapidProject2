using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomReset : MonoBehaviour
{
    void Update()
    {
        // ตรวจสอบว่าผู้เล่นกดปุ่ม Y
        if (Input.GetKeyDown(KeyCode.Y))
        {
            ResetRoom();
        }
    }

    // ฟังก์ชันสำหรับรีเซ็ตห้องปัจจุบัน
    void ResetRoom()
    {
        // โหลดซ้ำฉากปัจจุบัน
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
