using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlSwitch : MonoBehaviour
{
    public List<GameObject> controllableObjects; // ��¡�âͧ�Ϳ�硵�������ö�Ǻ�����
    private GameObject currentControlledObject;  // �Ϳ�硵�����ѧ�١�Ǻ�������
    public GameObject player;                     // �Ϳ�硵� Player
    private Camera mainCamera;                    // ���ͧ��ѡ
    private bool isControllingPlayer = true;      // ��Ǩ�ͺ��ҡ��ѧ�Ǻ��� Player �������

    public float moveSpeed = 5f;                  // ��������㹡������͹��Ǣͧ�Ϳ�硵�
    public Vector3 cameraOffset = new Vector3(0, 2, -10); // Offset ����Ѻ���ͧ�ҡ�Ϳ�硵���١�Ǻ���

    public float smoothSpeed = 0.125f; // ����������Ţͧ�������͹���ͧ���ͧ

    void Start()
    {
        mainCamera = Camera.main; // �Ѻ���ͧ��ѡ
        currentControlledObject = player; // ��������¡�äǺ��� Player
    }

    void Update()
    {
        // ��Ѻ�����ҧ��äǺ��� Player ��� Object ���»��� E
        if (Input.GetKeyDown(KeyCode.E))
        {
            ToggleControl();
        }

        // �Ѵ��á������͹��Ǣͧ�Ϳ�硵���١�Ǻ���
        HandleMovement();

        // �ѻവ���˹觡��ͧ�������Դ����Ϳ�硵���١�Ǻ���
        UpdateCameraPosition();
    }

    // ��Ѻ��äǺ��������ҧ Player ��� Object
    private void ToggleControl()
    {
        // ��ҡ��ѧ�Ǻ��� Player �������¹令Ǻ����Ϳ�硵�Ѵ����¡��
        if (isControllingPlayer)
        {
            // ����¹令Ǻ����Ϳ�硵�Ѵ����¡��
            int nextIndex = (controllableObjects.IndexOf(currentControlledObject) + 1) % controllableObjects.Count;
            currentControlledObject = controllableObjects[nextIndex];
        }
        else
        {
            currentControlledObject = player; // ��Ѻ令Ǻ��� Player
        }

        // ����¹ʶҹС�äǺ���
        isControllingPlayer = !isControllingPlayer;
    }

    // �Ѵ��á������͹��Ǣͧ�Ϳ�硵�����ѧ�١�Ǻ���
    private void HandleMovement()
    {
        // ��Ǩ�ͺ����Ϳ�硵���١�Ǻ�������� null
        if (currentControlledObject == null)
            return;

        float horizontalInput = Input.GetAxisRaw("Horizontal");
        Vector3 currentPosition = currentControlledObject.transform.position;

        // ����͹����Ϳ�硵���ǹ͹��������ŷ���͹
        currentPosition.x += horizontalInput * moveSpeed * Time.deltaTime;
        currentControlledObject.transform.position = currentPosition;
    }

    // �ѻവ���˹觡��ͧ�������Դ����Ϳ�硵���١�Ǻ���
    private void UpdateCameraPosition()
    {
        if (mainCamera != null && currentControlledObject != null)
        {
            // �ӹǳ���˹觡��ͧ����
            Vector3 targetPosition = currentControlledObject.transform.position + cameraOffset;
            Vector3 smoothedPosition = Vector3.Lerp(mainCamera.transform.position, targetPosition, smoothSpeed);
            mainCamera.transform.position = smoothedPosition;

            // �����ͧ�ѹ价ҧ�Ϳ�硵���١�Ǻ���
            mainCamera.transform.LookAt(currentControlledObject.transform);
        }
    }
}
