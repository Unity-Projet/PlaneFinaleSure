using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Android;

public class LightSensorManager : MonoBehaviour
{
    public Text caracteristiquesText; // Référence au composant Text dans Unity
    public Text caracteristiquesTextMoteurs; // Référence au composant Text pour les moteurs
    public Text caracteristiquesTextAiles; // Référence au composant Text pour les ailes

    private const float SEUIL_LUMIERE_ECLAIREE = 3.0f; // Seuil pour déterminer si l'environnement est éclairé
    private bool isBright = false; // État précédent : vrai = éclairé, faux = sombre

    private AndroidJavaObject lightSensor; // Pour le capteur de lumière
    private AndroidJavaObject sensorManager; // Pour gérer les capteurs
    private AndroidJavaObject activity; // Référence à l'activité Unity
    private SensorEventListener listener; // Référence à l'écouteur

    // Classe pour écouter les événements du capteur
    private class SensorEventListener : AndroidJavaProxy
    {
        private LightSensorManager lightSensorManager;

        public SensorEventListener(LightSensorManager manager) : base("android.hardware.SensorEventListener")
        {
            lightSensorManager = manager;
        }

        // Méthode appelée quand la luminosité change
        public void onSensorChanged(AndroidJavaObject sensorEvent)
        {
            float luminosity = sensorEvent.Get<float[]>("values")[0]; // Obtenir la valeur de luminosité
            Debug.Log("Niveau de lumière détecté : " + luminosity);
            lightSensorManager.UpdateLightStatus(luminosity); // Mettre à jour l'état de la lumière
        }

        public void onAccuracyChanged(AndroidJavaObject sensor, int accuracy) { }
    }

    void Start()
    {
        // Initialiser le capteur de lumière quand le jeu démarre
        RequestPermissions();
        InitializeLightSensor();
    }

    private void InitializeLightSensor()
    {
        // Obtenir l'instance de l'activité Android
        using (var unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
        {
            activity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
            var context = activity.Call<AndroidJavaObject>("getApplicationContext");
            sensorManager = context.Call<AndroidJavaObject>("getSystemService", "sensor");
            lightSensor = sensorManager.Call<AndroidJavaObject>("getDefaultSensor", 5); // 5 = TYPE_LIGHT

            if (lightSensor == null)
            {
                Debug.LogError("Capteur de lumière non trouvé !");
                return;
            }

            Debug.Log("Capteur de lumière initialisé avec succès.");

            // Créer et enregistrer le listener
            listener = new SensorEventListener(this);
            activity.Call("runOnUiThread", new AndroidJavaRunnable(() =>
            {
                sensorManager.Call<bool>("registerListener", listener, lightSensor, 3); // 3 pour SENSOR_DELAY_NORMAL
            }));
        }
    }

    // Méthode pour mettre à jour l'état de la lumière
    public void UpdateLightStatus(float luminosity)
    {
        Debug.Log("Luminosité actuelle : " + luminosity);

        if (luminosity > SEUIL_LUMIERE_ECLAIREE && !isBright)
        {
            caracteristiquesText.color = Color.black; // Change le texte en noir
            caracteristiquesTextMoteurs.color = Color.black; // Change le texte en noir
            caracteristiquesTextAiles.color = Color.black; // Change le texte en noir
            isBright = true;
            Debug.Log("Chambre éclairée : texte noir");
        }
        else if (luminosity <= SEUIL_LUMIERE_ECLAIREE && isBright)
        {
            caracteristiquesText.color = Color.red; // Change le texte en rouge
            caracteristiquesTextMoteurs.color = Color.red; // Change le texte en rouge
            caracteristiquesTextAiles.color = Color.red; // Change le texte en rouge
            isBright = false;
            Debug.Log("Chambre sombre : texte rouge");
        }
        else
        {
            Debug.Log("Aucune mise à jour de couleur nécessaire.");
        }
    }

    private void OnDestroy()
    {
        // Désenregistrer le listener lorsque l'objet est détruit
        if (sensorManager != null && lightSensor != null && listener != null)
        {
            activity.Call("runOnUiThread", new AndroidJavaRunnable(() =>
            {
                sensorManager.Call("unregisterListener", listener); // Désenregistre le listener
                Debug.Log("Listener désenregistré avec succès.");
            }));
        }
    }

    private void RequestPermissions()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            if (!Permission.HasUserAuthorizedPermission(Permission.FineLocation))
            {
                Permission.RequestUserPermission(Permission.FineLocation);
            }

            if (!Permission.HasUserAuthorizedPermission(Permission.Camera))
            {
                Permission.RequestUserPermission(Permission.Camera);
            }

            if (!Permission.HasUserAuthorizedPermission(Permission.ExternalStorageRead))
            {
                Permission.RequestUserPermission(Permission.ExternalStorageRead);
            }

            if (!Permission.HasUserAuthorizedPermission(Permission.ExternalStorageWrite))
            {
                Permission.RequestUserPermission(Permission.ExternalStorageWrite);
            }
        }
    }
}
