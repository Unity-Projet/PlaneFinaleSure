using UnityEngine;

public class UIFollowCamera : MonoBehaviour
{
    public Camera mainCamera; // Référence à la caméra principale
    public float distanceFromCamera = 32f; // Distance souhaitée entre l'UI et la caméra

    private void Start()
    {
        // Assurez-vous que la caméra est assignée
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
            if (mainCamera == null)
            {
                Debug.LogError("Aucune caméra principale trouvée !");
            }
        }
    }

    private void LateUpdate()
    {
        // Vérifie si la caméra principale est assignée
        if (mainCamera != null)
        {
            // Positionne l'UI à une distance fixe de la caméra
            Vector3 position = mainCamera.transform.position + mainCamera.transform.forward * distanceFromCamera;
            transform.position = position;

            // Fait que l'UI regarde toujours vers la caméra
            transform.LookAt(mainCamera.transform);
            transform.Rotate(0, 180, 0); // Inverser l'orientation pour que le texte soit lisible

            Debug.Log("UI Position: " + transform.position); // Débogage de la position de l'UI
        }
    }
}
