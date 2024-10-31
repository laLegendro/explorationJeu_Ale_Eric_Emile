using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class testAi : MonoBehaviour
{
    public Vector3 destination;
    public Vector3 ancienneDestination;
    public Transform destinationEnCours;
    public Transform destination1;
    public Transform destination2;
    public Transform destination3;
    public Transform destination4;
    public NavMeshAgent navAgent;

    // Start is called before the first frame update
    void Start()
    {
        navAgent = GetComponent<NavMeshAgent>();
        Deplacement();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void choisirDestination(){
        int destinationChoisie = Random.Range(0,4);
        if (destinationChoisie == 0)
        {
            destination = destination1.position;
            destinationEnCours = destination1;
        }
        else if(destinationChoisie == 1){
            destination = destination2.position;
            destinationEnCours = destination2;
        }
        else if(destinationChoisie == 2){
            destination = destination3.position;
            destinationEnCours = destination3;
        }
        else if(destinationChoisie == 3){
            destination = destination4.position;
            destinationEnCours = destination4;
        }
    }

    public void Deplacement(){
        Debug.Log("fonction fonctionne");
        choisirDestination();
        if (destination == ancienneDestination)
        {
            Debug.Log("condition active");
            choisirDestination();
            Debug.Log("condition active + fonction active");
        }
        Debug.Log("pre destination");
        navAgent.SetDestination(destination);
        Debug.Log("destination choisie");
        ancienneDestination = destination;
        Debug.Log("destination ancienne choisie");
    }


    void OnTriggerEnter(Collider infoTrigger){
        Debug.Log("oki");
        if (infoTrigger.tag == "destination")
        {
            Deplacement(); 
        }
    }
}
