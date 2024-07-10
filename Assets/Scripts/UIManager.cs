using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// This class is responsible for managing the UI elements of the game.
/// </summary>
public class UIManager : MonoBehaviour
{
    public Image fuelImageProgressBar;
    public Gradient fuelGradient;
    public Image lowFuelWarning;
    public Image pauseButtonBg;

    public Text coinText;
    public Text distanceCounter;
    public Text boostCounter;
    public Text rpmCounter;
    public Text fuelCounter;

    public CollectiblesManager collectiblesManager;
    private Vector3 _startPosition;
    public Image gasPedal;
    public Sprite gasPedalPressed;
    public Sprite gasPedalNormal;
    public Image brakePedal;
    public Sprite brakePedalPressed;
    public Sprite brakePedalNormal;
    public Button sfxButton;
    public Sprite sfxButtonOn;
    public Sprite sfxButtonOff;
    public AudioManager audioManager;
    public Image coinSprite;
    private bool _a = true;

    private void Start()
    {
        _startPosition = collectiblesManager.carBody.transform.position;
    }

    /// <summary>
    /// This method is responsible for updating the all the UI elements.
    /// </summary>
    /// <returns>
    /// void
    /// </returns>
    private void Update()
    {

        if (collectiblesManager.fuel <= 0)
        {
            RestartLevel();
        }

        UpdateGasBrakeSprites();

        CheckLowFuelWarning();

        UpdateFuelGUI();
        UpdateCoinGUI();

        UpdateRpmCounter();
        UpdateBoostCounter();
    }

    /// <summary>
    /// This method is responsible for updating the UI boost counter. Called on Update.
    /// </summary>
    /// <returns>
    /// void
    /// </returns>
    private void UpdateBoostCounter()
    {
        // Updates the boost counter text with the car's velocity multiplied by 0.5
        boostCounter.text = Mathf.Abs(collectiblesManager.carBody.velocity.magnitude * .5f).ToString("F0");
    }

    /// <summary>
    /// This method is responsible for updating the UI RPM counter and the distance counter. Called on Update.
    /// </summary>
    /// <returns>
    /// void
    /// </returns>
    /// <remarks>
    /// The RPM counter is calculated by multiplying the car's velocity by 3.
    /// </remarks>
    private void UpdateRpmCounter()
    {
        // Calculates the distance between the car's current position and the starting position
        var distance = collectiblesManager.carBody.transform.position - _startPosition;

        // Updates the distance counter text, formatting the distance to a whole number
        distanceCounter.text = distance.magnitude.ToString("F0") + "m";

        // Updates the RPM counter text with the car's velocity multiplied by 3
        rpmCounter.text = Mathf.Abs(collectiblesManager.carBody.velocity.magnitude * 3).ToString("F0");
    }

    /// <summary>
    /// This method is responsible for checking if the fuel level is low and displaying the low fuel warning. Called on Update.
    /// </summary>
    /// <returns>
    /// void
    /// </returns>
    /// <remarks>
    /// The low fuel warning is displayed when the fuel level is below 20%.
    /// </remarks>
    private void CheckLowFuelWarning()
    {
        if (collectiblesManager.fuel < 20)
        {
            lowFuelWarning.enabled = true;
            // Pulsing effect for the low fuel warning
            lowFuelWarning.color = new Color(lowFuelWarning.color.r,
                lowFuelWarning.color.g, lowFuelWarning.color.b, Mathf.PingPong(Time.time, 0.5f));
        }
        else
        {
            lowFuelWarning.enabled = false;
        }
    }

    /// <summary>
    /// This method is responsible for restarting the level. Called when the player runs out of fuel.
    /// </summary>
    /// <returns>
    /// void
    /// </returns>
    public static void RestartLevel()
    {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            // Resets the time scale to 1 in case the game was paused
            Time.timeScale = 1;
    }

    /// <summary>
    /// This method is responsible for pausing the game. It toggles the Time.timeScale between 0 and 1. Called when the pause button is clicked.
    /// </summary>
    /// <returns>
    /// void
    /// </returns>
    public void PauseGame()
    {

        if (_a)
        {
            Time.timeScale = 0;
            // Sets the color of the pause button to red when the game is paused
            pauseButtonBg.color = new Color(1f,0,0,1f);
            // Toggles the value of _a
            _a = false;
        }
        else
        {
            Time.timeScale = 1;
            // Sets the color of the pause button to white when the game is unpaused (default)
            pauseButtonBg.color = new Color(1,1f,1,1f);
            // Toggles the value of _a
            _a = true;
        }
    }

    /// <summary>
    /// This method is responsible for updating the fuel GUI. It updates the fill amount of the fuel image and the color of the fuel image based on the fuel level. Called on Update.
    /// </summary>
    /// <returns>
    /// void
    /// </returns>
    private void UpdateFuelGUI()
    {
        fuelImageProgressBar.fillAmount = collectiblesManager.fuel / 100f;

        // Gradient color based on fuel level for the fuel progress bar
        fuelImageProgressBar.color = fuelGradient.Evaluate(collectiblesManager.fuel / 100f);

        // Updates the fuel counter text with the fuel level as a percentage
        fuelCounter.text = collectiblesManager.fuel.ToString("F0") + "%";

        // Gradient color based on fuel level for the fuel counter text
        fuelCounter.color = fuelGradient.Evaluate(collectiblesManager.fuel / 100f);
    }

    /// <summary>
    /// This method is responsible for updating the coin GUI Text. It converts the coin count to a string and updates the text.
    /// </summary>
    /// <returns>
    /// void
    /// </returns>
    private void UpdateCoinGUI()
    {
        coinText.text = collectiblesManager.coins.ToString();
    }

    /// <summary>
    /// This method is responsible for pulsing the coin sprite in the UI when the player collects a coin.
    /// </summary>
    /// <returns>
    /// void
    /// </returns>
    public void PulseCoinSprite()
    {
        coinSprite.transform.localScale = Vector3.one * (0.3f + Mathf.PingPong(Time.time, 0.2f));
    }

    /// <summary>
    /// This method is responsible for toggling the SFX UI button ON/OFF. Called when the SFX button is clicked.
    /// </summary>
    /// <returns>
    /// void
    /// </returns>
    public void ToggleSfx()
    {
        sfxButton.image.sprite = sfxButton.image.sprite == sfxButtonOn ? sfxButtonOff : sfxButtonOn;
        audioManager.ToggleAllSfx();
    }

    /// <summary>
    /// This method is responsible for updating the gas and brake pedal sprites based on the player's input. Called on Update.
    /// </summary>
    /// <returns>
    /// void
    /// </returns>
    public void UpdateGasBrakeSprites()
    {
        // Updates the gas and brake pedal sprites based on the player's input
        gasPedal.sprite = Input.GetKey(KeyCode.D) ? gasPedalPressed : gasPedalNormal;
        brakePedal.sprite = Input.GetKey(KeyCode.A) ? brakePedalPressed : brakePedalNormal;
    }
}
