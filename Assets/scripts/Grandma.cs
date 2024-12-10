using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Grandma : MonoBehaviour
{

    private NavMeshAgent Lennemi;//la grand mère
    public Transform PersonnageCible;// le joueur
    private Animator Animator;// aller cxercher l'animator
    private AudioSource AudioSource;// quand qu'à te court après a va rire 
    private GameObject PorteActuelle; // pour gérer les portes lorsqu'elle touche une porte

   


    public Transform pointA; // les points de base pour son déplacement
    public Transform pointB;// le deuxième point
   

    public AudioClip RireVieille; // son rire creepy

    // public DeplacementPersonnage deplacementPersonnageScript; // Reference au script

    private Transform destination; // la destination de la vieille

    // Start is called before the first frame update
    void Start()
    {
        // je vais get les components nécessaires
        Animator = GetComponent<Animator>();
        Lennemi = GetComponent<NavMeshAgent>();
        AudioSource = GetComponent<AudioSource>();

        destination = pointA; // on commence par le point a bien sur
        Lennemi.SetDestination(destination.position);// je set la destination à la position du pointA

        



    }

    // Update is called once per frame
    void Update()
    {

        Animator.SetBool("Marche", true); // Active l'animation de marche dès le début

        // si l'ennemi est pas loin du prochain point, j'échange les points de destination
        if (!Lennemi.pathPending && Lennemi.remainingDistance < 0.5f)
        {
            destination = destination == pointA ? pointB : pointA; 
            Lennemi.SetDestination(destination.position);
           



        }




    }

    /*********************************************************************************************************  
                                       GESTIONNARE DE COLLISIONS DE TYPE TRIGGER EMTER
    ******************************************************************************************************/




    private void OnTriggerEnter(Collider InfosCollider)
    {
        // Si la vieille touche à la porte

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
               
            }

            // Déclenchez la fermeture de la porte après un délai. Assez de temps pour qu'ils passent.
            Invoke("RefermerPorte", 6f);
        }

        if (InfosCollider.gameObject.CompareTag("Joueur"))
        {
            AudioSource.PlayOneShot(RireVieille);
        }


    }




    private void OnTriggerStay(Collider InfosCollider)
    {
       
        if (InfosCollider.gameObject.CompareTag("Joueur"))
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
        if (InfosCollider.gameObject.CompareTag("Joueur"))
        {
            // Appeler la méthode après un délai de 10 secondes
            Invoke("RetourAuCheminInitial", 10f);
        }
    }

    private void RetourAuCheminInitial()
    {
        // Remettre la vitesse à la normale
        Lennemi.speed = 0.5f; //la vitesse redevient à la normale

        // Désactiver l'animation de course et activer celle de marche
        Animator.SetBool("Course", false);

        // lennemi revient à la position de départ
        destination = pointA;
        Lennemi.SetDestination(destination.position);
       
    }





}

