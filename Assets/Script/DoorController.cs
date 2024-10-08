using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorController : MonoBehaviour
{
    public string nextSceneName; // ���ͧ͢�ҡ�Ѵ价���ͧ����
    public int requiredItemCount = 3; // �ӹǹ���������ͧ�������Դ��е�

    private void Update()
    {
        // ��Ǩ�ͺ����Դ��á����� E �����������е�
        if (Input.GetKeyDown(KeyCode.E) && GameManager.instance.HasRequiredItems())
        {
            // ��ѧ��ҹ�Ѵ�
            SceneManager.LoadScene(nextSceneName);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // ��Ǩ�ͺ��Ҽ�������������е��������
        if (collision.CompareTag("Player"))
        {
            // ����ʴ���ͤ����͡�������蹡� E �����������ú
            Debug.Log("Press E to open the door.");
        }
    }
}