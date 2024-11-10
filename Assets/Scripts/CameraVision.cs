using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CameraVision : MonoBehaviour
{
    public CinemachineVirtualCamera frontCamera; // R�f�rence � la cam�ra virtuelle avant
    public CinemachineVirtualCamera backCamera;  // R�f�rence � la cam�ra virtuelle arri�re

    private void Start()
    {
        // Assurez-vous que seule la cam�ra arri�re est active au d�but
        backCamera.gameObject.SetActive(true);
        frontCamera.gameObject.SetActive(false);
    }

    // Fonction pour basculer entre les cam�ras
    public void SwitchCamera()
    {
        if (frontCamera.gameObject.activeSelf)
        {
            // Si la cam�ra avant est active, d�sactivez-la et activez la cam�ra arri�re
            frontCamera.gameObject.SetActive(false);
            backCamera.gameObject.SetActive(true);
        }
        else
        {
            // Sinon, activez la cam�ra avant et d�sactivez la cam�ra arri�re
            frontCamera.gameObject.SetActive(true);
            backCamera.gameObject.SetActive(false);
        }
    }
}
