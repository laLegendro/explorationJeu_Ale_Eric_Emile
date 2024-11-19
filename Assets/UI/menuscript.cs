using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menuscript : MonoBehaviour
{
  // LOAD THE PROPER SCENE
  public void StartBtn()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
