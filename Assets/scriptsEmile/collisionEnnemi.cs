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
        if (infoTrigger.name == "Camion")
        {
            if (infoTrigger.GetComponent<scriptCamion>().aClee == true)
            {
                gagnerPartie();   
            }
        }
    }

    public void gagnerPartie(){
        SceneManager.LoadScene("sceneVictoire");
    }
}
