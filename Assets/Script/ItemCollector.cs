using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollector : MonoBehaviour
{
    public int totalItemsNeeded = 3; // �ӹǹ���������ͧ�����ͻŴ��͡��е�
    private int itemsCollected = 0;  // �ӹǹ������������㹢�й��

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Item")) // ��Ǩ�ͺ��Ҫ��Ѻ������������
        {
            itemsCollected++;
            Destroy(other.gameObject); // ź������͡�ҡ����ѧ�ҡ����

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
