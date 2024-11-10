using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Android;

public class LightSensorManager : MonoBehaviour
{
    public Text caracteristiquesText; // R�f�rence au composant Text dans Unity
    public Text caracteristiquesTextMoteurs; // R�f�rence au composant Text pour les moteurs
    public Text caracteristiquesTextAiles; // R�f�rence au composant Text pour les ailes

    private const float SEUIL_LUMIERE_ECLAIREE = 3.0f; // Seuil pour d�terminer si l'environnement est �clair�
    private bool isBright = false; // �tat pr�c�dent : vrai = �clair�, faux = sombre

    private AndroidJavaObject lightSensor; // Pour le capteur de lumi�re
    private AndroidJavaObject sensorManager; // Pour g�rer les capteurs
    private AndroidJavaObject activity; // R�f�rence � l'activit� Unity
    private SensorEventListener listener; // R�f�rence � l'�couteur

    // Classe pour �couter les �v�nements du capteur
    private class SensorEventListener : AndroidJavaProxy
    {
        private LightSensorManager lightSensorManager;

        public SensorEventListener(LightSensorManager manager) : base("android.hardware.SensorEventListener")
        {
            lightSensorManager = manager;
        }

        // M�thode appel�e quand la luminosit� change
        public void onSensorChanged(AndroidJavaObject sensorEvent)
        {
            float luminosity = sensorEvent.Get<float[]>("values")[0]; // Obtenir la valeur de luminosit�
            Debug.Log("Niveau de lumi�re d�tect� : " + luminosity);
            lightSensorManager.UpdateLightStatus(luminosity); // Mettre � jour l'�tat de la lumi�re
        }

        public void onAccuracyChanged(AndroidJavaObject sensor, int accuracy) { }
    }

    void Start()
    {
        // Initialiser le capteur de lumi�re quand le jeu d�marre
        RequestPermissions();
        InitializeLightSensor();
    }

    private void InitializeLightSensor()
    {
        // Obtenir l'instance de l'activit� Android
        using (var unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
        {
            activity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
            var context = activity.Call<AndroidJavaObject>("getApplicationContext");
            sensorManager = context.Call<AndroidJavaObject>("getSystemService", "sensor");
            lightSensor = sensorManager.Call<AndroidJavaObject>("getDefaultSensor", 5); // 5 = TYPE_LIGHT

            if (lightSensor == null)
            {
                Debug.LogError("Capteur de lumi�re non trouv� !");
                return;
            }

            Debug.Log("Capteur de lumi�re initialis� avec succ�s.");

            // Cr�er et enregistrer le listener
            listener = new SensorEventListener(this);
            activity.Call("runOnUiThread", new AndroidJavaRunnable(() =>
            {
                sensorManager.Call<bool>("registerListener", listener, lightSensor, 3); // 3 pour SENSOR_DELAY_NORMAL
            }));
        }
    }

    // M�thode pour mettre � jour l'�tat de la lumi�re
    public void UpdateLightStatus(float luminosity)
    {
        Debug.Log("Luminosit� actuelle : " + luminosity);

        if (luminosity > SEUIL_LUMIERE_ECLAIREE && !isBright)
        {
            caracteristiquesText.color = Color.black; // Change le texte en noir
            caracteristiquesTextMoteurs.color = Color.black; // Change le texte en noir
            caracteristiquesTextAiles.color = Color.black; // Change le texte en noir
            isBright = true;
            Debug.Log("Chambre �clair�e : texte noir");
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
            Debug.Log("Aucune mise � jour de couleur n�cessaire.");
        }
    }

    private void OnDestroy()
    {
        // D�senregistrer le listener lorsque l'objet est d�truit
        if (sensorManager != null && lightSensor != null && listener != null)
        {
            activity.Call("runOnUiThread", new AndroidJavaRunnable(() =>
            {
                sensorManager.Call("unregisterListener", listener); // D�senregistre le listener
                Debug.Log("Listener d�senregistr� avec succ�s.");
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
