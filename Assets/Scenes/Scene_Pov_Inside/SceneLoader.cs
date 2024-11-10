using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    // Function to load the Cockpit scene
    public void LoadCockpitScene()
    {
        SceneManager.LoadScene("scene_cockpit"); // Replace "CockpitScene" with the exact name of your cockpit scene
    }
}
