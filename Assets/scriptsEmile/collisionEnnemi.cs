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
        // if (infoTrigger.name == "Wall_01_1 (48)")
        // {
        //     Debug.Log("oki");
        // }
        
        if (infoTrigger.name == "grandmere")
        {
            finirPartie();
        }
        
        if (infoTrigger.name == "camion")
        {
            if (infoTrigger.GetComponent<scriptCamion>().aClee == true)
            {
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
