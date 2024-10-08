using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    private Vector3 savedPosition;
    private Quaternion savedRotation;
    private bool isPositionSaved = false; // ติดตามว่าตำแหน่งถูกบันทึกหรือไม่

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T)) // ปุ่ม T สำหรับบันทึกหรือลงย้อนเวลา
        {
            if (!isPositionSaved)
            {
                // บันทึกตำแหน่งและการหมุนเมื่อกด T ครั้งแรก
                savedPosition = transform.position;
                savedRotation = transform.rotation;
                isPositionSaved = true; // เปลี่ยนสถานะว่าได้บันทึกแล้ว
                Debug.Log("Position saved: " + savedPosition);
            }
            else
            {
                // ย้อนกลับไปยังตำแหน่งที่บันทึกเมื่อกด T ครั้งที่สอง
                transform.position = savedPosition;
                transform.rotation = savedRotation;
                isPositionSaved = false; // รีเซ็ตสถานะ
                Debug.Log("Rewind to position: " + savedPosition);
            }
        }
    }
}


