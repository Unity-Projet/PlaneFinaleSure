using System.Collections;
using Sirenix.OdinInspector;
using UnityEngine;

public class ShipController : MonoBehaviour
{
    [BoxGroup("Ship movement values")]
    [SerializeField]
    [Range(1000f, 10000f)]
    private float _thrustForce = 7500f,
        _yawForce = 2000f;

    private Rigidbody _rigidBody;

    // Threshold for detecting significant rotation
    [SerializeField]
    private float rotationThreshold = 0.1f;

    void Awake()
    {
        _rigidBody = GetComponent<Rigidbody>();
        Input.gyro.enabled = true;  // Enable the gyroscope
    }

    void Update()
    {
        // Get the device's current gyroscope orientation
        Vector3 gyroRotation = GetGyroscopeRotation();

        // Check for significant rotation before applying forces
        if (IsSignificantRotation(gyroRotation))
        {
            ApplyYawForce(gyroRotation);
        }
    }

    void ApplyYawForce(Vector3 gyroRotation)
    {
        // Apply yaw (Y-axis rotation)
        _rigidBody.AddTorque(transform.up * (_yawForce * gyroRotation.y * Time.deltaTime));
    }

    // Get rotation from the gyroscope sensor
    Vector3 GetGyroscopeRotation()
    {
        // The attitude is the orientation of the device relative to the world
        Quaternion gyroAttitude = Input.gyro.attitude;

        // Convert the quaternion rotation to Euler angles (pitch, yaw, roll)
        Vector3 rotationEulerAngles = gyroAttitude.eulerAngles;

        // Adjust the rotation to ensure it's within a range suitable for controlling the plane
        return NormalizeGyroRotation(rotationEulerAngles);
    }

    // Normalize gyro rotation to match expected ranges
    Vector3 NormalizeGyroRotation(Vector3 rotationEulerAngles)
    {
        // Normalize the rotation values to be between -1 and 1
        float normalizedYaw = (rotationEulerAngles.y > 180 ? rotationEulerAngles.y - 360 : rotationEulerAngles.y) / 180f;

        return new Vector3(0f, normalizedYaw, 0f); // Only return yaw
    }

    // Check if the rotation is significant enough to apply forces
    private bool IsSignificantRotation(Vector3 gyroRotation)
    {
        return Mathf.Abs(gyroRotation.y) > rotationThreshold; // Only check yaw
    }
}
