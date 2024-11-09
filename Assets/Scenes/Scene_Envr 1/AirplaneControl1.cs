
//using UnityEngine;
//using UnityEngine.EventSystems;
//using UnityEngine.UI;
//using TMPro;
//using UnityEngine.SceneManagement; // N'oubliez pas d'importer pour utiliser SceneManager

//public class AirplaneControl1 : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
//{
//    public float vitesse = 20f;
//    private float currentSpeed = 0f;
//    private bool avancer = false;
//    private bool tournerDroite = false;
//    private bool tournerGauche = false;
//    private bool descendre = false;

//    public float sensitivity = 15.0f;
//    public float verticalSensitivity = 7.5f;
//    public float maxTiltAngle = 45f;
//    public float maxPitchAngle = 30f;
//    public float maxHeight = 10f;
//    public float minHeight = 0f;

//    public Button controlButton;
//    public Button turnRightButton;
//    public Button turnLeftButton;
//    public Button descendButton;
//    public TMP_Text speedText;

//    // Variables de la mission
//    public Transform targetPoint;
//    public float missionTimeLimit = 60f;
//    private float missionTimeElapsed = 0f;
//    private bool missionCompleted = false;
//    public TMP_Text missionStatusText;

//    public Renderer targetRingRenderer;

//    // Variables pour l'alerte
//    public GameObject alertDialog;
//    public TMP_Text alertText;
//    public Button quitButton;
//    public Button restartButton;

//    private Rigidbody rb;

//    void Start()
//    {
//        rb = GetComponent<Rigidbody>();

//        // Configurer les événements des boutons de contrôle
//        EventTrigger trigger = controlButton.gameObject.AddComponent<EventTrigger>();
//        EventTrigger.Entry pointerDownEntry = new EventTrigger.Entry { eventID = EventTriggerType.PointerDown };
//        pointerDownEntry.callback.AddListener((data) => { OnPointerDown((PointerEventData)data); });
//        trigger.triggers.Add(pointerDownEntry);

//        EventTrigger.Entry pointerUpEntry = new EventTrigger.Entry { eventID = EventTriggerType.PointerUp };
//        pointerUpEntry.callback.AddListener((data) => { OnPointerUp((PointerEventData)data); });
//        trigger.triggers.Add(pointerUpEntry);

//        AddEventTrigger(turnRightButton, EventTriggerType.PointerDown, (data) => { tournerDroite = true; });
//        AddEventTrigger(turnRightButton, EventTriggerType.PointerUp, (data) => { tournerDroite = false; });

//        AddEventTrigger(turnLeftButton, EventTriggerType.PointerDown, (data) => { tournerGauche = true; });
//        AddEventTrigger(turnLeftButton, EventTriggerType.PointerUp, (data) => { tournerGauche = false; });

//        AddEventTrigger(descendButton, EventTriggerType.PointerDown, (data) => { descendre = true; });
//        AddEventTrigger(descendButton, EventTriggerType.PointerUp, (data) => { descendre = false; });

//        // Initialisation de l'interface de mission
//        missionStatusText.text = "Mission: Atteignez le point cible dans 1 minute!";
//    }

//    void AddEventTrigger(Button button, EventTriggerType eventType, UnityEngine.Events.UnityAction<BaseEventData> action)
//    {
//        EventTrigger trigger = button.gameObject.GetComponent<EventTrigger>() ?? button.gameObject.AddComponent<EventTrigger>();
//        EventTrigger.Entry entry = new EventTrigger.Entry { eventID = eventType };
//        entry.callback.AddListener(action);
//        trigger.triggers.Add(entry);
//    }

//    public void OnPointerDown(PointerEventData eventData)
//    {
//        avancer = true;
//    }

//    public void OnPointerUp(PointerEventData eventData)
//    {
//        avancer = false;
//    }

//    void Update()
//    {
//        // Gérer la vitesse de l'avion
//        if (avancer)
//        {
//            currentSpeed = Mathf.Lerp(currentSpeed, vitesse, Time.deltaTime * 2);
//        }
//        else
//        {
//            currentSpeed = Mathf.Lerp(currentSpeed, 0, Time.deltaTime * 2);
//        }

//        transform.Translate(Vector3.forward * currentSpeed * Time.deltaTime);
//        speedText.text = currentSpeed.ToString("F2");

//        // Mouvement horizontal basé sur l'inclinaison du smartphone
//        float dirX = Input.acceleration.x * sensitivity;
//        transform.Translate(Vector3.right * dirX * Time.deltaTime);

//        // Limiter la position horizontale
//        float clampedX = Mathf.Clamp(transform.position.x, -850f, 850f);
//        transform.position = new Vector3(clampedX, transform.position.y, transform.position.z);

