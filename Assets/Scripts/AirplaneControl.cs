using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class AirplaneControl : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public float vitesse = 5f;
    private float currentSpeed = 0f;
    private bool avancer = false;
    private bool tournerDroite = false;
    private bool tournerGauche = false;
    private bool descendre = false;

    public float sensitivity = 15.0f; // Sensibilité ajustée
    public float verticalSensitivity = 7.5f; // Sensibilité verticale ajustée

    public float maxTiltAngle = 45f;
    public float maxPitchAngle = 30f;

    public float maxHeight = 10f; // Hauteur maximale
    public float minHeight = 0f;  // Hauteur minimale

    public Button controlButton;
    public Button turnRightButton;
    public Button turnLeftButton;
    public Button descendButton;
    public TMP_Text speedText;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        EventTrigger trigger = controlButton.gameObject.AddComponent<EventTrigger>();
        EventTrigger.Entry pointerDownEntry = new EventTrigger.Entry
        {
            eventID = EventTriggerType.PointerDown
        };
        pointerDownEntry.callback.AddListener((data) => { OnPointerDown((PointerEventData)data); });
        trigger.triggers.Add(pointerDownEntry);

        EventTrigger.Entry pointerUpEntry = new EventTrigger.Entry
        {
            eventID = EventTriggerType.PointerUp
        };
        pointerUpEntry.callback.AddListener((data) => { OnPointerUp((PointerEventData)data); });
        trigger.triggers.Add(pointerUpEntry);

        // Configurer les EventTrigger pour les boutons de rotation
        AddEventTrigger(turnRightButton, EventTriggerType.PointerDown, (data) => { tournerDroite = true; });
        AddEventTrigger(turnRightButton, EventTriggerType.PointerUp, (data) => { tournerDroite = false; });

        AddEventTrigger(turnLeftButton, EventTriggerType.PointerDown, (data) => { tournerGauche = true; });
        AddEventTrigger(turnLeftButton, EventTriggerType.PointerUp, (data) => { tournerGauche = false; });

        // Configurer les EventTrigger pour le bouton de descente
        AddEventTrigger(descendButton, EventTriggerType.PointerDown, (data) => { descendre = true; });
        AddEventTrigger(descendButton, EventTriggerType.PointerUp, (data) => { descendre = false; });
    }

    void AddEventTrigger(Button button, EventTriggerType eventType, UnityEngine.Events.UnityAction<BaseEventData> action)
    {
        EventTrigger trigger = button.gameObject.GetComponent<EventTrigger>() ?? button.gameObject.AddComponent<EventTrigger>();
        EventTrigger.Entry entry = new EventTrigger.Entry { eventID = eventType };
        entry.callback.AddListener(action);
        trigger.triggers.Add(entry);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        avancer = true;
        Debug.Log("Button Pressed: Avancer");
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        avancer = false;
        Debug.Log("Button Released: Avancer");
    }

    void Update()
    {
        // Gérer la vitesse de l'avion
        if (avancer)
        {
            currentSpeed = Mathf.Lerp(currentSpeed, vitesse, Time.deltaTime * 2);
        }
        else
        {
            currentSpeed = Mathf.Lerp(currentSpeed, 0, Time.deltaTime * 2);
        }

        transform.Translate(Vector3.forward * currentSpeed * Time.deltaTime);
        speedText.text = currentSpeed.ToString("F2");

        // Mouvement horizontal basé sur l'inclinaison du smartphone
        float dirX = Input.acceleration.x * sensitivity;
        transform.Translate(Vector3.right * dirX * Time.deltaTime); // Déplace l'avion vers la droite/gauche

        // Limiter la position horizontale
        float clampedX = Mathf.Clamp(transform.position.x, -850f, 850f);
        transform.position = new Vector3(clampedX, transform.position.y, transform.position.z);

        // Gérer l'inclinaison verticale
        float verticalTilt = Input.acceleration.z;
        verticalTilt = Mathf.Clamp(verticalTilt, -1, 1);
        if (transform.position.y < maxHeight || verticalTilt < 0)
        {
            transform.Rotate(Vector3.right, verticalTilt * verticalSensitivity * Time.deltaTime);
        }

        float currentXRotation = transform.eulerAngles.x;
        if (currentXRotation > 180) currentXRotation -= 360;
        currentXRotation = Mathf.Clamp(currentXRotation, -maxPitchAngle, maxPitchAngle);
        transform.rotation = Quaternion.Euler(currentXRotation, transform.eulerAngles.y, transform.eulerAngles.z);

        // Appliquer la rotation à gauche ou à droite
        if (tournerDroite)
        {
            transform.Rotate(Vector3.up, 200 * Time.deltaTime); // Ajuste la vitesse de rotation
        }

        if (tournerGauche)
        {
            transform.Rotate(Vector3.up, -200 * Time.deltaTime); // Ajuste la vitesse de rotation
        }

        // Appliquer la descente
        if (descendre && transform.position.y > minHeight)
        {
            Debug.Log("Descending");
            transform.Translate(Vector3.down * verticalSensitivity * Time.deltaTime);
        }
    }
}
