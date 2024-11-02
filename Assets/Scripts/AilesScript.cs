//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;
//using UnityEngine.SceneManagement; // Pour gérer le changement de scène

//public class AilesScript : MonoBehaviour
//{
//    // Panneaux
//    public GameObject panelCaracteristiquesAiles; // Panel des modèles d'ailes
//    public GameObject panelInfoAiles; // Panel pour les détails des ailes

//    // Textes
//    public Text caracteristiquesTextAiles; // Texte pour les caractéristiques des ailes

//    // Boutons pour composants (dans PanelComposants)
//    public Button ailesButton;

//    // Boutons pour modèles d'ailes (dans PanelCaracteristiquesAiles)
//    public Button wingModelAButton;
//    public Button wingModelBButton;
//    public Button wingModelCButton;

//    // Boutons "Fermer" et "Retour"
//    public Button fermerPanelInfoAilesButton; // Bouton fermer pour PanelInfoAiles
//    public Button fermerPanelCaracteristiquesAilesButton; // Bouton fermer pour PanelCaracteristiquesAiles
//    public Button retourButton; // Bouton pour revenir à la scène Plane

//    void Start()
//    {
//        // Initialisation des boutons
//        ailesButton.onClick.AddListener(ShowAilesPanel);
//        wingModelAButton.onClick.AddListener(ShowCaracteristiquesWingModelA);
//        wingModelBButton.onClick.AddListener(ShowCaracteristiquesWingModelB);
//        wingModelCButton.onClick.AddListener(ShowCaracteristiquesWingModelC);

//        // Boutons pour fermer les panels
//        fermerPanelInfoAilesButton.onClick.AddListener(ClosePanelInfoAiles);
//        fermerPanelCaracteristiquesAilesButton.onClick.AddListener(ClosePanelCaracteristiquesAiles);

//        // Bouton pour revenir à la scène "Plane"
//        retourButton.onClick.AddListener(ReturnToPlaneScene);
//    }

//    // Méthode pour afficher le PanelCaracteristiquesAiles
//    void ShowAilesPanel()
//    {
//        panelCaracteristiquesAiles.SetActive(true);
//    }

//    // Méthodes pour afficher les caractéristiques des ailes
//    void ShowCaracteristiquesWingModelA()
//    {
//        string caracteristiques = "Modèle WingModelA :\n\n" +
//                                  "Envergure: 35 m\n" +
//                                  "Surface: 150 m²\n" +
//                                  "Charge: 150 kg/m²";
//        caracteristiquesTextAiles.text = caracteristiques;
//        panelInfoAiles.SetActive(true); // Affiche le panel info ailes
//    }

//    void ShowCaracteristiquesWingModelB()
//    {
//        string caracteristiques = "Modèle WingModelB :\n\n" +
//                                  "Envergure: 40 m\n" +
//                                  "Surface: 180 m²\n" +
//                                  "Charge: 140 kg/m²";
//        caracteristiquesTextAiles.text = caracteristiques;
//        panelInfoAiles.SetActive(true); // Affiche le panel info ailes
//    }

//    void ShowCaracteristiquesWingModelC()
//    {
//        string caracteristiques = "Modèle WingModelC :\n\n" +
//                                  "Envergure: 32 m\n" +
//                                  "Surface: 130 m²\n" +
//                                  "Charge: 160 kg/m²";
//        caracteristiquesTextAiles.text = caracteristiques;
//        panelInfoAiles.SetActive(true); // Affiche le panel info ailes
//    }

//    // Méthodes pour fermer les panels
//    void ClosePanelInfoAiles()
//    {
//        panelInfoAiles.SetActive(false); // Cache le panel info ailes
//    }

//    void ClosePanelCaracteristiquesAiles()
//    {
//        panelCaracteristiquesAiles.SetActive(false); // Cache le panel caractéristiques ailes
//    }

//    // Méthode pour revenir à la scène "Plane"
//    void ReturnToPlaneScene()
//    {
//        SceneManager.LoadScene("Scene Plane 1"); // Charge la scène Plane
//    }
//}
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Aile
{
    public string Nom { get; set; }
    public float Envergure { get; set; }
    public float Surface { get; set; }
    public float Charge { get; set; }

    public Aile(string nom, float envergure, float surface, float charge)
    {
        Nom = nom;
        Envergure = envergure;
        Surface = surface;
        Charge = charge;
    }

    public string GetCaracteristiques()
    {
        return $"Modèle {Nom} :\n\n" +
               $"Envergure: {Envergure} m\n" +
               $"Surface: {Surface} m²\n" +
               $"Charge: {Charge} kg/m²";
    }
}

public class AilesScript : MonoBehaviour
{
    public GameObject panelCaracteristiquesAiles;
    public GameObject panelInfoAiles;
    public Text caracteristiquesTextAiles;

    public Button ailesButton;
    public Button wingModelAButton;
    public Button wingModelBButton;
    public Button wingModelCButton;

    public Button fermerPanelInfoAilesButton;
    public Button fermerPanelCaracteristiquesAilesButton;
    public Button retourButton;

    private List<Aile> ailes;

    void Start()
    {
        // Initialisation des ailes
        ailes = new List<Aile>
        {
            new Aile("WingModelA", 35f, 150f, 150f),
            new Aile("WingModelB", 40f, 180f, 140f),
            new Aile("WingModelC", 32f, 130f, 160f)
        };

        ailesButton.onClick.AddListener(ShowAilesPanel);
        wingModelAButton.onClick.AddListener(() => ShowCaracteristiques(0));
        wingModelBButton.onClick.AddListener(() => ShowCaracteristiques(1));
        wingModelCButton.onClick.AddListener(() => ShowCaracteristiques(2));

        fermerPanelInfoAilesButton.onClick.AddListener(ClosePanelInfoAiles);
        fermerPanelCaracteristiquesAilesButton.onClick.AddListener(ClosePanelCaracteristiquesAiles);
        retourButton.onClick.AddListener(ReturnToPlaneScene);
    }

    void ShowAilesPanel()
    {
        panelCaracteristiquesAiles.SetActive(true);
    }

    void ShowCaracteristiques(int index)
    {
        caracteristiquesTextAiles.text = ailes[index].GetCaracteristiques();
        panelInfoAiles.SetActive(true);
    }

    void ClosePanelInfoAiles()
    {
        panelInfoAiles.SetActive(false);
    }

    void ClosePanelCaracteristiquesAiles()
    {
        panelCaracteristiquesAiles.SetActive(false);
    }

    void ReturnToPlaneScene()
    {
        SceneManager.LoadScene("Scene Plane 1");
    }
}
