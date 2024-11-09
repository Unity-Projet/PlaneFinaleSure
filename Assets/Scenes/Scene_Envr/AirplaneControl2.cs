//using UnityEngine;
//using UnityEngine.EventSystems;
//using UnityEngine.UI;
//using TMPro;
//using UnityEngine.SceneManagement;

//public class AirplaneControl2 : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
//{
//    // Variables de vitesse et de contr�le de l'avion
//    public float vitesse = 20f; // Vitesse maximale de l'avion
//    private float currentSpeed = 0f; // Vitesse actuelle
//    private bool avancer = false; // D�termine si l'avion doit avancer
//    private bool tournerDroite = false; // D�termine si l'avion doit tourner � droite
//    private bool tournerGauche = false; // D�termine si l'avion doit tourner � gauche
//    private bool descendre = false; // D�termine si l'avion doit descendre

//    // Sensibilit� des contr�les
//    public float sensitivity = 15.0f; // Sensibilit� pour les mouvements horizontaux
//    public float verticalSensitivity = 7.5f; // Sensibilit� pour les mouvements verticaux

//    // Limites de mouvement de l'avion
//    public float maxTiltAngle = 45f; // Angle maximal de basculement (tilt)
//    public float maxPitchAngle = 30f; // Angle maximal de pitch (inclinaison avant/arri�re)
//    public float maxHeight = 10f; // Hauteur maximale
//    public float minHeight = 0f; // Hauteur minimale

//    // UI des boutons de contr�le
//    public Button controlButton; // Bouton pour avancer
//    public Button turnRightButton; // Bouton pour tourner � droite
//    public Button turnLeftButton; // Bouton pour tourner � gauche
//    public Button descendButton; // Bouton pour descendre
//    public TMP_Text speedText; // Affichage de la vitesse

//    // R�f�rence � Rigidbody pour les mouvements physiques
//    private Rigidbody rb;

//    // Bo�te de dialogue d'alerte
//    public GameObject alertDialog; // Dialogue d'alerte
//    public TMP_Text alertText; // Texte de l'alerte
//    public Button quitButton; // Bouton pour quitter
//    public Button restartButton; // Bouton pour red�marrer la mission

//    // R�f�rence � un obstacle
//    public Renderer obstacleRenderer;

//    void Start()
//    {
//        rb = GetComponent<Rigidbody>();

//        // Ajouter des �v�nements aux boutons de contr�le
//        AddEventTrigger(controlButton, EventTriggerType.PointerDown, (data) => { avancer = true; });
//        AddEventTrigger(controlButton, EventTriggerType.PointerUp, (data) => { avancer = false; });

//        AddEventTrigger(turnRightButton, EventTriggerType.PointerDown, (data) => { tournerDroite = true; });
//        AddEventTrigger(turnRightButton, EventTriggerType.PointerUp, (data) => { tournerDroite = false; });

//        AddEventTrigger(turnLeftButton, EventTriggerType.PointerDown, (data) => { tournerGauche = true; });
//        AddEventTrigger(turnLeftButton, EventTriggerType.PointerUp, (data) => { tournerGauche = false; });

//        AddEventTrigger(descendButton, EventTriggerType.PointerDown, (data) => { descendre = true; });
//        AddEventTrigger(descendButton, EventTriggerType.PointerUp, (data) => { descendre = false; });

//        // �v�nements pour les boutons Quit et Restart
//        quitButton.onClick.AddListener(QuitToMenu);
//        restartButton.onClick.AddListener(RestartMission);
//    }

//    // Ajouter un �v�nement au bouton
//    void AddEventTrigger(Button button, EventTriggerType eventType, UnityEngine.Events.UnityAction<BaseEventData> action)
//    {
//        EventTrigger trigger = button.gameObject.GetComponent<EventTrigger>() ?? button.gameObject.AddComponent<EventTrigger>();
//        EventTrigger.Entry entry = new EventTrigger.Entry { eventID = eventType };
//        entry.callback.AddListener(action);
//        trigger.triggers.Add(entry);
//    }

//    // Impl�mentation des interfaces IPointerDownHandler et IPointerUpHandler
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
//        // Logique de d�placement de l'avion
//        if (avancer)
//        {
//            currentSpeed = Mathf.Lerp(currentSpeed, vitesse, Time.deltaTime * 2);
//        }
//        else
//        {
//            currentSpeed = Mathf.Lerp(currentSpeed, 0, Time.deltaTime * 2);
//        }

