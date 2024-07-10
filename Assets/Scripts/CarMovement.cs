using UnityEngine;

/// <summary>
/// This class is in charge of moving the player's car.
/// </summary>
public class CarMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D frontWheel;
    [SerializeField] private Rigidbody2D backWheel;
    [SerializeField] private Rigidbody2D carBody;
    [Range(0, 1000)]
    [SerializeField] private float speed = 150f;
    [Range(0, 1000)]
    [SerializeField] private float rotationSpeed = 500f;
    public AudioSource carEngine;
    private float _jumpTimer;
    private const float JumpDelay = 1.5f;
    public AudioSource goofyCarHorn;

    private float _moveInput;

    private void Start()
    {
        // Play the car engine sound and loop it.
        carEngine.Play();
        carEngine.loop = true;
    }

    /// <summary>
    /// Handles the movement of the car and the horn.
    /// </summary>
    /// <returns>
    /// void
    /// </returns>
    private void Update()
{
    _moveInput = Input.GetAxis("Horizontal");

    carEngine.pitch = _moveInput;

    transform.localScale = _moveInput switch
    {
        < 0 => new Vector3(-1, 1, 1),
        > 0 => new Vector3(1, 1, 1),
        _ => transform.localScale
    };

    _jumpTimer -= Time.deltaTime;

    if (Input.GetKeyDown(KeyCode.H))
    {
        goofyCarHorn.Play();
    }

    if (!Input.GetKeyDown(KeyCode.Space) || !(_jumpTimer <= 0)) return;

    // Jump
    carBody.AddForce(Vector2.up * 500f);
    _jumpTimer = JumpDelay;
}

    /// <summary>
    /// Applies torque to the car's wheels and body to simulate movement and rotation based on player input.
    /// </summary>
    /// <remarks>
    /// This method is called every fixed framerate frame. It reads the player's input (_moveInput) to determine the direction and intensity of the force applied to the car's wheels and body.
    /// Negative torque is applied to simulate forward or backward movement based on the input direction. The car's body is also rotated to simulate steering.
    /// </remarks>
    /// <returns>
    /// void
    /// </returns>
    private void FixedUpdate()
    {
        // Apply torque to the front wheel in the opposite direction to simulate movement.
        frontWheel.AddTorque(-_moveInput * speed * Time.fixedDeltaTime);
        // Apply torque to the back wheel in the opposite direction to simulate movement.
        backWheel.AddTorque(-_moveInput * speed * Time.fixedDeltaTime);
        // Apply torque to the car's body to simulate rotation based on the input direction.
        carBody.AddTorque(-_moveInput * rotationSpeed * Time.fixedDeltaTime);
    }
