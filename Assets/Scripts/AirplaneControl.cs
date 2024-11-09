//using UnityEngine;
//using UnityEngine.EventSystems;
//using UnityEngine.UI;
//using TMPro;
//using UnityEngine.SceneManagement;

//public class AirplaneControl : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
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

//    private Rigidbody rb;

//    // Variables pour la mission
//    public Transform targetPoint;
//    public float missionTimeLimit = 60f;
//    private float missionTimeElapsed = 0f;
//    private bool missionCompleted = false;

//    // Bo�te de dialogue d'alerte
//    public GameObject alertDialog;
//    public TMP_Text alertText;
//    public Button quitButton;
//    public Button restartButton;

//    public Renderer targetRingRenderer;

//    void Start()
//    {
//        rb = GetComponent<Rigidbody>();

//        // Configurer les �v�nements pour les boutons de contr�le
//        EventTrigger trigger = controlButton.gameObject.AddComponent<EventTrigger>();
//        EventTrigger.Entry pointerDownEntry = new EventTrigger.Entry { eventID = EventTriggerType.PointerDown };
//        pointerDownEntry.callback.AddListener((data) => { OnPointerDown((PointerEventData)data); });
//        trigger.triggers.Add(pointerDownEntry);

//        EventTrigger.Entry pointerUpEntry = new EventTrigger.Entry { eventID = EventTriggerType.PointerUp };
//        pointerUpEntry.callback.AddListener((data) => { OnPointerUp((PointerEventData)data); });
//        trigger.triggers.Add(pointerUpEntry);

//        // Configurer les �v�nements pour les boutons de rotation et descente
//        AddEventTrigger(turnRightButton, EventTriggerType.PointerDown, (data) => { tournerDroite = true; });
//        AddEventTrigger(turnRightButton, EventTriggerType.PointerUp, (data) => { tournerDroite = false; });

//        AddEventTrigger(turnLeftButton, EventTriggerType.PointerDown, (data) => { tournerGauche = true; });
//        AddEventTrigger(turnLeftButton, EventTriggerType.PointerUp, (data) => { tournerGauche = false; });

//        AddEventTrigger(descendButton, EventTriggerType.PointerDown, (data) => { descendre = true; });
//        AddEventTrigger(descendButton, EventTriggerType.PointerUp, (data) => { descendre = false; });

//        // Assigner les �v�nements des boutons Quit et Restart
//        quitButton.onClick.AddListener(QuitToMenu);
//        restartButton.onClick.AddListener(RestartMission);
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

//        float dirX = Input.acceleration.x * sensitivity;
//        transform.Translate(Vector3.right * dirX * Time.deltaTime);

//        float clampedX = Mathf.Clamp(transform.position.x, -850f, 850f);
//        transform.position = new Vector3(clampedX, transform.position.y, transform.position.z);

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

//        if (tournerDroite)
//        {
//            transform.Rotate(Vector3.up, 200 * Time.deltaTime);
//        }

//        if (tournerGauche)
//        {
//            transform.Rotate(Vector3.up, -200 * Time.deltaTime);
//        }

//        if (descendre && transform.position.y > minHeight)
//        {
//            transform.Translate(Vector3.down * verticalSensitivity * Time.deltaTime);
//        }

//        if (!missionCompleted)
//        {
//            missionTimeElapsed += Time.deltaTime;

//            float distanceToTarget = Vector3.Distance(transform.position, targetPoint.position);
//            if (distanceToTarget < 5f)
//            {
//                transform.position = new Vector3(550f, transform.position.y, transform.position.z);
//                MissionSuccess();
//            }
//            else if (missionTimeElapsed >= missionTimeLimit)
//            {
//                MissionFailed();
//            }
//        }
//    }

//    private void ShowAlert(string message, bool isSuccess)
//    {
//        alertText.text = message;
//        alertDialog.SetActive(true);

//        quitButton.gameObject.SetActive(true);
//        restartButton.gameObject.SetActive(!isSuccess); // Le bouton "Restart" n'appara�t qu'en cas d'�chec
//    }

//    private void MissionSuccess()
//    {
//        missionCompleted = true;
//        ShowAlert("Mission r�ussie ", true); // Succ�s : seul le bouton Quit s'affiche
//        if (targetRingRenderer != null)
//        {
//            targetRingRenderer.material.color = Color.green;
//        }
//    }

//    private void MissionFailed()
//    {
//        missionCompleted = true;
//        ShowAlert("Mission �chou�e !", false); // �chec : les boutons Quit et Restart s'affichent
//        if (targetRingRenderer != null)
//        {
//            targetRingRenderer.material.color = Color.red;
//        }
//    }

//    private void OnTriggerEnter(Collider other)
//    {
//        if (other.CompareTag("TargetRing"))
//        {
//            transform.position = new Vector3(750f, transform.position.y, transform.position.z);
//            MissionSuccess();
//        }
//    }

//    public void QuitToMenu()
//    {
//        // Code pour retourner au menu principal, exemple :
//        SceneManager.LoadScene("Scene plane 1");
//    }
//    public void RetourMenu()
//    {
//        // Code pour retourner au menu principal, exemple :
//        SceneManager.LoadScene("Scene plane 1");
//    }

//    public void RestartMission()
//    {
//        SceneManager.LoadScene("scene_runway");
//        //// R�initialiser la mission
//        //missionTimeElapsed = 0;
//        //missionCompleted = false;
//        //alertDialog.SetActive(false);
//        //// R�initialiser d�autres variables si n�cessaire
//    }
//}

