using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance; // ตัวแปร Singleton สำหรับเข้าถึง GameManager

    public List<string> collectedItems = new List<string>(); // รายการไอเท็มที่เก็บ

    // จำนวนไอเท็มที่ต้องเก็บเพื่อเปิดประตู
    public int requiredItemCount = 3; // ตัวอย่าง เช่น ต้องเก็บ 3 ไอเท็ม

    private void Awake()
    {
        // เช็คว่า GameManager มีอยู่แล้วหรือไม่
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject); // ทำลาย GameManager ถ้ามีอยู่แล้ว
        }
    }

    // ฟังก์ชันเพื่อเก็บไอเท็ม
    public void CollectItem(string itemName)
    {
        if (!collectedItems.Contains(itemName))
        {
            collectedItems.Add(itemName);
            Debug.Log("Collected: " + itemName);
        }
    }

    // ฟังก์ชันตรวจสอบว่ามีไอเท็มครบตามจำนวนที่กำหนดหรือไม่
    public bool HasRequiredItems()
    {
        return collectedItems.Count >= requiredItemCount; // ตรวจสอบจำนวนไอเท็ม
    }
}