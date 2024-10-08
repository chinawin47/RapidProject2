using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlSwitch : MonoBehaviour
{
    public List<GameObject> controllableObjects; // รายการของออฟเจ็กต์ที่สามารถควบคุมได้
    private GameObject currentControlledObject;  // ออฟเจ็กต์ที่กำลังถูกควบคุมอยู่
    public GameObject player;                     // ออฟเจ็กต์ Player
    private Camera mainCamera;                    // กล้องหลัก
    private bool isControllingPlayer = true;      // ตรวจสอบว่ากำลังควบคุม Player หรือไม่

    public float moveSpeed = 5f;                  // ความเร็วในการเคลื่อนไหวของออฟเจ็กต์
    public Vector3 cameraOffset = new Vector3(0, 2, -10); // Offset สำหรับกล้องจากออฟเจ็กต์ที่ถูกควบคุม

    public float smoothSpeed = 0.125f; // ความนุ่มนวลของการเคลื่อนที่ของกล้อง

    void Start()
    {
        mainCamera = Camera.main; // รับกล้องหลัก
        currentControlledObject = player; // เริ่มด้วยการควบคุม Player
    }

    void Update()
    {
        // สลับระหว่างการควบคุม Player และ Object ด้วยปุ่ม E
        if (Input.GetKeyDown(KeyCode.E))
        {
            ToggleControl();
        }

        // จัดการการเคลื่อนไหวของออฟเจ็กต์ที่ถูกควบคุม
        HandleMovement();

        // อัปเดตตำแหน่งกล้องเพื่อให้ติดตามออฟเจ็กต์ที่ถูกควบคุม
        UpdateCameraPosition();
    }

    // สลับการควบคุมระหว่าง Player และ Object
    private void ToggleControl()
    {
        // ถ้ากำลังควบคุม Player ให้เปลี่ยนไปควบคุมออฟเจ็กต์ถัดไปในรายการ
        if (isControllingPlayer)
        {
            // เปลี่ยนไปควบคุมออฟเจ็กต์ถัดไปในรายการ
            int nextIndex = (controllableObjects.IndexOf(currentControlledObject) + 1) % controllableObjects.Count;
            currentControlledObject = controllableObjects[nextIndex];
        }
        else
        {
            currentControlledObject = player; // กลับไปควบคุม Player
        }

        // เปลี่ยนสถานะการควบคุม
        isControllingPlayer = !isControllingPlayer;
    }

    // จัดการการเคลื่อนไหวของออฟเจ็กต์ที่กำลังถูกควบคุม
    private void HandleMovement()
    {
        // ตรวจสอบว่าออฟเจ็กต์ที่ถูกควบคุมไม่เป็น null
        if (currentControlledObject == null)
            return;

        float horizontalInput = Input.GetAxisRaw("Horizontal");
        Vector3 currentPosition = currentControlledObject.transform.position;

        // เคลื่อนที่ออฟเจ็กต์ในแนวนอนตามข้อมูลที่ป้อน
        currentPosition.x += horizontalInput * moveSpeed * Time.deltaTime;
        currentControlledObject.transform.position = currentPosition;
    }

    // อัปเดตตำแหน่งกล้องเพื่อให้ติดตามออฟเจ็กต์ที่ถูกควบคุม
    private void UpdateCameraPosition()
    {
        if (mainCamera != null && currentControlledObject != null)
        {
            // คำนวณตำแหน่งกล้องใหม่
            Vector3 targetPosition = currentControlledObject.transform.position + cameraOffset;
            Vector3 smoothedPosition = Vector3.Lerp(mainCamera.transform.position, targetPosition, smoothSpeed);
            mainCamera.transform.position = smoothedPosition;

            // ให้กล้องหันไปทางออฟเจ็กต์ที่ถูกควบคุม
            mainCamera.transform.LookAt(currentControlledObject.transform);
        }
    }
}
