using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAndBlockController : MonoBehaviour
{
    public GameObject player;  // Reference to the player GameObject
    public GameObject block;   // Reference to the block GameObject

    private bool controllingPlayer = true; // Is the player currently in control?

    private PlayerMovement playerMovement; // Reference to the PlayerMovement script
    private BlockControl blockControl;     // Reference to the BlockControl script

    private void Start()
    {
        // Get references to the movement scripts attached to the player and block
        playerMovement = player.GetComponent<PlayerMovement>();
        blockControl = block.GetComponent<BlockControl>();

        // Ensure player starts with control
        EnablePlayerControl();
    }

    private void Update()
    {
        // Toggle control between player and block when pressing 'E'
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (controllingPlayer)
            {
                EnableBlockControl();
            }
            else
            {
                EnablePlayerControl();
            }
        }
    }

    void EnablePlayerControl()
    {
        controllingPlayer = true;

        // Enable player movement, disable block control
        playerMovement.enabled = true;
        blockControl.enabled = false;
    }

    void EnableBlockControl()
    {
        controllingPlayer = false;

        // Enable block control, disable player movement
        playerMovement.enabled = false;
        blockControl.enabled = true;
    }
}
