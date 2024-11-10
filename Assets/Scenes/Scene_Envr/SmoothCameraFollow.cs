using UnityEngine;

public class SmoothCameraFollow : MonoBehaviour
{
    public Transform target; // L'avion à suivre
    public Vector3 offset; // Décalage de la caméra par rapport à l'avion
    public float smoothSpeed = 0.125f; // Vitesse de lissage du mouvement

    void LateUpdate()
    {
        if (target != null)
        {
            Vector3 desiredPosition = target.position + offset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;

            transform.LookAt(target);
        }
    }
}