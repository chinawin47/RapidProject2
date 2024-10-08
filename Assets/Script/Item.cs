using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public string itemName; // ��������������

    // �ѧ��ѹ���ж١���¡������Դ��ê�
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // ��Ǩ�ͺ��Ҽ����蹪��Ѻ������������
        if (collision.CompareTag("Player"))
        {
            // ���¡��ҹ�ѧ��ѹ� GameManager �����������
            GameManager.instance.CollectItem(itemName);
            // �������������仨ҡ�ҡ
            Destroy(gameObject);
        }
    }
}
