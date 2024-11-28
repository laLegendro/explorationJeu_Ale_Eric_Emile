using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menuscript : MonoBehaviour
{
  // LOAD THE PROPER SCENE prendre la bonne scene pour jouer le jeu
  // NOMMER LA SCENE DE ALEJANDRO
  public void StartBtn()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
