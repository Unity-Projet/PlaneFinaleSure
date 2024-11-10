using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;  // For loading scenes

public class ShipController : MonoBehaviour
{
    [SerializeField]
    [Range(1000f, 10000f)]
    private float _thrustForce = 7500f;

    [SerializeField]
    [Range(500f, 5000f)]
    private float _yawForce = 500f;

    [SerializeField]
    private float forwardSpeed = 20f;  // Constant forward speed

    [SerializeField]
    private float upwardSpeed = 15f;   // Upward speed after button press

    private Rigidbody _rigidBody;
    private bool isGoingUp = false;    // Flag to start upward movement
    private bool isMovingForward = false;  // Flag to start forward movement
    private bool isGameOver = false;   // Flag for game over
    private float targetHeight = 17f;  // Target height

    // Game Over logic
    private float targetX = 600.3f;
    private float targetZ = 8.47f;
    private float tolerance = 0.0062f;  // Tolerance range

    // Damping factor to smooth out rotation
    [SerializeField]
    private float dampingFactor = 0.1f;

    // Slow down factor for gyroscope input
    [SerializeField]
    private float slowDownFactor = 0.5f;  // Adjust this to make rotation slower or faster

    // UI text reference for displaying the "You hit the gate! Game Over" message
    public TextMeshProUGUI gameOverMessageText;
    public TextMeshProUGUI countdownText;  // Reference to the countdown text

    void Awake()
    {
        _rigidBody = GetComponent<Rigidbody>();
        Input.gyro.enabled = true;
    }

    void Start()
    {
        if (gameOverMessageText != null)
        {
            gameOverMessageText.text = "";  // Initially hide the message
        }
        if (countdownText != null)
        {
            countdownText.text = "";  // Hide countdown initially
        }
    }

    void Update()
    {
        if (isGameOver) return;  // Stop movement if game is over

        // Start moving forward only when isMovingForward is true
        if (isMovingForward)
        {
            MoveForward();  // Constant forward movement
        }

        // Start moving upward only when isGoingUp is true and the target height isn't reached
        if (isGoingUp && transform.position.y < targetHeight)
        {
            MoveUpward();
        }

        // Check if the ship has passed near the target x or z position within tolerance
        if (IsNearTargetPosition())
        {
            Debug.Log("Ship has reached or is near the target x or z position. Game Over.");
            GameOver();
        }
    }

    // Method to start moving forward
    void MoveForward()
    {
        Vector3 forwardVelocity = transform.forward * forwardSpeed;
        forwardVelocity.y = _rigidBody.velocity.y; // Preserve the current upward velocity
        _rigidBody.velocity = forwardVelocity;
    }

    // Method to start moving upward
    void MoveUpward()
    {
        Vector3 upwardVelocity = new Vector3(0f, upwardSpeed * Time.deltaTime, 0f);
        _rigidBody.velocity += upwardVelocity;

        // Clamp position to target height
        if (transform.position.y >= targetHeight)
        {
            Vector3 clampedPosition = transform.position;
            clampedPosition.y = targetHeight;
            transform.position = clampedPosition;
            _rigidBody.velocity = new Vector3(_rigidBody.velocity.x, 0, _rigidBody.velocity.z);
        }
    }

    // Method to start flying and moving forward when called
    public void StartFlying()
    {
        // Start both upward movement and forward movement when the button is pressed
        isGoingUp = true;
        isMovingForward = true;
        Debug.Log("Plane started flying and moving forward!");
    }

    // Check if the ship is near the target x or z position within tolerance
    private bool IsNearTargetPosition()
    {
        bool nearX = Mathf.Abs(transform.position.x - targetX) <= tolerance;
        bool nearZ = Mathf.Abs(transform.position.z - targetZ) <= tolerance;
        return nearX || nearZ;
    }

    // Game Over logic
    void GameOver()
    {
        isGameOver = true;
        _rigidBody.velocity = Vector3.zero;
        Debug.Log("Game Over!");
        ShowGameOverMessage();
        StartCoroutine(RestartCountdown());  // Start countdown after game over
    }

    // Show "You hit the gate! Game Over" message
    void ShowGameOverMessage()
    {
        if (gameOverMessageText != null)
        {
            gameOverMessageText.text = "You hit the gate! Game Over";
        }
    }

    // Coroutine to handle the countdown and scene restart
    private IEnumerator RestartCountdown()
    {
        int countdownValue = 5;
        while (countdownValue > 0)
        {
            if (countdownText != null)
            {
                countdownText.text = "Restarting in " + countdownValue;
            }
            yield return new WaitForSeconds(1f);  // Wait for 1 second
            countdownValue--;  // Decrease the countdown value
        }
        // After countdown ends, load the scene
        SceneManager.LoadScene("scene plane 1");
    }

    // Detect collision with the gate
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Gate"))
        {
            GameOver();  // Trigger Game Over when hitting the gate
        }
    }
}
