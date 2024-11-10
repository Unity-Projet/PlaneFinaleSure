using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TrainAtterrissage
{
    public string Nom { get; set; }
    public float Poids { get; set; }
    public float Resistance { get; set; }
    public string TypeDeFrein { get; set; }

    public TrainAtterrissage(string nom, float poids, float resistance, string typeDeFrein)
    {
        Nom = nom;
        Poids = poids;
        Resistance = resistance;
        TypeDeFrein = typeDeFrein;
    }

    public string GetCaracteristiques()
    {
        return $"{Nom}:\n\n" +
               $"Poids: {Poids} kg\n" +
               $"Résistance: {Resistance} kg\n" +
               $"Type de frein: {TypeDeFrein}";
    }
}

public class TrainAtterrissageScript : MonoBehaviour
{
    public GameObject panelCaracteristiquesTrainAtterrissage;
    public GameObject panelInfoTrainAtterrissage;
    public Text caracteristiquesText;

    public Button trainAtterrissageButton;
    public Button mainLandingGearButton;
    public Button noseLandingGearButton;
    public Button steerableLandingGearButton;

    public Button fermerPanelInfoTrainAtterrissageButton;
    public Button fermerPanelCaracteristiquesTrainAtterrissageButton;
    public Button retourButton;

    // Ajout des éléments pour l'alerte
    public GameObject alertPanel;
    public Text alertText;
    public Button closeAlertButton;

    // Ajout du bouton "Acheter"
    public Button acheterButton;

    private List<TrainAtterrissage> trainsAtterrissage;

    void Start()
    {
        // Initialisation des trains d'atterrissage
        trainsAtterrissage = new List<TrainAtterrissage>
        {
            new TrainAtterrissage("Main Landing Gear", 1200f, 2500f, "Hydraulique"),
            new TrainAtterrissage("Nose Landing Gear", 800f, 1500f, "Mécanique"),
            new TrainAtterrissage("Steerable Landing Gear", 950f, 1800f, "Électrique")
        };

        // Assignation des listeners aux boutons
        trainAtterrissageButton.onClick.AddListener(ShowTrainAtterrissagePanel);
        mainLandingGearButton.onClick.AddListener(() => ShowCaracteristiques(0));
        noseLandingGearButton.onClick.AddListener(() => ShowCaracteristiques(1));
        steerableLandingGearButton.onClick.AddListener(() => ShowCaracteristiques(2));

        fermerPanelInfoTrainAtterrissageButton.onClick.AddListener(ClosePanelInfoTrainAtterrissage);
        fermerPanelCaracteristiquesTrainAtterrissageButton.onClick.AddListener(ClosePanelCaracteristiquesTrainAtterrissage);
        retourButton.onClick.AddListener(ReturnToPlaneScene);

        // Ajout du listener pour la fermeture de l'alerte
        closeAlertButton.onClick.AddListener(CloseAlert);

        // Ajout du listener pour le bouton "Acheter"
        acheterButton.onClick.AddListener(OpenAlertForAcheter);
    }

    // Méthode pour afficher le panneau des caractéristiques
    void ShowTrainAtterrissagePanel()
    {
        panelCaracteristiquesTrainAtterrissage.SetActive(true);
    }

    // Méthode pour afficher les caractéristiques du train d'atterrissage sélectionné
    void ShowCaracteristiques(int index)
    {
        caracteristiquesText.text = trainsAtterrissage[index].GetCaracteristiques();
        panelInfoTrainAtterrissage.SetActive(true);
    }

    // Méthode pour fermer le panneau des caractéristiques
    void ClosePanelInfoTrainAtterrissage()
    {
        panelInfoTrainAtterrissage.SetActive(false);
    }

    // Méthode pour fermer le panneau principal des trains d'atterrissage
    void ClosePanelCaracteristiquesTrainAtterrissage()
    {
        panelCaracteristiquesTrainAtterrissage.SetActive(false);
    }

    // Méthode pour retourner à la scène principale
    void ReturnToPlaneScene()
    {
        SceneManager.LoadScene("Scene Plane 1");
    }

