using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

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
    public AudioClip MusiqueChasse; // Chase music
    public AudioClip HorrorAmbient; // Ambient horror music (this is already set to play on wake and loop in AudioSource)

    private bool aDejaRit = false;

    private Transform destination; // Grandma's current destination (either pointA or pointB)

    void Start()
    {
        // Get the necessary components
        Animator = GetComponent<Animator>();
        Lennemi = GetComponent<NavMeshAgent>();
        AudioSource = GetComponent<AudioSource>();

        // Ensure HorrorAmbient is playing at the start
        AudioSource.clip = HorrorAmbient;
        AudioSource.loop = true;
        AudioSource.Play();

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

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision detected with: " + collision.gameObject.name);

        // La Vieille
        if (collision.gameObject.CompareTag("Joueur"))
        {
            Debug.Log("Le joueur a été touché par la Vieille ! Redémarrage de la scène...");
            SceneManager.LoadScene("Alejandro");
        }
    }

    /*********************************************************************************************************  
                               HANDLING TRIGGER COLLISIONS
     ******************************************************************************************************/

    private void OnTriggerEnter(Collider infosCollider)
    {
        // If grandma touches the player
        if (infosCollider.gameObject.CompareTag("Joueur") && !aDejaRit)
        {
            AudioSource.PlayOneShot(RireVieille); // Play the creepy laugh
            aDejaRit = true;
        }
    }

    private void OnTriggerStay(Collider infosCollider)
    {
        // If the player is in the trigger area, grandma will chase the player
        if (infosCollider.gameObject.CompareTag("Joueur"))
        {
            aDejaRit = false;
            // Set grandma's destination to the player's position
            Lennemi.SetDestination(infosCollider.gameObject.transform.position);
            Lennemi.speed = 1.6f; // Run faster when chasing the player
            Animator.SetBool("Course", true); // Activate running animation

            // If the music is not MusiqueChasse, switch to it
            if (AudioSource.clip != MusiqueChasse)
            {
                Debug.Log("Switching to chase music.");
                AudioSource.Stop(); // Stop the current music
                AudioSource.clip = MusiqueChasse; // Change the clip to MusiqueChasse
                AudioSource.loop = true; // Ensure it loops
                AudioSource.Play(); // Play the chase music
            }
        }
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
        // Switch back to HorrorAmbient if it's not already playing
        if (AudioSource.clip != HorrorAmbient)
        {
            Debug.Log("Switching back to ambient horror music.");
            AudioSource.Stop(); // Stop the current music
            AudioSource.clip = HorrorAmbient; // Change back to HorrorAmbient
            AudioSource.loop = true; // Ensure it loops
            AudioSource.Play(); // Play the ambient music
        }
    }
}
