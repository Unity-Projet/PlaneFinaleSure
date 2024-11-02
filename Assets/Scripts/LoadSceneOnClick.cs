using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Cette ligne permet de changer de sc�ne

public class LoadSceneOnClick : MonoBehaviour
{
    // Fonction qui sera appel�e quand on clique sur le bouton
    public void LoadSampleScene()
    {
        SceneManager.LoadScene("scene_runway"); // Charger la sc�ne SampleScene
    }
    public void LoadShopScene()
    {
        SceneManager.LoadScene("ShopScene"); // Charger la sc�ne SampleScene
    }
    public void LoadQuizScene()
    {
        SceneManager.LoadScene("scene_quiz"); // Charger la sc�ne SampleScene
    }
}

