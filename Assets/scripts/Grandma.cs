using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Grandma : MonoBehaviour
{

    private NavMeshAgent Lennemi;
    public Transform PersonnageCible;
    private Animator Animator;
    private AudioSource AudioSource;
    private GameObject PorteActuelle;// quelle porte?

   


    public Transform pointA;
    public Transform pointB;
   

    public AudioClip RireEnnemi1; // son rire

    // public DeplacementPersonnage deplacementPersonnageScript; // Reference au script

    private Transform destination; // la destination de lennemi

    // Start is called before the first frame update
    void Start()
    {
        Lennemi = GetComponent<NavMeshAgent>();
        Animator = GetComponent<Animator>();
        AudioSource = GetComponent<AudioSource>();

        destination = pointA; // on commence par le point a bien sur
        Lennemi.SetDestination(destination.position);

        



    }

    // Update is called once per frame
    void Update()
    {


        if (!Lennemi.pathPending && Lennemi.remainingDistance < 0.5f)
        {
            destination = destination == pointA ? pointB : pointA; // Changer la destination
            Lennemi.SetDestination(destination.position);



        }




    }

    /*********************************************************************************************************  
                                       GESTIONNARE DE COLLISIONS DE TYPE TRIGGER EMTER
    ******************************************************************************************************/




    private void OnTriggerEnter(Collider InfosCollider)
    {
        // Si les mannequins touchent à une porte

        if (InfosCollider.gameObject.tag == "Porte")
        {
            // On parle de quelle porte?
            PorteActuelle = InfosCollider.gameObject;

            // La porte s'ouvre
            Animator porteAnimator = PorteActuelle.GetComponent<Animator>();
            if (porteAnimator != null)
            {
                porteAnimator.SetBool("Ouvre", true);
                PorteActuelle.GetComponent<Collider>().enabled = false; // Désactivation du collider
                // les mannequins sont lents. je ne veux pas qu'ils restent poignés.
            }

            // Déclenchez la fermeture de la porte après un délai. Assez de temps pour qu'ils passent.
            Invoke("RefermerPorte", 6f);
        }

        if (InfosCollider.gameObject.CompareTag("Personnage"))
        {
            AudioSource.PlayOneShot(RireEnnemi1);
        }


    }




    private void OnTriggerStay(Collider InfosCollider)
    {
       
        if (InfosCollider.gameObject.CompareTag("Personnage"))
        {
            //  la destination de l'ennemi pour suivre le personnage
            destination = InfosCollider.gameObject.transform;
            Lennemi.SetDestination(destination.position);
            Lennemi.speed = 1.6f;
            Animator.SetBool("Course", true); // Active l'animation de course
       


        }


    }





    private void RefermerPorte()
    {
        // Quelle porte? l'actuelle. celle qui est touché.


        if (PorteActuelle != null)
        {
            Animator porteAnimator = PorteActuelle.GetComponent<Animator>();
            if (porteAnimator != null)
            {
                porteAnimator.SetBool("Ouvre", false);
                PorteActuelle.GetComponent<Collider>().enabled = true; // Réactivation du collider
            }
        }
    }


    private void OnTriggerExit(Collider InfosCollider)
    {
        if (InfosCollider.gameObject.CompareTag("Personnage"))
        {
            // Appeler la méthode après un délai de 10 secondes
            Invoke("RetourAuCheminInitial", 10f);
        }
    }

    private void RetourAuCheminInitial()
    {
        // Remettre la vitesse à la normale
        Lennemi.speed = 1; //la vitesse redevient à la normale

        // Désactiver l'animation de course et activer celle de marche
        Animator.SetBool("Course", false);

        // lennemi revient à la position de départ
        destination = pointA;
        Lennemi.SetDestination(destination.position);
       
    }





}

