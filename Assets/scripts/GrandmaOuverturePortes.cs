using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrandmaOuverturePortes : MonoBehaviour
{
    private Animator Animator; // To control animations
    private AudioSource AudioSource; // For sound effects
    private GameObject PorteActuelle; // To handle doors when grandma touches a door

    public AudioClip SonPorteOuvre; // Door opening sound
    public AudioClip SonFermePorte; // Door closing sound
    private bool porteEnTrainDOuvrir = false;





    // Start is called before the first frame update
    void Start()
    {
        Animator = GetComponent<Animator>();
       
        AudioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


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






}