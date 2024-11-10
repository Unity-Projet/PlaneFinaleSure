//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.SceneManagement;

//public class LoadSceneOnClick : MonoBehaviour
//{
//    public GameObject missionPanel; // R�f�rence au panel contenant les boutons de mission

//    // Fonction pour afficher le panel des missions
//    public void ShowMissionPanel()
//    {
//        missionPanel.SetActive(true); // Afficher le panel
//    }

//    // Fonction pour cacher le panel des missions (si besoin)
//    public void HideMissionPanel()
//    {
//        missionPanel.SetActive(false); // Cacher le panel
//    }

//    // Fonction pour charger la sc�ne de la Mission 1
//    public void LoadMission1Scene()
//    {
//        SceneManager.LoadScene("scene_runway"); // Charger la sc�ne pour la Mission 1
//    }

//    // Fonction pour charger la sc�ne de la Mission 2
//    public void LoadMission2Scene()
//    {
//        SceneManager.LoadScene("scene_runway1"); // Charger la sc�ne pour la Mission 2
//    }


//    public void LoadMission3Scene()
//    {
//        SceneManager.LoadScene("scene_runway 2"); // Charger la sc�ne pour la Mission 1
//    }
//    public void LoadSSSSSSSSSS()
//    {
//        SceneManager.LoadScene("FlightTipsScene"); // Charger la sc�ne pour la Mission 1
//    }

//    // Fonction pour charger la sc�ne du magasin
//    public void LoadShopScene()
//    {
//        SceneManager.LoadScene("ShopScene"); // Charger la sc�ne du magasin
//    }

//    // Fonction pour charger la sc�ne du quiz
//    public void LoadQuizScene()
//    {
//        SceneManager.LoadScene("scene_quiz"); // Charger la sc�ne du quiz
//    }
//    public void LoadAvionScene()
//    {
//        SceneManager.LoadScene("avion"); // Charger la sc�ne du quiz
//    }
//      public void LoadScene(string sceneName)
//    {
//        SceneManager.LoadScene(sceneName); 
//    }
//}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadSceneOnClick : MonoBehaviour
{
    public GameObject missionPanel; // Référence au panel contenant les boutons de mission
    public GameObject alertPanel; // Référence au panel d'alerte
    public Text alertMessageText; // Référence au texte du message dans le panneau d'alerte
    private string sceneToLoad; // Variable pour stocker le nom de la scène à charger

    // Fonction pour afficher le panel des missions
    public void ShowMissionPanel()
    {
        missionPanel.SetActive(true); // Afficher le panel
    }

    // Fonction pour cacher le panel des missions (si besoin)
    public void HideMissionPanel()
    {
        missionPanel.SetActive(false); // Cacher le panel
    }

    // Affiche un message d'alerte avant de charger une scène
    public void ShowAlert(string message, string sceneName)
    {
        alertMessageText.text = message; // Définit le texte de l'alerte
        sceneToLoad = sceneName; // Enregistre le nom de la scène cible
        alertPanel.SetActive(true); // Affiche le panneau d'alerte
    }

    // Appelée lorsqu'on appuie sur "Commencer" dans l'alerte
    public void OnConfirmLoadScene()
    {
        alertPanel.SetActive(false); // Masque le panneau d'alerte
        SceneManager.LoadScene(sceneToLoad); // Charge la scène stockée dans sceneToLoad
    }

    // Appelée lorsqu'on appuie sur "Annuler" dans l'alerte
    public void OnCancelLoadScene()
    {
        alertPanel.SetActive(false); // Masque le panneau d'alerte sans changer de scène
    }

    // Fonction pour charger la scène de la Mission 1 avec alerte
    public void LoadMission1Scene()
    {
        ShowAlert("Mission1 : Passe exactement au centre de l'anneau en moins d'une minute avec ton avion !", "scene_runway");
    }

    // Fonction pour charger la scène de la Mission 2 avec alerte
    public void LoadMission2Scene()
    {
        ShowAlert("Mission 2 : Descends en moins d'une minute, passe par l'anneau en suivant la trajectoire indiquée !", "scene_runway1");
    }

    // Fonction pour charger la scène de la Mission 3 avec alerte
    public void LoadMission3Scene()
    {
        ShowAlert("Mission 3 : Éloigne-toi des obstacles et évite tout contact pendant 35 secondes !", "scene_runway 2");
    }

    // Fonction pour charger la scène du magasin sans alerte
    public void LoadShopScene()
    {
        SceneManager.LoadScene("ShopScene"); // Charge la scène du magasin
    }

    // Fonction pour charger la scène du quiz sans alerte
    public void LoadQuizScene()
    {
        SceneManager.LoadScene("scene_quiz"); // Charge la scène du quiz
    }

    // Fonction pour charger la scène de l'avion sans alerte
    public void LoadAvionScene()
    {
        SceneManager.LoadScene("avion"); // Charge la scène de l'avion
    }

    // Fonction générique pour charger une scène spécifique sans alerte
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
