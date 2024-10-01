using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;      // The player's Transform to follow
    public Vector3 offset;        // Offset distance between the player and camera
    public float smoothSpeed = 0.125f; // Smoothing speed for the camera's movement

    void LateUpdate()
    {
        // Desired position for the camera
        Vector3 desiredPosition = player.position + offset;

        // Smoothly interpolate between current and desired position
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        // Apply the smoothed position to the camera
        transform.position = smoothedPosition;

        // Optionally: Make the camera look at the player
        // transform.LookAt(player);
    }

}