//        // D�placer l'avion en fonction de la vitesse
//        transform.Translate(Vector3.forward * currentSpeed * Time.deltaTime);
//        speedText.text = currentSpeed.ToString("F2");

//        // Mouvement horizontal bas� sur l'acc�l�rom�tre
//        float dirX = Input.acceleration.x * sensitivity;
//        transform.Translate(Vector3.right * dirX * Time.deltaTime);

//        // Limiter le mouvement horizontal
//        float clampedX = Mathf.Clamp(transform.position.x, -850f, 850f);
//        transform.position = new Vector3(clampedX, transform.position.y, transform.position.z);

//        // Rotation verticale (basculement avant/arri�re)
//        float verticalTilt = Input.acceleration.z;
//        verticalTilt = Mathf.Clamp(verticalTilt, -1, 1);
//        if (transform.position.y < maxHeight || verticalTilt < 0)
//        {
//            transform.Rotate(Vector3.right, verticalTilt * verticalSensitivity * Time.deltaTime);
//        }

//        // Limiter l'angle de pitch (inclinaison avant/arri�re)
//        float currentXRotation = transform.eulerAngles.x;
//        if (currentXRotation > 180) currentXRotation -= 360;
//        currentXRotation = Mathf.Clamp(currentXRotation, -maxPitchAngle, maxPitchAngle);
//        transform.rotation = Quaternion.Euler(currentXRotation, transform.eulerAngles.y, transform.eulerAngles.z);

//        // Tourner � droite ou � gauche
//        if (tournerDroite)
//        {
//            transform.Rotate(Vector3.up, 200 * Time.deltaTime);
//        }

//        if (tournerGauche)
//        {
//            transform.Rotate(Vector3.up, -200 * Time.deltaTime);
//        }

//        // Descendre l'avion
//        if (descendre && transform.position.y > minHeight)
//        {
//            transform.Translate(Vector3.down * verticalSensitivity * Time.deltaTime);
//        }
//    }

//    // Afficher un message d'alerte
//    private void ShowAlert(string message, bool isSuccess)
//    {
//        alertText.text = message;
//        alertDialog.SetActive(true);

//        quitButton.gameObject.SetActive(true);
//        restartButton.gameObject.SetActive(!isSuccess);
//    }

//    // Gestion de la mission �chou�e
//    private void MissionFailed()
//    {
//        ShowAlert("Mission �chou�e !", false);
//        if (obstacleRenderer != null)
//        {
//            obstacleRenderer.material.color = Color.red;
//        }
//    }

//    // D�tection de la collision avec un obstacle
//    private void OnTriggerEnter(Collider other)
//    {
//        if (other.CompareTag("Obstacle"))
//        {
//            // Arr�ter l'avion
//            currentSpeed = 0f;
//            avancer = false;

//            // Mission �chou�e
//            MissionFailed();
//        }
//    }

//    // Retourner au menu principal
//    public void QuitToMenu()
//    {
//        SceneManager.LoadScene("Scene plane 1");
//    }

//    // Red�marrer la mission
//    public void RestartMission()
//    {
//        SceneManager.LoadScene("scene_runway 2");
//    }
//}

//using UnityEngine;
//using UnityEngine.EventSystems;
//using UnityEngine.UI;
//using TMPro;
//using UnityEngine.SceneManagement;

//public class AirplaneControl2 : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
//{
//    // Variables de vitesse et de contr�le de l'avion
//    public float vitesse = 20f; // Vitesse maximale de l'avion
//    private float currentSpeed = 0f; // Vitesse actuelle
//    private bool avancer = false; // D�termine si l'avion doit avancer
//    private bool tournerDroite = false; // D�termine si l'avion doit tourner � droite
//    private bool tournerGauche = false; // D�termine si l'avion doit tourner � gauche
//    private bool descendre = false; // D�termine si l'avion doit descendre

//    // Sensibilit� des contr�les
//    public float sensitivity = 15.0f; // Sensibilit� pour les mouvements horizontaux
//    public float verticalSensitivity = 7.5f; // Sensibilit� pour les mouvements verticaux

//    // Limites de mouvement de l'avion
//    public float maxTiltAngle = 45f; // Angle maximal de basculement (tilt)
//    public float maxPitchAngle = 30f; // Angle maximal de pitch (inclinaison avant/arri�re)
//    public float maxHeight = 10f; // Hauteur maximale
//    public float minHeight = 0f; // Hauteur minimale

