using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// This class is in charge of managing the collectibles in the game, including fuel, coins and their respective animations when collected.
/// </summary>
/// <remarks>
/// - We get fuel by overlapping with it.
/// - We lose fuel by moving.
/// </remarks>
/// <value>
/// fuel: The fuel value.
/// coins: The coins value.
/// </value>
public class CollectiblesManager : MonoBehaviour
{
    [Range(0, 100)]
    public float fuel = 100f;
    public int coins;
    public Rigidbody2D carBody;
    public AudioManager audioManager;
    public UIManager uiManager;

    private void Start()
    {
        carBody = GetComponent<Rigidbody2D>();
    }

    /// <summary>
    /// Updates the fuel value and handles the restart of the level if the fuel is less than or equal to 0.
    /// </summary>
    /// <returns>
    /// void
    /// </returns>
    private void Update()
    {
        // We lose fuel by moving.
        fuel -= (Mathf.Clamp(Mathf.Abs(carBody.velocity.x), 0, 0.8f) * Time.deltaTime * 2.5f) * 2;

        if (!(fuel <= 0)) return;

        // Restarts the level if the fuel is less than or equal to 0.
        Invoke(nameof(RestartLevel), 1f);
    }

    /// <summary>
    /// Restarts the level.
    /// </summary>
    /// <returns>
    /// void
    /// </returns>
    private void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    /// <summary>
    /// Handles the collection of fuel and coins trough overlaps.
    /// </summary>
    /// <param name="other"></param>
    /// <returns>
    /// void
    /// </returns>
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag($"Fuel"))
        {
            fuel = 100f;
            audioManager.PlayOneShotRefuel();
            StartCoroutine(MakeObjectFloatAwayAndFadeOut(other.gameObject));
        }
        else if (other.CompareTag($"Coin5"))
        {
            coins += 5;
            audioManager.PlayOneShotCoinSound();
            uiManager.PulseCoinSprite();
            StartCoroutine(MakeObjectFloatAwayAndFadeOut(other.gameObject));
        }
        else if (other.CompareTag($"Coin25"))
        {
            coins += 25;
            audioManager.PlayOneShotCoinSound();
            uiManager.PulseCoinSprite();
            StartCoroutine(MakeObjectFloatAwayAndFadeOut(other.gameObject));
        }
        else if (other.CompareTag($"Coin100"))
        {
            coins += 100;
            audioManager.PlayOneShotCoinSound();
            uiManager.PulseCoinSprite();
            StartCoroutine(MakeObjectFloatAwayAndFadeOut(other.gameObject));
        }
        else if (other.CompareTag($"Coin500"))
        {
            coins += 500;
            audioManager.PlayOneShotCoinSound();
            uiManager.PulseCoinSprite();
            StartCoroutine(MakeObjectFloatAwayAndFadeOut(other.gameObject));
        }
    }

    /// <summary>
    /// Makes the collectible float away and fade out.
    /// </summary>
    /// <param name="obj"></param>
    /// <returns>
    /// It really returns void, but Unity requires it to return IEnumerator for coroutines and delays.
    /// </returns>
    private static IEnumerator MakeObjectFloatAwayAndFadeOut(GameObject obj)
    {
        const float duration = 1f;
        var elapsedTime = 0f;
        var startPosition = obj.transform.position;
        var endPosition = new Vector3(obj.transform.position.x, 5, obj.transform.position.z);
        var spriteRenderer = obj.GetComponent<SpriteRenderer>();

        while (elapsedTime < duration)
        {
            // Move the object up.
            var newY = Mathf.Lerp(startPosition.y, endPosition.y, elapsedTime / duration);
            obj.transform.position = new Vector3(obj.transform.position.x, newY, obj.transform.position.z);

            // Fade out the object.
            var color = spriteRenderer.color;
            color.a = Mathf.Lerp(1f, 0f, elapsedTime / duration);
            spriteRenderer.color = color;

            // Update the time.
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        Destroy(obj, 1f);
    }
}
