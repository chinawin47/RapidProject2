using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollector : MonoBehaviour
{
    public int totalItemsNeeded = 3; // จำนวนไอเท็มที่ต้องเก็บเพื่อปลดล็อกประตู
    private int itemsCollected = 0;  // จำนวนไอเท็มที่เก็บได้ในขณะนี้

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Item")) // ตรวจสอบว่าชนกับไอเท็มหรือไม่
        {
            itemsCollected++;
            Destroy(other.gameObject); // ลบไอเท็มออกจากเกมหลังจากเก็บได้

            Debug.Log("Collected Item: " + itemsCollected);

            if (itemsCollected >= totalItemsNeeded)
            {
                Debug.Log("All items collected! You can open the door.");
            }
        }
    }

    public bool HasAllItems()
    {
        return itemsCollected >= totalItemsNeeded;
    }
}