//    // UI des boutons de contr�le
//    public Button controlButton; // Bouton pour avancer
//    public Button turnRightButton; // Bouton pour tourner � droite
//    public Button turnLeftButton; // Bouton pour tourner � gauche
//    public Button descendButton; // Bouton pour descendre
//    public TMP_Text speedText; // Affichage de la vitesse

//    // R�f�rence � Rigidbody pour les mouvements physiques
//    private Rigidbody rb;

//    // Bo�te de dialogue d'alerte
//    public GameObject alertDialog; // Dialogue d'alerte
//    public TMP_Text alertText; // Texte de l'alerte
//    public Button quitButton; // Bouton pour quitter
//    public Button restartButton; // Bouton pour red�marrer la mission

//    // R�f�rences aux obstacles
//    public Renderer[] obstacleRenderers; // Tableau de rendus pour les obstacles
//    private int numberOfObstacles = 3; // Nombre d'obstacles

//    void Start()
//    {
//        rb = GetComponent<Rigidbody>();

//        // Ajouter des �v�nements aux boutons de contr�le
//        AddEventTrigger(controlButton, EventTriggerType.PointerDown, (data) => { avancer = true; });
//        AddEventTrigger(controlButton, EventTriggerType.PointerUp, (data) => { avancer = false; });

//        AddEventTrigger(turnRightButton, EventTriggerType.PointerDown, (data) => { tournerDroite = true; });
//        AddEventTrigger(turnRightButton, EventTriggerType.PointerUp, (data) => { tournerDroite = false; });

//        AddEventTrigger(turnLeftButton, EventTriggerType.PointerDown, (data) => { tournerGauche = true; });
//        AddEventTrigger(turnLeftButton, EventTriggerType.PointerUp, (data) => { tournerGauche = false; });

//        AddEventTrigger(descendButton, EventTriggerType.PointerDown, (data) => { descendre = true; });
//        AddEventTrigger(descendButton, EventTriggerType.PointerUp, (data) => { descendre = false; });

//        // �v�nements pour les boutons Quit et Restart
//        quitButton.onClick.AddListener(QuitToMenu);
//        restartButton.onClick.AddListener(RestartMission);
//    }

//    // Ajouter un �v�nement au bouton
//    void AddEventTrigger(Button button, EventTriggerType eventType, UnityEngine.Events.UnityAction<BaseEventData> action)
//    {
//        EventTrigger trigger = button.gameObject.GetComponent<EventTrigger>() ?? button.gameObject.AddComponent<EventTrigger>();
//        EventTrigger.Entry entry = new EventTrigger.Entry { eventID = eventType };
//        entry.callback.AddListener(action);
//        trigger.triggers.Add(entry);
//    }

//    // Impl�mentation des interfaces IPointerDownHandler et IPointerUpHandler
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
//        // Logique de d�placement de l'avion
//        if (avancer)
//        {
//            currentSpeed = Mathf.Lerp(currentSpeed, vitesse, Time.deltaTime * 2);
//        }
//        else
//        {
//            currentSpeed = Mathf.Lerp(currentSpeed, 0, Time.deltaTime * 2);
//        }

//        // D�placer l'avion en fonction de la vitesse
//        transform.Translate(Vector3.forward * currentSpeed * Time.deltaTime);
//        speedText.text = currentSpeed.ToString("F2");

//        // Mouvement horizontal bas� sur l'acc�l�rom�tre
//        float dirX = Input.acceleration.x * sensitivity;
//        transform.Translate(Vector3.right * dirX * Time.deltaTime);

//        // Limiter le mouvement horizontal
//        float clampedX = Mathf.Clamp(transform.position.x, -850f, 850f);
//        transform.position = new Vector3(clampedX, transform.position.y, transform.position.z);

//        // Rotation verticale (basculement avant/arri�re)
//        float verticalTilt = Input.acceleration.z;
//        verticalTilt = Mathf.Clamp(verticalTilt, -1, 1);
//        if (transform.position.y < maxHeight || verticalTilt < 0)
//        {
//            transform.Rotate(Vector3.right, verticalTilt * verticalSensitivity * Time.deltaTime);
//        }

