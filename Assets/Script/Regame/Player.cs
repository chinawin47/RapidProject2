using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Call this method when the player dies
    public void Die()
    {
        // Handle player death logic here
        Debug.Log("Player has died!");

        // Optionally, you can disable the player or trigger a respawn
        gameObject.SetActive(false); // Disable the player
        // Alternatively, you could reload the scene or show a death screen
    }
}
