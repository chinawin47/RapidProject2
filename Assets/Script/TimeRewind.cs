using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeRewind : MonoBehaviour
{
    // A class to store position, rotation, etc.
    private class RewindState
    {
        public Vector2 position;
        public Quaternion rotation;
    }

    public float recordDuration = 5f; // How many seconds of data to store
    private List<RewindState> rewindStates; // List to store all the recorded states
    private bool isRewinding = false; // Check if rewinding is active

    private Rigidbody2D rb;

    void Start()
    {
        rewindStates = new List<RewindState>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T)) // Press 'R' to start rewinding
        {
            StartRewind();
        }

        if (Input.GetKeyUp(KeyCode.T)) // Release 'R' to stop rewinding
        {
            StopRewind();
        }
    }

    void FixedUpdate()
    {
        if (isRewinding)
        {
            Rewind();
        }
        else
        {
            Record();
        }
    }

    void Record()
    {
        // Store the current position and rotation into a state
        if (rewindStates.Count > Mathf.Round(recordDuration / Time.fixedDeltaTime))
        {
            rewindStates.RemoveAt(rewindStates.Count - 1); // Limit the length of stored states
        }

        rewindStates.Insert(0, new RewindState
        {
            position = transform.position,
            rotation = transform.rotation
        });
    }

    void Rewind()
    {
        if (rewindStates.Count > 0)
        {
            // Get the last recorded state and apply it
            RewindState state = rewindStates[0];
            transform.position = state.position;
            transform.rotation = state.rotation;

            rewindStates.RemoveAt(0); // Remove the state after applying it
        }
        else
        {
            // Stop rewinding when no more states are available
            StopRewind();
        }
    }

    public void StartRewind()
    {
        isRewinding = true;
        rb.isKinematic = true; // Disable physics while rewinding
    }

    public void StopRewind()
    {
        isRewinding = false;
        rb.isKinematic = false; // Enable physics again after rewinding
    }
}
