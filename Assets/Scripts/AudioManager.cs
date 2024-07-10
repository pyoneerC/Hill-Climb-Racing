using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Manages all audio-related functionalities within the game, including background music and sound effects.
/// </summary>
public class AudioManager : MonoBehaviour
{
    public Sprite backgroundMusicToggleSpriteOff;
    public Sprite backgroundMusicToggleSpriteOn;
    public Button backgroundMusicToggle;
    private AudioSource _backgroundMusic;
    private bool _isBackgroundMusicOn = true;
    public AudioSource coinSound;
    public AudioSource refuelSound;
    private bool _isSfxOn = true;
    public CrashSoundsOnOverlap crashSoundsOnOverlap;
    public CarMovement carMovement;

    /// <summary>
    /// Initializes the AudioManager component by retrieving the AudioSource component attached to the same GameObject.
    /// This method is called when the script instance is being loaded.
    /// </summary>
    private void Start()
    {
        _backgroundMusic = GetComponent<AudioSource>();
    }

    /// <summary>
    /// Toggles the state of the background music between playing and paused.
    /// </summary>
    /// <remarks>
    /// This method checks the current state of the background music. If it is playing, the music will be paused,
    /// and the toggle button's sprite will change to indicate the music is off. Conversely, if the music is paused,
    /// it will start playing, and the toggle button's sprite will change to indicate the music is on.
    /// </remarks>
    public void ToggleBackgroundMusic()
    {
        // Check if the background music is currently on.
        if (_isBackgroundMusicOn)
        {
            // Pause the music and update the toggle button to show the 'off' sprite.
            _backgroundMusic.Pause();
            backgroundMusicToggle.image.sprite = backgroundMusicToggleSpriteOff;
        }
        else
        {
            // Play the music and update the toggle button to show the 'on' sprite.
            _backgroundMusic.Play();
            backgroundMusicToggle.image.sprite = backgroundMusicToggleSpriteOn;
        }

        // Invert the state of the background music.
        _isBackgroundMusicOn = !_isBackgroundMusicOn;
    }

    /// <summary>
    /// Played when the player touches any coin collectible.
    /// </summary>
    public void PlayOneShotCoinSound()
    {
        if (_isSfxOn)
        {
            coinSound.PlayOneShot(coinSound.clip);
        }
    }

    /// <summary>
    /// Played when the player touches a refuel collectible.
    /// </summary>
    public void PlayOneShotRefuel()
    {
        if (_isSfxOn)
        {
            refuelSound.PlayOneShot(refuelSound.clip);
        }
    }

    /// <summary>
    /// Toggles the state of all sound effects between playing and paused.
    /// </summary>
    public void ToggleAllSfx()
    {

        if (_isSfxOn)
        {
            // Mute all sound effects
            coinSound.volume = 0;
            refuelSound.volume = 0;
            crashSoundsOnOverlap.crashSound1.volume = 0;
            crashSoundsOnOverlap.crashSound2.volume = 0;
            crashSoundsOnOverlap.crashSound3.volume = 0;
            crashSoundsOnOverlap.crashSound4.volume = 0;
            carMovement.carEngine.volume = 0;
            carMovement.goofyCarHorn.volume = 0;
        }
        else
        {
            // Unmute all sound effects
            coinSound.volume = 1;
            refuelSound.volume = 1;
            crashSoundsOnOverlap.crashSound1.volume = 1;
            crashSoundsOnOverlap.crashSound2.volume = 1;
            crashSoundsOnOverlap.crashSound3.volume = 1;
            crashSoundsOnOverlap.crashSound4.volume = 1;
            carMovement.carEngine.volume = 1;
            carMovement.goofyCarHorn.volume = 1;
        }

        // Toggle the sound effects state
        _isSfxOn = !_isSfxOn;
    }
}
