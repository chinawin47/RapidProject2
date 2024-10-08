using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CameraFollow : MonoBehaviour
{
    public Transform player; // Reference to the player's transform
    public Transform block;  // Reference to the block's transform
    public float smoothSpeed = 0.125f; // Smoothness of camera movement
    public Vector3 offset;   // Offset from the target object

    private Transform currentTarget; // The current target to follow

    void Start()
    {
        // Initially set the camera to follow the player
        currentTarget = player;
    }

    void LateUpdate()
    {
        // Check for input to switch the target
        if (Input.GetKeyDown(KeyCode.E)) // Press Tab to switch
        {
            SwitchTarget();
        }

        // Smoothly move the camera towards the target
        Vector3 desiredPosition = currentTarget.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
    }

    void SwitchTarget()
    {
        // Switch between player and block
        if (currentTarget == player)
        {
            currentTarget = block;
        }
        else
        {
            currentTarget = player;
        }
    }
}
