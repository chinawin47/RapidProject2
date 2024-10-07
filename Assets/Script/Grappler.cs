using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grappler : MonoBehaviour
{
    public Camera mainCamera;
    public LineRenderer _lineRenderer;
    public DistanceJoint2D _distanceJoint;
    public LayerMask grappleLayerMask;  // Layer for grapple points
    public float climbSpeed = 2f;        // Speed at which the player can climb the rope
    public float lineLifeTime = 3f;      // Time after which the line will be destroyed
    public float pullForce = 10f;        // Force applied to pull objects
    private Coroutine lineCoroutine;      // To keep track of the line destruction coroutine
    private GameObject connectedObject;   // Store the connected object

    void Start()
    {
        _distanceJoint.enabled = false;
        _lineRenderer.enabled = false; // Ensure the line renderer is initially disabled
    }

    void Update()
    {
        // Shooting the grappling hook
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Vector2 mousePos = (Vector2)mainCamera.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero, Mathf.Infinity, grappleLayerMask);

            // Check if the raycast hits a valid grapple point
            if (hit.collider != null)
            {
                if (_distanceJoint.enabled)
                {
                    // If already grappling, stop the current grapple
                    StopGrapple();
                }

                // Setup the new grappling line
                _lineRenderer.SetPosition(0, hit.point); // Set line's anchor to the grapple point
                _lineRenderer.SetPosition(1, transform.position); // Player position
                _distanceJoint.connectedAnchor = hit.point; // Set the joint anchor to the grapple point
                _distanceJoint.enabled = true;
                _lineRenderer.enabled = true;

                // Store the connected object
                connectedObject = hit.collider.gameObject;

                // Start the coroutine to destroy the line after a set time
                lineCoroutine = StartCoroutine(DestroyLineAfterTime(lineLifeTime));
            }
        }
        else if (Input.GetKeyUp(KeyCode.Mouse0)) // Releasing the grappling hook
        {
            StopGrapple();
        }

        // Updating the rope position
        if (_distanceJoint.enabled)
        {
            _lineRenderer.SetPosition(1, transform.position); // Update the player's end of the line

            // Pull the connected object toward the player
            PullConnectedObject();
            AutoClimb();
        }
    }

    private void StopGrapple()
    {
        _distanceJoint.enabled = false;
        _lineRenderer.enabled = false;

        // Stop the coroutine if it's running
        if (lineCoroutine != null)
        {
            StopCoroutine(lineCoroutine);
            lineCoroutine = null; // Reset the coroutine reference
        }

        connectedObject = null; // Clear the connected object
    }

    private IEnumerator DestroyLineAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        StopGrapple(); // Stop grappling
    }

    void PullConnectedObject()
    {
        if (connectedObject != null)
        {
            // Calculate the direction to pull
            Vector2 direction = (transform.position - connectedObject.transform.position).normalized;
            Rigidbody2D rb = connectedObject.GetComponent<Rigidbody2D>();

            // Apply a force to pull the object
            if (rb != null)
            {
                rb.AddForce(direction * pullForce);
            }
        }
    }

    void AutoClimb()
    {
        // Automatically climb up the rope when grappling
        if (_distanceJoint.enabled)
        {
            _distanceJoint.distance -= climbSpeed * Time.deltaTime; // Shorten the rope distance to climb up

            // Optional: Check for a minimum distance
            if (_distanceJoint.distance < 1f) 
            {
                _distanceJoint.distance = 1f; // Prevent going below the minimum distance
            }
        }
    }
}
