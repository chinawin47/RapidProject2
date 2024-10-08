using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSwitch : MonoBehaviour
{
    public PlayerMovement playerMovement;
    public PlayerMovement playerMovement2;
    public bool playerActive = true;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            SwitchPlayer();
        }
    }

    public void SwitchPlayer()
    {
        if (playerActive)
        {
            playerMovement.enabled = false;
            playerMovement2.enabled = true;
            playerActive = false;
        }
        else
        {
            playerMovement.enabled = true;
            playerMovement2.enabled = false;
            playerActive = true;
        }
    }
}
