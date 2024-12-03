using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class winscript : MonoBehaviour
{
   
  // LOAD THE PROPER SCENE prendre la bonne scene pour jouer le jeu
  // REPLAY SI TU WIN ANYWAYS similar avec script defeat
  public void StartBtn()
    {
        SceneManager.LoadScene("SampleScene_v2 avatar UI START");
    }

}
