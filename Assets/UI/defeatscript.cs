using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class defeatscript : MonoBehaviour
{
  // LOAD THE PROPER SCENE prendre la bonne scene pour jouer le jeu
  public void StartBtn()
    {
        SceneManager.LoadScene("SampleScene_v2 avatar UI START");
    }
}
