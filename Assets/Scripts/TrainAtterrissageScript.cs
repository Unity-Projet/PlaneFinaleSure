//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;
//using UnityEngine.SceneManagement;

//public class TrainAtterrissageScript : MonoBehaviour
//{
//    // Panneaux
//    public GameObject panelCaracteristiquesTrainAtterrissage; // Panel des trains d'atterrissage
//    public GameObject panelInfoTrainAtterrissage; // Panel pour les d�tails des trains d'atterrissage

//    // Textes
//    public Text caracteristiquesText; // Texte pour les caract�ristiques des trains d'atterrissage

//    // Boutons pour composants (dans PanelComposants)
//    public Button trainAtterrissageButton;

//    // Boutons pour types de trains d'atterrissage (dans PanelCaracteristiquesTrainAtterrissage)
//    public Button mainLandingGearButton;
//    public Button noseLandingGearButton;
//    public Button steerableLandingGearButton;

//    // Boutons "Fermer" et "Retour"
//    public Button fermerPanelInfoTrainAtterrissageButton; // Bouton fermer pour PanelInfoTrainAtterrissage
//    public Button fermerPanelCaracteristiquesTrainAtterrissageButton; // Bouton fermer pour PanelCaracteristiquesTrainAtterrissage
//    public Button retourButton; // Bouton pour revenir � la sc�ne Plane

//    void Start()
//    {
//        // Initialisation des boutons
//        trainAtterrissageButton.onClick.AddListener(ShowTrainAtterrissagePanel);
//        mainLandingGearButton.onClick.AddListener(ShowMainLandingGearCaracteristiques);
//        noseLandingGearButton.onClick.AddListener(ShowNoseLandingGearCaracteristiques);
//        steerableLandingGearButton.onClick.AddListener(ShowSteerableLandingGearCaracteristiques);

//        // Boutons pour fermer les panels
//        fermerPanelInfoTrainAtterrissageButton.onClick.AddListener(ClosePanelInfoTrainAtterrissage);
//        fermerPanelCaracteristiquesTrainAtterrissageButton.onClick.AddListener(ClosePanelCaracteristiquesTrainAtterrissage);

//        // Bouton pour revenir � la sc�ne "Plane"
//        retourButton.onClick.AddListener(ReturnToPlaneScene);
//    }

//    // M�thode pour afficher le PanelCaracteristiquesTrainAtterrissage
//    void ShowTrainAtterrissagePanel()
//    {
//        panelCaracteristiquesTrainAtterrissage.SetActive(true);
//    }

//    // M�thodes pour afficher les caract�ristiques des trains d'atterrissage
//    public void ShowMainLandingGearCaracteristiques()
//    {
//        string caracteristiques = "Main Landing Gear:\n\n" +
//                                  "Poids: 1200 kg\n" +
//                                  "R�sistance: 2500 kg\n" +
//                                  "Type de frein: Hydraulique";
//        caracteristiquesText.text = caracteristiques;
//        panelInfoTrainAtterrissage.SetActive(true); // Affiche le panneau des caract�ristiques
//    }

//    public void ShowNoseLandingGearCaracteristiques()
//    {
//        string caracteristiques = "Nose Landing Gear:\n\n" +
//                                  "Poids: 800 kg\n" +
//                                  "R�sistance: 1500 kg\n" +
//                                  "Type de frein: M�canique";
//        caracteristiquesText.text = caracteristiques;
//        panelInfoTrainAtterrissage.SetActive(true); // Affiche le panneau des caract�ristiques
//    }

//    public void ShowSteerableLandingGearCaracteristiques()
//    {
//        string caracteristiques = "Steerable Landing Gear:\n\n" +
//                                  "Poids: 950 kg\n" +
//                                  "R�sistance: 1800 kg\n" +
//                                  "Type de frein: �lectrique";
//        caracteristiquesText.text = caracteristiques;
//        panelInfoTrainAtterrissage.SetActive(true); // Affiche le panneau des caract�ristiques
//    }

//    // M�thodes pour fermer les panels
//    void ClosePanelInfoTrainAtterrissage()
//    {
//        panelInfoTrainAtterrissage.SetActive(false); // Cache le panneau info trains d'atterrissage
//    }

//    void ClosePanelCaracteristiquesTrainAtterrissage()
//    {
//        panelCaracteristiquesTrainAtterrissage.SetActive(false); // Cache le panneau caract�ristiques trains d'atterrissage
//    }

//    // M�thode pour revenir � la sc�ne "Plane"
//    void ReturnToPlaneScene()
//    {
//        SceneManager.LoadScene("Scene Plane 1"); // Charge la sc�ne Plane
//    }
//}
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
               $"R�sistance: {Resistance} kg\n" +
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

    private List<TrainAtterrissage> trainsAtterrissage;

    void Start()
    {
        // Initialisation des trains d'atterrissage
        trainsAtterrissage = new List<TrainAtterrissage>
        {
            new TrainAtterrissage("Main Landing Gear", 1200f, 2500f, "Hydraulique"),
            new TrainAtterrissage("Nose Landing Gear", 800f, 1500f, "M�canique"),
            new TrainAtterrissage("Steerable Landing Gear", 950f, 1800f, "�lectrique")
        };

        trainAtterrissageButton.onClick.AddListener(ShowTrainAtterrissagePanel);
        mainLandingGearButton.onClick.AddListener(() => ShowCaracteristiques(0));
        noseLandingGearButton.onClick.AddListener(() => ShowCaracteristiques(1));
        steerableLandingGearButton.onClick.AddListener(() => ShowCaracteristiques(2));

        fermerPanelInfoTrainAtterrissageButton.onClick.AddListener(ClosePanelInfoTrainAtterrissage);
        fermerPanelCaracteristiquesTrainAtterrissageButton.onClick.AddListener(ClosePanelCaracteristiquesTrainAtterrissage);
        retourButton.onClick.AddListener(ReturnToPlaneScene);
    }

    void ShowTrainAtterrissagePanel()
    {
        panelCaracteristiquesTrainAtterrissage.SetActive(true);
    }

    void ShowCaracteristiques(int index)
    {
        caracteristiquesText.text = trainsAtterrissage[index].GetCaracteristiques();
        panelInfoTrainAtterrissage.SetActive(true);
    }

    void ClosePanelInfoTrainAtterrissage()
    {
        panelInfoTrainAtterrissage.SetActive(false);
    }

    void ClosePanelCaracteristiquesTrainAtterrissage()
    {
        panelCaracteristiquesTrainAtterrissage.SetActive(false);
    }

    void ReturnToPlaneScene()
    {
        SceneManager.LoadScene("Scene Plane 1");
    }
}

