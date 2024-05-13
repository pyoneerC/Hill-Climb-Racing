using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathByHead : MonoBehaviour
{
    private void Start()
    {
        GetComponent<CircleCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Ground")) return;
        Invoke(nameof(RestartScene), 1f);
    }

    private void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
