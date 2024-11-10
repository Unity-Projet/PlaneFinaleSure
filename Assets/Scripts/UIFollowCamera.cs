using UnityEngine;

public class UIFollowCamera : MonoBehaviour
{
    public Camera mainCamera; // R�f�rence � la cam�ra principale
    public float distanceFromCamera = 32f; // Distance souhait�e entre l'UI et la cam�ra

    private void Start()
    {
        // Assurez-vous que la cam�ra est assign�e
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
            if (mainCamera == null)
            {
                Debug.LogError("Aucune cam�ra principale trouv�e !");
            }
        }
    }

    private void LateUpdate()
    {
        // V�rifie si la cam�ra principale est assign�e
        if (mainCamera != null)
        {
            // Positionne l'UI � une distance fixe de la cam�ra
            Vector3 position = mainCamera.transform.position + mainCamera.transform.forward * distanceFromCamera;
            transform.position = position;

            // Fait que l'UI regarde toujours vers la cam�ra
            transform.LookAt(mainCamera.transform);
            transform.Rotate(0, 180, 0); // Inverser l'orientation pour que le texte soit lisible

            Debug.Log("UI Position: " + transform.position); // D�bogage de la position de l'UI
        }
    }
}