    // Méthode pour afficher une alerte
    public void ShowAlert(string message)
    {
        alertPanel.SetActive(true);  // Affiche le panneau d'alerte
        alertText.text = message;    // Affiche le message dans l'alerte
    }

    // Méthode pour fermer l'alerte
    public void CloseAlert()
    {
        alertPanel.SetActive(false); // Cache le panneau d'alerte
    }

    // Méthode pour ouvrir l'alerte lorsque l'utilisateur clique sur "Acheter"
    public void OpenAlertForAcheter()
    {
        ShowAlert("Vous n'avez pas assez d'argent pour acheter");
    }
}

//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;
//using UnityEngine.SceneManagement;

//public class TrainAtterrissage
//{
//    public string Nom { get; set; }
//    public float Poids { get; set; }
//    public float Resistance { get; set; }
//    public string TypeDeFrein { get; set; }

//    public TrainAtterrissage(string nom, float poids, float resistance, string typeDeFrein)
//    {
//        Nom = nom;
//        Poids = poids;
//        Resistance = resistance;
//        TypeDeFrein = typeDeFrein;
//    }

//    public string GetCaracteristiques()
//    {
//        return $"{Nom}:\n\n" +
//               $"Poids: {Poids} kg\n" +
//               $"Résistance: {Resistance} kg\n" +
//               $"Type de frein: {TypeDeFrein}";
//    }
//}

//public class TrainAtterrissageScript : MonoBehaviour
//{
//    public GameObject panelCaracteristiquesTrainAtterrissage;
//    public GameObject panelInfoTrainAtterrissage;
//    public Text caracteristiquesText;

//    public Button trainAtterrissageButton;
//    public Button mainLandingGearButton;
//    public Button noseLandingGearButton;
//    public Button steerableLandingGearButton;

//    public Button fermerPanelInfoTrainAtterrissageButton;
//    public Button fermerPanelCaracteristiquesTrainAtterrissageButton;
//    public Button retourButton;

//    private List<TrainAtterrissage> trainsAtterrissage;

//    void Start()
//    {
//        // Initialisation des trains d'atterrissage
//        trainsAtterrissage = new List<TrainAtterrissage>
//        {
//            new TrainAtterrissage("Main Landing Gear", 1200f, 2500f, "Hydraulique"),
//            new TrainAtterrissage("Nose Landing Gear", 800f, 1500f, "Mécanique"),
//            new TrainAtterrissage("Steerable Landing Gear", 950f, 1800f, "Électrique")
//        };

//        trainAtterrissageButton.onClick.AddListener(ShowTrainAtterrissagePanel);
//        mainLandingGearButton.onClick.AddListener(() => ShowCaracteristiques(0));
//        noseLandingGearButton.onClick.AddListener(() => ShowCaracteristiques(1));
//        steerableLandingGearButton.onClick.AddListener(() => ShowCaracteristiques(2));

//        fermerPanelInfoTrainAtterrissageButton.onClick.AddListener(ClosePanelInfoTrainAtterrissage);
//        fermerPanelCaracteristiquesTrainAtterrissageButton.onClick.AddListener(ClosePanelCaracteristiquesTrainAtterrissage);
//        retourButton.onClick.AddListener(ReturnToPlaneScene);
//    }

//    void ShowTrainAtterrissagePanel()
//    {
//        panelCaracteristiquesTrainAtterrissage.SetActive(true);
//    }

//    void ShowCaracteristiques(int index)
//    {
//        caracteristiquesText.text = trainsAtterrissage[index].GetCaracteristiques();
//        panelInfoTrainAtterrissage.SetActive(true);
//    }

//    void ClosePanelInfoTrainAtterrissage()
//    {
//        panelInfoTrainAtterrissage.SetActive(false);
//    }

//    void ClosePanelCaracteristiquesTrainAtterrissage()
//    {
//        panelCaracteristiquesTrainAtterrissage.SetActive(false);
//    }

//    void ReturnToPlaneScene()
//    {
//        SceneManager.LoadScene("Scene Plane 1");
//    }
//}