//        // Gérer l'inclinaison verticale
//        float verticalTilt = Input.acceleration.z;
//        verticalTilt = Mathf.Clamp(verticalTilt, -1, 1);
//        if (transform.position.y < maxHeight || verticalTilt < 0)
//        {
//            transform.Rotate(Vector3.right, verticalTilt * verticalSensitivity * Time.deltaTime);
//        }

//        float currentXRotation = transform.eulerAngles.x;
//        if (currentXRotation > 180) currentXRotation -= 360;
//        currentXRotation = Mathf.Clamp(currentXRotation, -maxPitchAngle, maxPitchAngle);
//        transform.rotation = Quaternion.Euler(currentXRotation, transform.eulerAngles.y, transform.eulerAngles.z);

//        // Appliquer la rotation à gauche ou à droite
//        if (tournerDroite)
//        {
//            transform.Rotate(Vector3.up, 200 * Time.deltaTime);
//        }

//        if (tournerGauche)
//        {
//            transform.Rotate(Vector3.up, -200 * Time.deltaTime);
//        }

//        // Appliquer la descente
//        if (descendre && transform.position.y > minHeight)
//        {
//            transform.Translate(Vector3.down * verticalSensitivity * Time.deltaTime);
//        }

//        // Gestion de la mission
//        if (!missionCompleted)
//        {
//            missionTimeElapsed += Time.deltaTime;

//            float distanceToTarget = Vector3.Distance(transform.position, targetPoint.position);
//            if (distanceToTarget < 5f)
//            {
//                transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
//                MissionSuccess();
//            }
//            else if (missionTimeElapsed >= missionTimeLimit)
//            {
//                MissionFailed();
//            }
//        }
//    }

//    private void MissionSuccess()
//    {
//        missionCompleted = true;
//        missionStatusText.text = "Mission réussie !";
//        ShowAlert("Mission réussie!", true);

//        if (targetRingRenderer != null)
//        {
//            targetRingRenderer.material.color = Color.green;
//        }
//    }

//    private void MissionFailed()
//    {
//        missionCompleted = true;
//        missionStatusText.text = "Mission échouée !";
//        ShowAlert("Mission échouée!", false);

//        if (targetRingRenderer != null)
//        {
//            targetRingRenderer.material.color = Color.red;
//        }
//    }

//    // Afficher l'alerte
//    private void ShowAlert(string message, bool isSuccess)
//    {
//        alertText.text = message;
//        alertDialog.SetActive(true);

//        quitButton.gameObject.SetActive(true);
//        restartButton.gameObject.SetActive(!isSuccess); // Affiche "Restart" seulement si échec

//        quitButton.onClick.AddListener(QuitToMenu);
//        restartButton.onClick.AddListener(RestartMission);
//    }

//    // Fonction pour quitter au menu principal
//    public void QuitToMenu()
//    {
//        SceneManager.LoadScene("Scene plane 1");
//    }

//    // Fonction pour redémarrer la mission
//    public void RestartMission()
//    {
//        SceneManager.LoadScene("scene_runway1");
//    }
//    public void RetourMenuu()
//    {
//        // Code pour retourner au menu principal, exemple :
//        SceneManager.LoadScene("Scene plane 1");
//    }

//    private void OnTriggerEnter(Collider other)
//    {
//        if (other.CompareTag("TargetRing"))
//        {
//            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
//            MissionSuccess();
//        }
//    }
//}

using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement; // N'oubliez pas d'importer pour utiliser SceneManager