//        // Limiter l'angle de pitch (inclinaison avant/arri�re)
//        float currentXRotation = transform.eulerAngles.x;
//        if (currentXRotation > 180) currentXRotation -= 360;
//        currentXRotation = Mathf.Clamp(currentXRotation, -maxPitchAngle, maxPitchAngle);
//        transform.rotation = Quaternion.Euler(currentXRotation, transform.eulerAngles.y, transform.eulerAngles.z);

//        // Tourner � droite ou � gauche
//        if (tournerDroite)
//        {
//            transform.Rotate(Vector3.up, 200 * Time.deltaTime);
//        }

//        if (tournerGauche)
//        {
//            transform.Rotate(Vector3.up, -200 * Time.deltaTime);
//        }

//        // Descendre l'avion
//        if (descendre && transform.position.y > minHeight)
//        {
//            transform.Translate(Vector3.down * verticalSensitivity * Time.deltaTime);
//        }
//    }

//    // Afficher un message d'alerte
//    private void ShowAlert(string message, bool isSuccess)
//    {
//        alertText.text = message;
//        alertDialog.SetActive(true);

//        quitButton.gameObject.SetActive(true);
//        restartButton.gameObject.SetActive(!isSuccess);
//    }

//    // Gestion de la mission �chou�e
//    private void MissionFailed()
//    {
//        ShowAlert("Mission �chou�e !", false);
//        foreach (var obstacle in obstacleRenderers)
//        {
//            if (obstacle != null)
//            {
//                obstacle.material.color = Color.black; // Change la couleur de tous les obstacles � rouge
//            }
//        }
//    }

//    // D�tection de la collision avec un obstacle
//    private void OnTriggerEnter(Collider other)
//    {
//        if (other.CompareTag("Obstacle"))
//        {
//            // Arr�ter l'avion
//            currentSpeed = 0f;
//            avancer = false;

//            // Mission �chou�e
//            MissionFailed();
//        }
//    }

//    // Retourner au menu principal
//    public void QuitToMenu()
//    {
//        SceneManager.LoadScene("Scene plane 1");
//    }

