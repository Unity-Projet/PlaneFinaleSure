using UnityEngine;

public class SmoothCameraFollow : MonoBehaviour
{
    public Transform target; // L'avion � suivre
    public Vector3 offset; // D�calage de la cam�ra par rapport � l'avion
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