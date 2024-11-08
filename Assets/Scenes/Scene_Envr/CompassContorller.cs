using UnityEngine;

public class CompassController : MonoBehaviour
{
    // Reference to the needle (arrow) transform
    private RectTransform needleRectTransform;

    void Awake()
    {
        // Get the RectTransform component attached to the needle GameObject
        needleRectTransform = GetComponent<RectTransform>();

        // Enable the gyroscope
        Input.gyro.enabled = true;
    }

    void Update()
    {
        // Get the yaw value from the gyroscope, which represents the rotation around the Y-axis (up/down)
        float yaw = GetGyroscopeYaw();

        // Rotate the compass needle to reflect the yaw value
        UpdateNeedleRotation(yaw);
    }

    // Function to retrieve the gyroscope yaw
    float GetGyroscopeYaw()
    {
        // Get the gyroscope's attitude (orientation)
        Quaternion gyroAttitude = Input.gyro.attitude;

        // Convert the quaternion to Euler angles to get the yaw (rotation around Y-axis)
        Vector3 gyroRotation = gyroAttitude.eulerAngles;

        // Return the yaw (Y-axis) rotation
        return gyroRotation.y;
    }

    // Function to update the compass needle rotation
    void UpdateNeedleRotation(float yaw)
    {
        // Set the rotation of the needle based on the yaw value
        // Adjust the rotation value to align with the compass direction
        needleRectTransform.localEulerAngles = new Vector3(0, 0, -yaw);
    }
}
