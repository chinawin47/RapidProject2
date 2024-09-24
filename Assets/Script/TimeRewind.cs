using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeRewind : MonoBehaviour
{
    private bool isRewinding = false;
    private List<PlayerState> stateHistory = new List<PlayerState>(); // Stores past states

    [SerializeField] private float rewindDuration = 5f; // How many seconds we can rewind
    [SerializeField] private float rewindSpeed = 1f;    // Speed at which we rewind (1x speed)

    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R)) // Press 'R' to start rewinding
        {
            StartRewind();
        }
        if (Input.GetKeyUp(KeyCode.R)) // Release 'R' to stop rewinding
        {
            StopRewind();
        }
    }

    private void FixedUpdate()
    {
        if (isRewinding)
        {
            Rewind();
        }
        else
        {
            RecordState();
        }
    }

    // Starts rewinding time
    private void StartRewind()
    {
        isRewinding = true;
        rb.isKinematic = true; // Make Rigidbody kinematic while rewinding to prevent physics interaction
    }

    // Stops rewinding time
    private void StopRewind()
    {
        isRewinding = false;
        rb.isKinematic = false; // Return Rigidbody to non-kinematic after rewind
    }

    // Rewinds the player to previous states
    private void Rewind()
    {
        if (stateHistory.Count > 0)
        {
            // Get the last recorded state
            PlayerState lastState = stateHistory[0];
            transform.position = lastState.position;
            transform.rotation = lastState.rotation;

            // Remove the state after applying it
            stateHistory.RemoveAt(0);
        }
        else
        {
            // If no more history is available, stop rewinding
            StopRewind();
        }
    }

    // Records the player's current state (position, rotation, and velocity)
    private void RecordState()
    {
        // Limit history to avoid excessive memory usage
        if (stateHistory.Count > Mathf.Round(rewindDuration / Time.fixedDeltaTime))
        {
            stateHistory.RemoveAt(stateHistory.Count - 1); // Remove the oldest state
        }

        // Save the player's current state
        stateHistory.Insert(0, new PlayerState(transform.position, transform.rotation));
    }
}

// Struct to store the player's state (position and rotation)
[System.Serializable]
public struct PlayerState
{
    public Vector3 position;
    public Quaternion rotation;

    public PlayerState(Vector3 pos, Quaternion rot)
    {
        position = pos;
        rotation = rot;
    }
}
