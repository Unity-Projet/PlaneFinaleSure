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

    // Ajout des éléments pour l'alerte
    public GameObject alertPanel;
    public Text alertText;
    public Button closeAlertButton;

    // Ajout du bouton "Acheter" pour chaque modèle d'aile
    public Button acheterModelAButton;

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

        // Assignation des listeners aux boutons
        ailesButton.onClick.AddListener(ShowAilesPanel);
        wingModelAButton.onClick.AddListener(() => ShowCaracteristiques(0));
        wingModelBButton.onClick.AddListener(() => ShowCaracteristiques(1));
        wingModelCButton.onClick.AddListener(() => ShowCaracteristiques(2));

        fermerPanelInfoAilesButton.onClick.AddListener(ClosePanelInfoAiles);
        fermerPanelCaracteristiquesAilesButton.onClick.AddListener(ClosePanelCaracteristiquesAiles);
        retourButton.onClick.AddListener(ReturnToPlaneScene);

        // Ajout des listeners pour le bouton "Acheter"
        acheterModelAButton.onClick.AddListener(() => OpenAlertForAcheter("Vous n'avez pas assez d'argent pour acheter"));

        // Ajout du listener pour la fermeture de l'alerte
        closeAlertButton.onClick.AddListener(CloseAlert);
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

    // Méthode pour afficher l'alerte lorsque l'utilisateur clique sur "Acheter"
    public void OpenAlertForAcheter(string message)
    {
        ShowAlert(message); // Affiche le message personnalisé selon le modèle d'aile acheté
    }
}
