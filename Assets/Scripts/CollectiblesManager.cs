using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollectiblesManager : MonoBehaviour
{
    // This class is in charge of managing the fuel of the car.
    // We get fuel by overlapping with it.
    // We lose fuel by moving.
    // We will display the fuel and the coins in the GUI. Fuel in image that fills and coins in a text.

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

    private void Update()
    {
        fuel -= Mathf.Clamp(Mathf.Abs(carBody.velocity.x), 0, 0.8f) * Time.deltaTime * 2.5f;
        fuel -= Mathf.Clamp(Mathf.Abs(carBody.velocity.y), 0, 0.8f) * Time.deltaTime * 2.5f;

        if (!(fuel <= 0)) return;
        Invoke(nameof(RestartLevel), 1f);
    }

    private void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

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


    private static IEnumerator MakeObjectFloatAwayAndFadeOut(GameObject obj)
    {
        const float duration = 1f;
        var elapsedTime = 0f;
        var startPosition = obj.transform.position;
        var endPosition = new Vector3(obj.transform.position.x, 5, obj.transform.position.z);
        var spriteRenderer = obj.GetComponent<SpriteRenderer>();

        while (elapsedTime < duration)
        {
            var newY = Mathf.Lerp(startPosition.y, endPosition.y, elapsedTime / duration);
            obj.transform.position = new Vector3(obj.transform.position.x, newY, obj.transform.position.z);

            var color = spriteRenderer.color;
            color.a = Mathf.Lerp(1f, 0f, elapsedTime / duration);
            spriteRenderer.color = color;

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        Destroy(obj, 1f);
    }
}
