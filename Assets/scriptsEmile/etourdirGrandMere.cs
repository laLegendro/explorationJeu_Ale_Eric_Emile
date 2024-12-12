using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class etourdirGrandMere : MonoBehaviour
{
    NavMeshAgent navAgent ;
    public Vector3 destination;
    public bool aCollision;

    public GameObject gameObjectAAllume;

    // Start is called before the first frame update
    void Start()
    {
        navAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision InfoCollision){
        Debug.Log("oki");
        if (InfoCollision.gameObject.tag == "Objet")
        {
            gameObjectAAllume = InfoCollision.gameObject;
            InfoCollision.gameObject.SetActive(false);
            navAgent.enabled = false;
            Invoke("ReprendreGrandMere", 10f);
            Invoke("ReprendreGameObject", 12f);
        }
    }

    private void OnTriggerExit(){
        aCollision=true;
    }

    public void ReprendreGameObject(){
        gameObjectAAllume.SetActive(true);
    }

    public void ReprendreGrandMere(){
        navAgent.enabled = true;
        navAgent.SetDestination(destination);
    }
}
