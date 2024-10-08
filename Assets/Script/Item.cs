using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public string itemName; // ชื่อไอเท็มที่เก็บ

    // ฟังก์ชันที่จะถูกเรียกเมื่อเกิดการชน
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // ตรวจสอบว่าผู้เล่นชนกับไอเท็มหรือไม่
        if (collision.CompareTag("Player"))
        {
            // เรียกใช้งานฟังก์ชันใน GameManager เพื่อเก็บไอเท็ม
            GameManager.instance.CollectItem(itemName);
            // ทำให้ไอเท็มหายไปจากฉาก
            Destroy(gameObject);
        }
    }
}
