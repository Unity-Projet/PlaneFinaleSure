using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Cette ligne permet de changer de scène

public class LoadSceneOnClick : MonoBehaviour
{
    // Fonction qui sera appelée quand on clique sur le bouton
    public void LoadSampleScene()
    {
        SceneManager.LoadScene("scene_runway"); // Charger la scène SampleScene
    }
    public void LoadShopScene()
    {
        SceneManager.LoadScene("ShopScene"); // Charger la scène SampleScene
    }
    public void LoadQuizScene()
    {
        SceneManager.LoadScene("scene_quiz"); // Charger la scène SampleScene
    }
}

