using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneOnClick : MonoBehaviour
{
    public GameObject missionPanel; // R�f�rence au panel contenant les boutons de mission

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

    // Fonction pour charger la sc�ne de la Mission 1
    public void LoadMission1Scene()
    {
        SceneManager.LoadScene("scene_runway"); // Charger la sc�ne pour la Mission 1
    }

    // Fonction pour charger la sc�ne de la Mission 2
    public void LoadMission2Scene()
    {
        SceneManager.LoadScene("scene_runway1"); // Charger la sc�ne pour la Mission 2
    }
    

    public void LoadMission3Scene()
    {
        SceneManager.LoadScene("scene_runway 2"); // Charger la sc�ne pour la Mission 1
    }
    public void LoadSSSSSSSSSS()
    {
        SceneManager.LoadScene("FlightTipsScene"); // Charger la sc�ne pour la Mission 1
    }

    // Fonction pour charger la sc�ne du magasin
    public void LoadShopScene()
    {
        SceneManager.LoadScene("ShopScene"); // Charger la sc�ne du magasin
    }

    // Fonction pour charger la sc�ne du quiz
    public void LoadQuizScene()
    {
        SceneManager.LoadScene("scene_quiz"); // Charger la sc�ne du quiz
    }
    public void LoadAvionScene()
    {
        SceneManager.LoadScene("avion"); // Charger la sc�ne du quiz
    }
      public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName); 
    }
}
