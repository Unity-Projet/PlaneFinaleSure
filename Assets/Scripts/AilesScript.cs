//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;
//using UnityEngine.SceneManagement; // Pour g�rer le changement de sc�ne

//public class AilesScript : MonoBehaviour
//{
//    // Panneaux
//    public GameObject panelCaracteristiquesAiles; // Panel des mod�les d'ailes
//    public GameObject panelInfoAiles; // Panel pour les d�tails des ailes

//    // Textes
//    public Text caracteristiquesTextAiles; // Texte pour les caract�ristiques des ailes

//    // Boutons pour composants (dans PanelComposants)
//    public Button ailesButton;

//    // Boutons pour mod�les d'ailes (dans PanelCaracteristiquesAiles)
//    public Button wingModelAButton;
//    public Button wingModelBButton;
//    public Button wingModelCButton;

//    // Boutons "Fermer" et "Retour"
//    public Button fermerPanelInfoAilesButton; // Bouton fermer pour PanelInfoAiles
//    public Button fermerPanelCaracteristiquesAilesButton; // Bouton fermer pour PanelCaracteristiquesAiles
//    public Button retourButton; // Bouton pour revenir � la sc�ne Plane

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

//        // Bouton pour revenir � la sc�ne "Plane"
//        retourButton.onClick.AddListener(ReturnToPlaneScene);
//    }

//    // M�thode pour afficher le PanelCaracteristiquesAiles
//    void ShowAilesPanel()
//    {
//        panelCaracteristiquesAiles.SetActive(true);
//    }

//    // M�thodes pour afficher les caract�ristiques des ailes
//    void ShowCaracteristiquesWingModelA()
//    {
//        string caracteristiques = "Mod�le WingModelA :\n\n" +
//                                  "Envergure: 35 m\n" +
//                                  "Surface: 150 m�\n" +
//                                  "Charge: 150 kg/m�";
//        caracteristiquesTextAiles.text = caracteristiques;
//        panelInfoAiles.SetActive(true); // Affiche le panel info ailes
//    }

//    void ShowCaracteristiquesWingModelB()
//    {
//        string caracteristiques = "Mod�le WingModelB :\n\n" +
//                                  "Envergure: 40 m\n" +
//                                  "Surface: 180 m�\n" +
//                                  "Charge: 140 kg/m�";
//        caracteristiquesTextAiles.text = caracteristiques;
//        panelInfoAiles.SetActive(true); // Affiche le panel info ailes
//    }

//    void ShowCaracteristiquesWingModelC()
//    {
//        string caracteristiques = "Mod�le WingModelC :\n\n" +
//                                  "Envergure: 32 m\n" +
//                                  "Surface: 130 m�\n" +
//                                  "Charge: 160 kg/m�";
//        caracteristiquesTextAiles.text = caracteristiques;
//        panelInfoAiles.SetActive(true); // Affiche le panel info ailes
//    }

//    // M�thodes pour fermer les panels
//    void ClosePanelInfoAiles()
//    {
//        panelInfoAiles.SetActive(false); // Cache le panel info ailes
//    }

//    void ClosePanelCaracteristiquesAiles()
//    {
//        panelCaracteristiquesAiles.SetActive(false); // Cache le panel caract�ristiques ailes
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
        return $"Mod�le {Nom} :\n\n" +
               $"Envergure: {Envergure} m\n" +
               $"Surface: {Surface} m�\n" +
               $"Charge: {Charge} kg/m�";
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
