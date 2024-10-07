using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomReset : MonoBehaviour
{
    void Update()
    {
        // ��Ǩ�ͺ��Ҽ����蹡����� Y
        if (Input.GetKeyDown(KeyCode.Y))
        {
            ResetRoom();
        }
    }

    // �ѧ��ѹ����Ѻ������ͧ�Ѩ�غѹ
    void ResetRoom()
    {
        // ��Ŵ��өҡ�Ѩ�غѹ
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
