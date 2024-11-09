using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneOnClick : MonoBehaviour
{
    public GameObject missionPanel; // Référence au panel contenant les boutons de mission

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

    // Fonction pour charger la scène de la Mission 1
    public void LoadMission1Scene()
    {
        SceneManager.LoadScene("scene_runway"); // Charger la scène pour la Mission 1
    }

    // Fonction pour charger la scène de la Mission 2
    public void LoadMission2Scene()
    {
        SceneManager.LoadScene("scene_runway1"); // Charger la scène pour la Mission 2
    }

    // Fonction pour charger la scène du magasin
    public void LoadShopScene()
    {
        SceneManager.LoadScene("ShopScene"); // Charger la scène du magasin
    }

    // Fonction pour charger la scène du quiz
    public void LoadQuizScene()
    {
        SceneManager.LoadScene("scene_quiz"); // Charger la scène du quiz
    }
    public void LoadAvionScene()
    {
        SceneManager.LoadScene("avion"); // Charger la scène du quiz
    }
}
