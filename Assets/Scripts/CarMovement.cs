using UnityEngine;

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
        carEngine.Play();
        carEngine.loop = true;
    }

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
    carBody.AddForce(Vector2.up * 500f);
    _jumpTimer = JumpDelay;
}

    private void FixedUpdate()
    {
        frontWheel.AddTorque(-_moveInput * speed * Time.fixedDeltaTime);
        backWheel.AddTorque(-_moveInput * speed * Time.fixedDeltaTime);
        carBody.AddTorque(-_moveInput * rotationSpeed * Time.fixedDeltaTime);
    }
}