public class AirplaneControl1 : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public float vitesse = 20f;
    private float currentSpeed = 0f;
    private bool avancer = false;
    private bool tournerDroite = false;
    private bool tournerGauche = false;
    private bool descendre = false;

    public float sensitivity = 15.0f;
    public float verticalSensitivity = 7.5f;
    public float maxTiltAngle = 45f;
    public float maxPitchAngle = 30f;
    public float maxHeight = 10f;
    public float minHeight = 0f;

    public Button controlButton;
    public Button turnRightButton;
    public Button turnLeftButton;
    public Button descendButton;
    public TMP_Text speedText;

    // Boutons pour déplacer l'avion à gauche et à droite
    public Button moveLeftButton;
    public Button moveRightButton;

    private bool moveLeft = false;
    private bool moveRight = false;

    // Variables de la mission
    public Transform targetPoint;
    public float missionTimeLimit = 60f;
    private float missionTimeElapsed = 0f;
    private bool missionCompleted = false;
    public TMP_Text missionStatusText;

    public Renderer targetRingRenderer;

    // Variables pour l'alerte
    public GameObject alertDialog;
    public TMP_Text alertText;
    public Button quitButton;
    public Button restartButton;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        // Configurer les événements des boutons de contrôle
        EventTrigger trigger = controlButton.gameObject.AddComponent<EventTrigger>();
        EventTrigger.Entry pointerDownEntry = new EventTrigger.Entry { eventID = EventTriggerType.PointerDown };
        pointerDownEntry.callback.AddListener((data) => { OnPointerDown((PointerEventData)data); });
        trigger.triggers.Add(pointerDownEntry);

        EventTrigger.Entry pointerUpEntry = new EventTrigger.Entry { eventID = EventTriggerType.PointerUp };
        pointerUpEntry.callback.AddListener((data) => { OnPointerUp((PointerEventData)data); });
        trigger.triggers.Add(pointerUpEntry);

        AddEventTrigger(turnRightButton, EventTriggerType.PointerDown, (data) => { tournerDroite = true; });
        AddEventTrigger(turnRightButton, EventTriggerType.PointerUp, (data) => { tournerDroite = false; });

        AddEventTrigger(turnLeftButton, EventTriggerType.PointerDown, (data) => { tournerGauche = true; });
        AddEventTrigger(turnLeftButton, EventTriggerType.PointerUp, (data) => { tournerGauche = false; });

        AddEventTrigger(descendButton, EventTriggerType.PointerDown, (data) => { descendre = true; });
        AddEventTrigger(descendButton, EventTriggerType.PointerUp, (data) => { descendre = false; });

        // Ajouter les événements des boutons pour déplacer l'avion à gauche ou à droite
        AddEventTrigger(moveLeftButton, EventTriggerType.PointerDown, (data) => { moveLeft = true; });
        AddEventTrigger(moveLeftButton, EventTriggerType.PointerUp, (data) => { moveLeft = false; });

        AddEventTrigger(moveRightButton, EventTriggerType.PointerDown, (data) => { moveRight = true; });
        AddEventTrigger(moveRightButton, EventTriggerType.PointerUp, (data) => { moveRight = false; });

        // Initialisation de l'interface de mission
        missionStatusText.text = "Mission: Atteignez le point cible dans 1 minute!";
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
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        avancer = false;
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

        // Appliquer le mouvement horizontal basé sur les boutons
        if (moveLeft)
        {
            transform.Translate(Vector3.left * vitesse * Time.deltaTime);  // Déplacer à gauche
        }
        else if (moveRight)
        {
            transform.Translate(Vector3.right * vitesse * Time.deltaTime);  // Déplacer à droite
        }

        // Limiter la position horizontale
        float clampedX = Mathf.Clamp(transform.position.x, -850f, 850f);
        transform.position = new Vector3(clampedX, transform.position.y, transform.position.z);

        // Gérer l'inclinaison verticale (mouvement haut/bas)
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
            transform.Rotate(Vector3.up, 200 * Time.deltaTime);
        }

        if (tournerGauche)
        {
            transform.Rotate(Vector3.up, -200 * Time.deltaTime);
        }

        // Appliquer la descente
        if (descendre && transform.position.y > minHeight)
        {
            transform.Translate(Vector3.down * verticalSensitivity * Time.deltaTime);
        }

        // Gestion de la mission
        if (!missionCompleted)
        {
            missionTimeElapsed += Time.deltaTime;

            float distanceToTarget = Vector3.Distance(transform.position, targetPoint.position);
            if (distanceToTarget < 5f)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
                MissionSuccess();
            }
            else if (missionTimeElapsed >= missionTimeLimit)
            {
                MissionFailed();
            }
        }
    }

    private void MissionSuccess()
    {
        missionCompleted = true;
        missionStatusText.text = "Mission réussie !";
        ShowAlert("Mission réussie!", true);

        if (targetRingRenderer != null)
        {
            targetRingRenderer.material.color = Color.green;
        }
    }

    private void MissionFailed()
    {
        missionCompleted = true;
        missionStatusText.text = "Mission échouée !";
        ShowAlert("Mission échouée!", false);

        if (targetRingRenderer != null)
        {
            targetRingRenderer.material.color = Color.red;
        }
    }

    // Afficher l'alerte
    private void ShowAlert(string message, bool isSuccess)
    {
        alertText.text = message;
        alertDialog.SetActive(true);

        quitButton.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(!isSuccess); // Affiche "Restart" seulement si échec

        quitButton.onClick.AddListener(QuitToMenu);
        restartButton.onClick.AddListener(RestartMission);
    }

    // Fonction pour quitter au menu principal
    public void QuitToMenu()
    {
        SceneManager.LoadScene("Scene plane 1");
    }

    // Fonction pour redémarrer la mission
    public void RestartMission()
    {
        SceneManager.LoadScene("scene_runway1");
    }

    public void RetourMenuu()
    {
        // Code pour retourner au menu principal, exemple :
        SceneManager.LoadScene("Scene plane 1");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("TargetRing"))
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            MissionSuccess();
        }
    }
}