using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class AirplaneControl : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
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

    private Rigidbody rb;

    // Variables pour la mission
    public Transform targetPoint;
    public float missionTimeLimit = 60f;
    private float missionTimeElapsed = 0f;
    private bool missionCompleted = false;

    // Bo�te de dialogue d'alerte
    public GameObject alertDialog;
    public TMP_Text alertText;
    public Button quitButton;
    public Button restartButton;

    public Renderer targetRingRenderer;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        // Configurer les �v�nements pour les boutons de contr�le
        EventTrigger trigger = controlButton.gameObject.AddComponent<EventTrigger>();
        EventTrigger.Entry pointerDownEntry = new EventTrigger.Entry { eventID = EventTriggerType.PointerDown };
        pointerDownEntry.callback.AddListener((data) => { OnPointerDown((PointerEventData)data); });
        trigger.triggers.Add(pointerDownEntry);

        EventTrigger.Entry pointerUpEntry = new EventTrigger.Entry { eventID = EventTriggerType.PointerUp };
        pointerUpEntry.callback.AddListener((data) => { OnPointerUp((PointerEventData)data); });
        trigger.triggers.Add(pointerUpEntry);

        // Configurer les �v�nements pour les boutons de rotation et descente
        AddEventTrigger(turnRightButton, EventTriggerType.PointerDown, (data) => { tournerDroite = true; });
        AddEventTrigger(turnRightButton, EventTriggerType.PointerUp, (data) => { tournerDroite = false; });

        AddEventTrigger(turnLeftButton, EventTriggerType.PointerDown, (data) => { tournerGauche = true; });
        AddEventTrigger(turnLeftButton, EventTriggerType.PointerUp, (data) => { tournerGauche = false; });

        AddEventTrigger(descendButton, EventTriggerType.PointerDown, (data) => { descendre = true; });
        AddEventTrigger(descendButton, EventTriggerType.PointerUp, (data) => { descendre = false; });

        // Assigner les �v�nements des boutons Quit et Restart
        quitButton.onClick.AddListener(QuitToMenu);
        restartButton.onClick.AddListener(RestartMission);
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

        // Suppression de l'utilisation de l'acc�l�rom�tre pour le mouvement gauche/droit
        // float dirX = Input.acceleration.x * sensitivity;
        // transform.Translate(Vector3.right * dirX * Time.deltaTime);

        // Rotation verticale (basculement avant/arri�re) avec l'acc�l�rom�tre
        float verticalTilt = Input.acceleration.z;
        verticalTilt = Mathf.Clamp(verticalTilt, -1, 1);
        if (transform.position.y < maxHeight || verticalTilt < 0)
        {
            transform.Rotate(Vector3.right, verticalTilt * verticalSensitivity * Time.deltaTime);
        }

        // Limiter l'angle de rotation de l'avion
        float currentXRotation = transform.eulerAngles.x;
        if (currentXRotation > 180) currentXRotation -= 360;
        currentXRotation = Mathf.Clamp(currentXRotation, -maxPitchAngle, maxPitchAngle);
        transform.rotation = Quaternion.Euler(currentXRotation, transform.eulerAngles.y, transform.eulerAngles.z);

        // Gestion de la rotation avec les boutons de droite et gauche
        if (tournerDroite)
        {
            transform.Rotate(Vector3.up, 200 * Time.deltaTime);
        }

        if (tournerGauche)
        {
            transform.Rotate(Vector3.up, -200 * Time.deltaTime);
        }

        // Gestion de la descente
        if (descendre && transform.position.y > minHeight)
        {
            transform.Translate(Vector3.down * verticalSensitivity * Time.deltaTime);
        }

        // Logique de la mission
        if (!missionCompleted)
        {
            missionTimeElapsed += Time.deltaTime;

            float distanceToTarget = Vector3.Distance(transform.position, targetPoint.position);
            if (distanceToTarget < 5f)
            {
                transform.position = new Vector3(550f, transform.position.y, transform.position.z);
                MissionSuccess();
            }
            else if (missionTimeElapsed >= missionTimeLimit)
            {
                MissionFailed();
            }
        }
    }

    private void ShowAlert(string message, bool isSuccess)
    {
        alertText.text = message;
        alertDialog.SetActive(true);

        quitButton.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(!isSuccess); // Le bouton "Restart" n'appara�t qu'en cas d'�chec
    }

    private void MissionSuccess()
    {
        missionCompleted = true;
        ShowAlert("Mission r�ussie ", true); // Succ�s : seul le bouton Quit s'affiche
        if (targetRingRenderer != null)
        {
            targetRingRenderer.material.color = Color.green;
        }
    }

    private void MissionFailed()
    {
        missionCompleted = true;
        ShowAlert("Mission �chou�e !", false); // �chec : les boutons Quit et Restart s'affichent
        if (targetRingRenderer != null)
        {
            targetRingRenderer.material.color = Color.red;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("TargetRing"))
        {
            transform.position = new Vector3(750f, transform.position.y, transform.position.z);
            MissionSuccess();
        }
    }

    public void QuitToMenu()
    {
        // Code pour retourner au menu principal, exemple :
        SceneManager.LoadScene("Scene plane 1");
    }

    public void RestartMission()
    {
        SceneManager.LoadScene("scene_runway");
    }
    public void RetourMenuu()
    {
        // Code pour retourner au menu principal, exemple :
        SceneManager.LoadScene("Scene plane 1");
    }
}
