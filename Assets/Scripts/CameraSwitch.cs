using UnityEngine;

public class CameraSwitch : MonoBehaviour
{
    public Camera mainCamera;        // Assign the main camera in the Inspector
    public Camera cockpitCamera;     // Assign the cockpit camera in the Inspector

    private void Start()
    {
        // Enable the main camera and disable the cockpit camera at the start
        mainCamera.enabled = true;
        cockpitCamera.enabled = false;

        // Ensure only one Audio Listener is active
        mainCamera.GetComponent<AudioListener>().enabled = true;
        cockpitCamera.GetComponent<AudioListener>().enabled = false;
    }

    // Method to switch to cockpit view
    public void SwitchToCockpitView()
    {
        if (mainCamera.enabled)
        {
            // Switch to cockpit camera
            mainCamera.enabled = false;
            cockpitCamera.enabled = true;

            // Toggle Audio Listener
            mainCamera.GetComponent<AudioListener>().enabled = false;
            cockpitCamera.GetComponent<AudioListener>().enabled = true;

            Debug.Log("Switched to cockpit view");
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

            Debug.Log("Switched to plane view");
        }
    }
}