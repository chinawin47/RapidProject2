using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ObjectPlacement : MonoBehaviour
{
    public GameObject indicatorObject; // �ѵ�ط���ʴ���һ�е��Դ
    private Color originalColor; // �纤��������鹢ͧ��
    private bool canOpenDoor = false; // ���������ö�Դ��е������

    void Start()
    {
        if (indicatorObject != null)
        {
            originalColor = indicatorObject.GetComponent<SpriteRenderer>().color;
            indicatorObject.SetActive(false); // ��͹�ѵ������͹�͹�������
        }
    }

    void Update()
    {
        if (canOpenDoor && Input.GetKeyDown(KeyCode.E)) // ����ҡ� E �����Դ��е�
        {
            LoadNextLevel(); // �ѧ��ѹ��Ŵ��ҹ�Ѵ�
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("MovableObject"))
        {
            ActivateIndicator(); // �ʴ�����ѵ���Դ����
            canOpenDoor = true; // ����ö�Դ��е���
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("MovableObject"))
        {
            DeactivateIndicator(); // ��͹�ѵ������͹������ѵ���͡�ҡ��鹷��
            canOpenDoor = false; // �������ö�Դ��е���
        }
    }

    void ActivateIndicator()
    {
        if (indicatorObject != null)
        {
            indicatorObject.SetActive(true); // �ʴ��ѵ������͹
            SpriteRenderer sr = indicatorObject.GetComponent<SpriteRenderer>();
            if (sr != null)
            {
                sr.color = Color.white; // ����¹�����բ��
            }
        }
    }

    void DeactivateIndicator()
    {
        if (indicatorObject != null)
        {
            SpriteRenderer sr = indicatorObject.GetComponent<SpriteRenderer>();
            if (sr != null)
            {
                sr.color = originalColor; // ����¹��Ѻ�������
            }
            indicatorObject.SetActive(false); // ��͹�ѵ������͹
        }
    }

    void LoadNextLevel()
    {
        // ����¹���� "NextLevelScene" �繪��ͧ͢�ҡ�Ѵ仢ͧ�س
        SceneManager.LoadScene("Scene2");
    }

}
