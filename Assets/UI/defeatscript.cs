using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class defeatscript : MonoBehaviour
{
  // LOAD THE PROPER SCENE prendre la bonne scene pour jouer le jeu
  // LOAD LE HUB PRINCIPAL MENU
  public void StartBtn()
    {
        SceneManager.LoadScene("sceneDebut");
    }
}
