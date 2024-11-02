//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;
//using UnityEngine.SceneManagement; // Pour g�rer le changement de sc�ne

//public class MoteursScript : MonoBehaviour
//{
//    // Panneaux
//    public GameObject panelCaracteristiquesMoteurs; // Panel des moteurs
//    public GameObject panelInfoMoteurs; // Panel pour les d�tails des moteurs

//    // Textes
//    public Text caracteristiquesTextMoteurs; // Texte pour les caract�ristiques

//    // Boutons pour composants (dans PanelComposants)
//    public Button moteursButton;
//    //public Button ailesButton;

//    // Boutons pour moteurs (dans PanelCaracteristiquesMoteurs)
//    public Button trent500Button;
//    public Button trent700Button;
//    public Button cf680E1Button;

//    // Boutons "Fermer" et "Retour"
//    public Button fermerPanelInfoButton; // Bouton fermer pour PanelInfoMoteurs
//    public Button fermerPanelCaracteristiquesButton; // Bouton fermer pour PanelCaracteristiquesMoteurs
//    public Button retourButton; // Bouton pour revenir � la sc�ne Plane

//    void Start()
//    {
//        // Initialisation des boutons
//        moteursButton.onClick.AddListener(ShowMoteursPanel);
//        trent500Button.onClick.AddListener(ShowCaracteristiquesTrent500);
//        trent700Button.onClick.AddListener(ShowCaracteristiquesTrent700);
//        cf680E1Button.onClick.AddListener(ShowCaracteristiquesCF680E1);

//        // Boutons pour fermer les panels
//        fermerPanelInfoButton.onClick.AddListener(ClosePanelInfoMoteurs);
//        fermerPanelCaracteristiquesButton.onClick.AddListener(ClosePanelCaracteristiquesMoteurs);

//        // Bouton pour revenir � la sc�ne "Plane"
//        retourButton.onClick.AddListener(ReturnToPlaneScene);
//    }

//    // M�thode pour afficher le PanelCaracteristiquesMoteurs
//    void ShowMoteursPanel()
//    {
//        panelCaracteristiquesMoteurs.SetActive(true);
//    }

//    // M�thodes pour afficher les caract�ristiques des moteurs
//    void ShowCaracteristiquesTrent500()
//    {
//        string caracteristiques = "Mod�le Trent500 :\n\n" +
//                                  "Vitesse: 500\n" +
//                                  "Consommation de carburant: 300\n" +
//                                  "Poids: 500 kg\n" +
//                                  "R�sistance: 2500 kg\n" +
//                                  "Type de frein: Hydraulique";
//        caracteristiquesTextMoteurs.text = caracteristiques;
//        panelInfoMoteurs.SetActive(true); // Affiche le nouveau panel
//    }

//    void ShowCaracteristiquesTrent700()
//    {
//        string caracteristiques = "Mod�le Trent700 :\n\n" +
//                                  "Vitesse: 700\n" +
//                                  "Consommation de carburant: 250\n" +
//                                  "Poids: 800 kg\n" +
//                                  "R�sistance: 1500 kg\n" +
//                                  "Type de frein: M�canique";
//        caracteristiquesTextMoteurs.text = caracteristiques;
//        panelInfoMoteurs.SetActive(true); // Affiche le nouveau panel
//    }

//    void ShowCaracteristiquesCF680E1()
//    {
//        string caracteristiques = "Mod�le CF680E1 :\n\n" +
//                                  "Vitesse: 600\n" +
//                                  "Consommation de carburant: 280\n" +
//                                  "Poids: 950 kg\n" +
//                                  "R�sistance: 1800 kg\n" +
//                                  "Type de frein: �lectrique";
//        caracteristiquesTextMoteurs.text = caracteristiques;
//        panelInfoMoteurs.SetActive(true); // Affiche le nouveau panel
//    }

//    // M�thodes pour fermer les panels
//    void ClosePanelInfoMoteurs()
//    {
//        panelInfoMoteurs.SetActive(false); // Cache le panel info moteurs
//    }

//    void ClosePanelCaracteristiquesMoteurs()
//    {
//        panelCaracteristiquesMoteurs.SetActive(false); // Cache le panel caract�ristiques moteurs
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

public class Moteur
{
    public string Nom { get; set; }
    public int Vitesse { get; set; }
    public int Consommation { get; set; }
    public int Poids { get; set; }
    public int Resistance { get; set; }
    public string TypeDeFrein { get; set; }

    public Moteur(string nom, int vitesse, int consommation, int poids, int resistance, string typeDeFrein)
    {
        Nom = nom;
        Vitesse = vitesse;
        Consommation = consommation;
        Poids = poids;
        Resistance = resistance;
        TypeDeFrein = typeDeFrein;
    }

    public string GetCaracteristiques()
    {
        return $"Mod�le {Nom} :\n\n" +
               $"Vitesse: {Vitesse}\n" +
               $"Consommation de carburant: {Consommation}\n" +
               $"Poids: {Poids} kg\n" +
               $"R�sistance: {Resistance} kg\n" +
               $"Type de frein: {TypeDeFrein}";
    }
}

public class MoteursScript : MonoBehaviour
{
    public GameObject panelCaracteristiquesMoteurs;
    public GameObject panelInfoMoteurs;
    public Text caracteristiquesTextMoteurs;

    public Button moteursButton;
    public Button trent500Button;
    public Button trent700Button;
    public Button cf680E1Button;

    public Button fermerPanelInfoButton;
    public Button fermerPanelCaracteristiquesButton;
    public Button retourButton;

    private List<Moteur> moteurs;

    void Start()
    {
        // Initialisation des moteurs
        moteurs = new List<Moteur>
        {
            new Moteur("Trent500", 500, 300, 500, 2500, "Hydraulique"),
            new Moteur("Trent700", 700, 250, 800, 1500, "M�canique"),
            new Moteur("CF680E1", 600, 280, 950, 1800, "�lectrique")
        };

        moteursButton.onClick.AddListener(ShowMoteursPanel);
        trent500Button.onClick.AddListener(() => ShowCaracteristiques(0));
        trent700Button.onClick.AddListener(() => ShowCaracteristiques(1));
        cf680E1Button.onClick.AddListener(() => ShowCaracteristiques(2));

        fermerPanelInfoButton.onClick.AddListener(ClosePanelInfoMoteurs);
        fermerPanelCaracteristiquesButton.onClick.AddListener(ClosePanelCaracteristiquesMoteurs);
        retourButton.onClick.AddListener(ReturnToPlaneScene);
    }

    void ShowMoteursPanel()
    {
        panelCaracteristiquesMoteurs.SetActive(true);
    }

    void ShowCaracteristiques(int index)
    {
        caracteristiquesTextMoteurs.text = moteurs[index].GetCaracteristiques();
        panelInfoMoteurs.SetActive(true);
    }

    void ClosePanelInfoMoteurs()
    {
        panelInfoMoteurs.SetActive(false);
    }

    void ClosePanelCaracteristiquesMoteurs()
    {
        panelCaracteristiquesMoteurs.SetActive(false);
    }

    void ReturnToPlaneScene()
    {
        SceneManager.LoadScene("Scene Plane 1");
    }
}
