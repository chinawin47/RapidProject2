using UnityEngine;
using UnityEngine.SceneManagement; // Required for scene management

public class Trap : MonoBehaviour
{
    // Ensure the trap has a trigger collider
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the colliding object has the "Player" tag
        if (collision.CompareTag("Player"))
        {
            // Load the next scene
            LoadNextScene();
        }
    }

    private void LoadNextScene()
    {
        // Get the current scene index and increment it
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1; // Move to the next scene in the build settings

        // Check if the next scene index is valid
        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex); // Load the next scene
        }
        else
        {
            Debug.LogWarning("No next scene to load!"); // Log a warning if no next scene exists
        }
    }
}