//    // Red�marrer la mission
//    public void RestartMission()
//    {
//        SceneManager.LoadScene("scene_runway 2");
//    }
//}
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class AirplaneControl2 : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    // Variables de vitesse et de contr�le de l'avion
    public float vitesse = 20f;
    private float currentSpeed = 0f;
    private bool avancer = false;
    private bool tournerDroite = false;
    private bool tournerGauche = false;
    private bool descendre = false;

    // Sensibilit� des contr�les
    public float sensitivity = 15.0f;
    public float verticalSensitivity = 7.5f;

    // Limites de mouvement de l'avion
    public float maxTiltAngle = 45f;
    public float maxPitchAngle = 30f;
    public float maxHeight = 10f;
    public float minHeight = 0f;

    // UI des boutons de contr�le
    public Button controlButton;
    public Button turnRightButton;
    public Button turnLeftButton;
    public Button descendButton;
    public TMP_Text speedText;

    // R�f�rence � Rigidbody pour les mouvements physiques
    private Rigidbody rb;

    // Bo�te de dialogue d'alerte
    public GameObject alertDialog;
    public TMP_Text alertText;
    public Button quitButton;
    public Button restartButton;

    // R�f�rences aux obstacles
    public Renderer[] obstacleRenderers;
    private int numberOfObstacles = 3;

    // Variable pour g�rer le temps depuis le dernier obstacle
    private float timeSinceLastObstacle = 0f;
    private float timeToShowAlert = 50f; // Temps pour afficher l'alerte

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        // Ajouter des �v�nements aux boutons de contr�le
        AddEventTrigger(controlButton, EventTriggerType.PointerDown, (data) => { avancer = true; });
        AddEventTrigger(controlButton, EventTriggerType.PointerUp, (data) => { avancer = false; });

        AddEventTrigger(turnRightButton, EventTriggerType.PointerDown, (data) => { tournerDroite = true; });
        AddEventTrigger(turnRightButton, EventTriggerType.PointerUp, (data) => { tournerDroite = false; });

        AddEventTrigger(turnLeftButton, EventTriggerType.PointerDown, (data) => { tournerGauche = true; });
        AddEventTrigger(turnLeftButton, EventTriggerType.PointerUp, (data) => { tournerGauche = false; });

        AddEventTrigger(descendButton, EventTriggerType.PointerDown, (data) => { descendre = true; });
        AddEventTrigger(descendButton, EventTriggerType.PointerUp, (data) => { descendre = false; });

        // �v�nements pour les boutons Quit et Restart
        quitButton.onClick.AddListener(QuitToMenu);
        restartButton.onClick.AddListener(RestartMission);
    }

    // Ajouter un �v�nement au bouton
    void AddEventTrigger(Button button, EventTriggerType eventType, UnityEngine.Events.UnityAction<BaseEventData> action)
    {
        EventTrigger trigger = button.gameObject.GetComponent<EventTrigger>() ?? button.gameObject.AddComponent<EventTrigger>();
        EventTrigger.Entry entry = new EventTrigger.Entry { eventID = eventType };
        entry.callback.AddListener(action);
        trigger.triggers.Add(entry);
    }

    // Impl�mentation des interfaces IPointerDownHandler et IPointerUpHandler
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
        // Logique de d�placement de l'avion
        if (avancer)
        {
            currentSpeed = Mathf.Lerp(currentSpeed, vitesse, Time.deltaTime * 2);
        }
        else
        {
            currentSpeed = Mathf.Lerp(currentSpeed, 0, Time.deltaTime * 2);
        }

        // D�placer l'avion en fonction de la vitesse
        transform.Translate(Vector3.forward * currentSpeed * Time.deltaTime);
        speedText.text = currentSpeed.ToString("F2");

        // Mouvement horizontal bas� sur l'acc�l�rom�tre
        float dirX = Input.acceleration.x * sensitivity;
        transform.Translate(Vector3.right * dirX * Time.deltaTime);

        // Rotation verticale (basculement avant/arri�re)
        float verticalTilt = Input.acceleration.z;
        verticalTilt = Mathf.Clamp(verticalTilt, -1, 1);
        if (transform.position.y < maxHeight || verticalTilt < 0)
        {
            transform.Rotate(Vector3.right, verticalTilt * verticalSensitivity * Time.deltaTime);
        }

        // Limiter l'angle de pitch (inclinaison avant/arri�re)
        float currentXRotation = transform.eulerAngles.x;
        if (currentXRotation > 180) currentXRotation -= 360;
        currentXRotation = Mathf.Clamp(currentXRotation, -maxPitchAngle, maxPitchAngle);
        transform.rotation = Quaternion.Euler(currentXRotation, transform.eulerAngles.y, transform.eulerAngles.z);

        // Tourner � droite ou � gauche
        if (tournerDroite)
        {
            transform.Rotate(Vector3.up, 200 * Time.deltaTime);
        }

        if (tournerGauche)
        {
            transform.Rotate(Vector3.up, -200 * Time.deltaTime);
        }

        // Descendre l'avion
        if (descendre && transform.position.y > minHeight)
        {
            transform.Translate(Vector3.down * verticalSensitivity * Time.deltaTime);
        }

        // Mettre � jour le temps depuis le dernier obstacle
        timeSinceLastObstacle += Time.deltaTime;

        // V�rifier si 50 secondes se sont �coul�es sans collision avec un obstacle
        if (timeSinceLastObstacle >= timeToShowAlert)
        {
            //ShowAlert("Succ�s ! Vous avez �vit� tous les obstacles ", true);
            ShowAlert(" Succ�s ", true);
        }
    }

    // Afficher un message d'alerte
    private void ShowAlert(string message, bool isSuccess)
    {
        alertText.text = message;
        alertDialog.SetActive(true);

        quitButton.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(!isSuccess);
    }

    // D�tection de la collision avec un obstacle
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            // Arr�ter l'avion
            currentSpeed = 0f;
            avancer = false;

            // Mission �chou�e
            MissionFailed();

            // R�initialiser le compteur de temps
            timeSinceLastObstacle = 0f;
        }
    }

    // Gestion de la mission �chou�e
    private void MissionFailed()
    {
        ShowAlert("Mission �chou�e !", false);
        if (obstacleRenderers != null)
        {
            foreach (var renderer in obstacleRenderers)
            {
                renderer.material.color = Color.red;
            }
        }
    }

    // Retourner au menu principal
    public void QuitToMenu()
    {
        SceneManager.LoadScene("Scene plane 1");
    }

    // Red�marrer la mission
    public void RestartMission()
    {
        SceneManager.LoadScene("scene_runway 2");
    }
}

