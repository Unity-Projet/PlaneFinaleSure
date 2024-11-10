using UnityEngine;

public class CameraSwitch : MonoBehaviour
{
    public Camera mainCamera;        // Assign the main camera in the Inspector
    public Camera cockpitCamera;     // Assign the cockpit camera in the Inspector
    public ShipController shipController; // Reference to the ShipController script

    private bool isFlying = false;   // Track whether the ship has started flying

    private void Start()
    {
        // Initially, the main camera is active and cockpit camera is inactive
        mainCamera.enabled = true;
        cockpitCamera.enabled = false;

        // Ensure only one Audio Listener is active
        mainCamera.GetComponent<AudioListener>().enabled = true;
        cockpitCamera.GetComponent<AudioListener>().enabled = false;
    }

    // Method to switch to cockpit view and start flying
    public void SwitchToCockpitView()
    {
        if (mainCamera.enabled && !isFlying)
        {
            // Switch to cockpit camera
            mainCamera.enabled = false;
            cockpitCamera.enabled = true;

            // Toggle Audio Listener
            mainCamera.GetComponent<AudioListener>().enabled = false;
            cockpitCamera.GetComponent<AudioListener>().enabled = true;

            // Start flying when cockpit view is switched
            isFlying = true;
            if (shipController != null)
            {
                shipController.StartFlying(); // Start both flying and forward movement
            }

            Debug.Log("Switched to cockpit view and started flying");
        }
    }

    // Method to switch back to plane view
    public void SwitchToPlaneView()
    {
        if (cockpitCamera.enabled)
        {
            // Switch back to main camera
            mainCamera.enabled = true;
            cockpitCamera.enabled = false;

            // Toggle Audio Listener
            mainCamera.GetComponent<AudioListener>().enabled = true;
            cockpitCamera.GetComponent<AudioListener>().enabled = false;

            // Reset flying status if returning to plane view
            isFlying = false;

            Debug.Log("Switched to plane view");
        }
    }
}
