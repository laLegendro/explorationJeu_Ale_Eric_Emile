using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scriptCamion : MonoBehaviour
{

    public bool aClee = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision infoCollision){
        Debug.Log(infoCollision.gameObject.name);
        if (infoCollision.gameObject.name == "Clee")
        {
            aClee = true;
            infoCollision.gameObject.SetActive(false);
        }
    }
}
