using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class ShipController : MonoBehaviour
{
    [SerializeField]
    [Range(1000f, 10000f)]
    private float _thrustForce = 7500f;

    [SerializeField]
    [Range(500f, 5000f)]
    private float _yawForce = 5f;

    [SerializeField]
    private float forwardSpeed = 20f;

    [SerializeField]
    private float upwardSpeed = 15f;

    private Rigidbody _rigidBody;
    private float rotationThreshold = 0.1f;

    private bool isGoingUp = false;
    private bool isGameOver = false;
    private float targetHeight = 17f;

    private float targetX = 600.3f;
    private float targetZ = 8.47f;
    private float tolerance = 0.0062f;

    [SerializeField]
    private float dampingFactor = 0.05f;
    private float smoothedYaw = 0f;
    private float maxRotationSpeed = 15f;

    public TMP_Text gameOverMessageText;
    public TMP_Text countdownText;

    private bool countdownStarted = false;

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
        
        StartCoroutine(StartUpwardMovement());
    }

    void Update()
    {
        if (isGameOver) return;

        Vector3 gyroRotation = GetGyroscopeRotation();

        if (IsSignificantRotation(gyroRotation))
        {
            ApplySmoothYawForce(gyroRotation);
        }

        MoveForward();

        if (isGoingUp && transform.position.y < targetHeight)
        {
            MoveUpward();
        }

        if (IsNearTargetPosition())
        {
            GameOver();
        }
    }

    public void StartFlying()
    {
        isGoingUp = true;
    }

    IEnumerator StartUpwardMovement()
    {
        yield return new WaitForSeconds(5f);
        isGoingUp = true;
    }

    void MoveForward()
    {
        Vector3 forwardVelocity = transform.forward * forwardSpeed;
        forwardVelocity.y = _rigidBody.velocity.y;
        _rigidBody.velocity = forwardVelocity;
    }

    void MoveUpward()
    {
        Vector3 upwardVelocity = new Vector3(0f, upwardSpeed * Time.deltaTime, 0f);
        _rigidBody.velocity += upwardVelocity;

        if (transform.position.y >= targetHeight)
        {
            Vector3 clampedPosition = transform.position;
            clampedPosition.y = targetHeight;
            transform.position = clampedPosition;
            _rigidBody.velocity = new Vector3(_rigidBody.velocity.x, 0, _rigidBody.velocity.z);
        }
    }

    void ApplySmoothYawForce(Vector3 gyroRotation)
    {
        smoothedYaw = Mathf.Lerp(smoothedYaw, gyroRotation.y, dampingFactor);
        smoothedYaw *= 0.1f;
        smoothedYaw = Mathf.Clamp(smoothedYaw, -maxRotationSpeed, maxRotationSpeed);
        _rigidBody.AddTorque(transform.up * (_yawForce * smoothedYaw * Time.deltaTime));
    }

    Vector3 GetGyroscopeRotation()
    {
        Quaternion gyroAttitude = Input.gyro.attitude;
        Vector3 rotationEulerAngles = gyroAttitude.eulerAngles;
        return NormalizeGyroRotation(rotationEulerAngles);
    }

    Vector3 NormalizeGyroRotation(Vector3 rotationEulerAngles)
    {
        float normalizedYaw = (rotationEulerAngles.y > 180 ? rotationEulerAngles.y - 360 : rotationEulerAngles.y) / 180f;
        return new Vector3(0f, normalizedYaw, 0f);
    }

    private bool IsSignificantRotation(Vector3 gyroRotation)
    {
        return Mathf.Abs(gyroRotation.y) > rotationThreshold;
    }

    private bool IsNearTargetPosition()
    {
        bool nearX = Mathf.Abs(transform.position.x - targetX) <= tolerance;
        bool nearZ = Mathf.Abs(transform.position.z - targetZ) <= tolerance;
        return nearX || nearZ;
    }

    void GameOver()
    {
        isGameOver = true;
        _rigidBody.velocity = Vector3.zero;
        Debug.Log("Game Over!");
        ShowGameOverMessage();
        StartCoroutine(RestartCountdown());
    }

    void ShowGameOverMessage()
    {
        if (gameOverMessageText != null)
        {
            gameOverMessageText.text = "You hit the gate! Game Over";
        }
    }

    private IEnumerator RestartCountdown()
    {
        int countdownValue = 5;

        while (countdownValue > 0)
        {
            if (countdownText != null)
            {
                countdownText.text = "Restarting in " + countdownValue;
            }
            yield return new WaitForSeconds(1f);
            countdownValue--;
        }

        SceneManager.LoadScene("scene plane 1");
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Gate"))
        {
            GameOver();
        }
    }
}
