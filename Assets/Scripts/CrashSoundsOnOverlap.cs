using UnityEngine;

/// <summary>
/// This script plays a random crash sound when the car collides with the ground.
/// </summary>
public class CrashSoundsOnOverlap : MonoBehaviour
{
    public AudioSource crashSound1;
    public AudioSource crashSound2;
    public AudioSource crashSound3;
    public AudioSource crashSound4;

    /// <summary>
    /// Plays a random crash sound when the car collides with the ground.
    /// </summary>
    /// <param name="other">
    /// The other collider that the car collided with.
    /// </param>
    /// <returns>
    /// void
    /// </returns>
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Ground")) return;

        var random = Random.Range(0, 3);
        switch (random)
        {
            case 0:
                crashSound1.Play();
                break;
            case 1:
                crashSound2.Play();
                break;
            case 2:
                crashSound3.Play();
                break;
            case 3:
                crashSound4.Play();
                break;
            default:
                Debug.LogError("WHAT?! How did you get here? This should never happen!");
                break;
        }

    }
}
