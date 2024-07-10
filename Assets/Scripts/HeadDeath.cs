using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// This class is responsible for handling the player's death by head.
/// </summary>
public class DeathByHead : MonoBehaviour
{
    private void Start()
    {
        GetComponent<CircleCollider2D>();
    }

    /// <summary>
    /// This method is responsible for detecting the head collision with the ground.
    /// </summary>
    /// <param name="other">
    /// The other collider that the head collided with.
    /// </param>
    /// <returns>
    /// void
    /// </returns>
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Ground")) return;

        // Restart the scene after 1 second
        Invoke(nameof(RestartScene), 1f);
    }

    /// <summary>
    /// This method is responsible for restarting the scene.
    /// </summary>
    /// <returns>
    /// void
    /// </returns>
    private void RestartScene()
    {
        // Restart the current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
