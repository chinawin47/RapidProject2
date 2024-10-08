using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance; // ����� Singleton ����Ѻ��Ҷ֧ GameManager

    public List<string> collectedItems = new List<string>(); // ��¡������������

    // �ӹǹ���������ͧ�������Դ��е�
    public int requiredItemCount = 3; // ������ҧ �� ��ͧ�� 3 �����

    private void Awake()
    {
        // ����� GameManager �����������������
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject); // ����� GameManager �������������
        }
    }

    // �ѧ��ѹ�����������
    public void CollectItem(string itemName)
    {
        if (!collectedItems.Contains(itemName))
        {
            collectedItems.Add(itemName);
            Debug.Log("Collected: " + itemName);
        }
    }

    // �ѧ��ѹ��Ǩ�ͺ�����������ú����ӹǹ����˹��������
    public bool HasRequiredItems()
    {
        return collectedItems.Count >= requiredItemCount; // ��Ǩ�ͺ�ӹǹ�����
    }
}