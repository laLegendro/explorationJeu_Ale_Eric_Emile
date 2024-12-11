using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Grandma : MonoBehaviour
{
    private NavMeshAgent Lennemi; // The enemy (grandma)
    public Transform PersonnageCible; // The player (target)
    private Animator Animator; // To control animations
    private AudioSource AudioSource; // For sound effects
    private GameObject PorteActuelle; // To handle doors when grandma touches a door

    public Transform pointA; // First point for her movement
    public Transform pointB; // Second point

    public AudioClip RireVieille; // Creepy laugh sound
    public AudioClip SonPorteOuvre; // Door opening sound
    public AudioClip SonFermePorte; // Door closing sound

    // Flag to prevent opening/closing the door multiple times
    private bool porteEnTrainDOuvrir = false;

    private Transform destination; // Grandma's current destination (either pointA or pointB)

    void Start()
    {
        // Get the necessary components
        Animator = GetComponent<Animator>();
        Lennemi = GetComponent<NavMeshAgent>();
        AudioSource = GetComponent<AudioSource>();

        destination = pointA; // Start by setting the destination to pointA
        Lennemi.SetDestination(destination.position); // Set the NavMeshAgent destination to pointA
    }

    void Update()
    {
        // Control the walking and running animations
        if (Lennemi.speed < 1.6f)
        {
            Animator.SetBool("Marche", true); // Activate walking animation
            Animator.SetBool("Course", false); // Deactivate running animation
        }
        else
        {
            Animator.SetBool("Marche", false); // Deactivate walking animation
            Animator.SetBool("Course", true); // Activate running animation
        }

        // Change destination if the enemy reaches the current point
        if (!Lennemi.pathPending && Lennemi.remainingDistance < 0.5f)
        {
            destination = destination == pointA ? pointB : pointA; // Toggle destination
            Lennemi.SetDestination(destination.position);
        }
    }

    /*********************************************************************************************************  
                               HANDLING TRIGGER COLLISIONS
     ******************************************************************************************************/

    private void OnTriggerEnter(Collider infosCollider)
    {
        // If grandma touches the door
        if (infosCollider.gameObject.CompareTag("Porte") && !porteEnTrainDOuvrir)
        {
            PorteActuelle = infosCollider.gameObject; // Identify which door it is

            // Open the door
            PorteActuelle.GetComponent<Animator>().SetBool("Ouvre", true);
            AudioSource.PlayOneShot(SonPorteOuvre);

            // Mark the door as opening
            porteEnTrainDOuvrir = true;

            // Close the door after 4 seconds
            Invoke("RefermerPorte", 4f);
        }

        // If grandma touches the player
        if (infosCollider.gameObject.CompareTag("Joueur"))
        {
            AudioSource.PlayOneShot(RireVieille); // Play the creepy laugh
        }
    }

    private void OnTriggerStay(Collider infosCollider)
    {
        // If the player is in the trigger area, grandma will chase the player
        if (infosCollider.gameObject.CompareTag("Joueur"))
        {
            // Set grandma's destination to the player's position
            Lennemi.SetDestination(infosCollider.gameObject.transform.position);
            Lennemi.speed = 1.6f; // Run faster when chasing the player
            Animator.SetBool("Course", true); // Activate running animation
        }
    }

    private void RefermerPorte()
    {
        // Close the door
        if (PorteActuelle != null)
        {
            PorteActuelle.GetComponent<Animator>().SetBool("Ouvre", false); // Close door animation
            AudioSource.PlayOneShot(SonFermePorte); // Play door closing sound
        }

        // Reset the flag to allow interaction with the door again
        porteEnTrainDOuvrir = false;
    }

    private void OnTriggerExit(Collider infosCollider)
    {
        // If the player leaves the trigger area, grandma will return to her starting path
        if (infosCollider.gameObject.CompareTag("Joueur"))
        {
            // Call the method after 10 seconds
            Invoke("RetourAuCheminInitial", 10f);
        }
    }

    private void RetourAuCheminInitial()
    {
        // Reset grandma's speed to normal
        Lennemi.speed = 0.5f;

        // Stop running animation and start walking animation
        Animator.SetBool("Course", false);
        Animator.SetBool("Marche", true);

        // Set grandma's destination back to pointA
        Lennemi.SetDestination(pointA.position);
    }
}
