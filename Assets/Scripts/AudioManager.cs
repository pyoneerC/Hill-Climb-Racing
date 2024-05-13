using UnityEngine;
using UnityEngine.UI;

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

    private void Start()
    {
        _backgroundMusic = GetComponent<AudioSource>();
    }

    public void ToggleBackgroundMusic()
    {
        if (_isBackgroundMusicOn)
        {
            _backgroundMusic.Pause();
            backgroundMusicToggle.image.sprite = backgroundMusicToggleSpriteOff;
        }
        else
        {
            _backgroundMusic.Play();
            backgroundMusicToggle.image.sprite = backgroundMusicToggleSpriteOn;
        }

        _isBackgroundMusicOn = !_isBackgroundMusicOn;
    }

    public void PlayOneShotCoinSound()
    {
        if (_isSfxOn)
        {
            coinSound.PlayOneShot(coinSound.clip);
        }
    }

    public void PlayOneShotRefuel()
    {
        if (_isSfxOn)
        {
            refuelSound.PlayOneShot(refuelSound.clip);
        }
    }

    public void ToggleAllSfx()
    {

        if (_isSfxOn)
        {
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
            coinSound.volume = 1;
            refuelSound.volume = 1;
            crashSoundsOnOverlap.crashSound1.volume = 1;
            crashSoundsOnOverlap.crashSound2.volume = 1;
            crashSoundsOnOverlap.crashSound3.volume = 1;
            crashSoundsOnOverlap.crashSound4.volume = 1;
            carMovement.carEngine.volume = 1;
            carMovement.goofyCarHorn.volume = 1;
        }

        _isSfxOn = !_isSfxOn;
    }
}
