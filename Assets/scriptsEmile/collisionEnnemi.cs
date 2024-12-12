using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class collisionEnnemi : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider infoTrigger){
        Debug.Log("oki general");
        
        if (infoTrigger.name == "Capsule")
        {
            Debug.Log("oki");
            // finirPartie();
        }
        
        if (infoTrigger.name == "Camion")
        {
            if (infoTrigger.GetComponent<scriptCamion>().aClee == true)
            {
                // Debug.Log("oki");
                gagnerPartie();   
            }
        }
    }

    public void finirPartie(){
        SceneManager.LoadScene("SampleScene_v2 avatar UI DEFEAT");
    }

    public void gagnerPartie(){
        SceneManager.LoadScene("SampleScene_v2 avatar UI WIN");
    }
}
