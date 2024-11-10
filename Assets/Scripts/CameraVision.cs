using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CameraVision : MonoBehaviour
{
    public CinemachineVirtualCamera frontCamera; // Référence à la caméra virtuelle avant
    public CinemachineVirtualCamera backCamera;  // Référence à la caméra virtuelle arrière

    private void Start()
    {
        // Assurez-vous que seule la caméra arrière est active au début
        backCamera.gameObject.SetActive(true);
        frontCamera.gameObject.SetActive(false);
    }

    // Fonction pour basculer entre les caméras
    public void SwitchCamera()
    {
        if (frontCamera.gameObject.activeSelf)
        {
            // Si la caméra avant est active, désactivez-la et activez la caméra arrière
            frontCamera.gameObject.SetActive(false);
            backCamera.gameObject.SetActive(true);
        }
        else
        {
            // Sinon, activez la caméra avant et désactivez la caméra arrière
            frontCamera.gameObject.SetActive(true);
            backCamera.gameObject.SetActive(false);
        }
    }
}
