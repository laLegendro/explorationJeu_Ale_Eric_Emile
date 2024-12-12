using System.Collections;
using UnityEngine;


public class GestionJoueur : MonoBehaviour
{
    private Rigidbody RigidPersonnage;
    private Animator AnimPersonnage;
    private AudioSource AudioPersonnage;

    // Mes objets
    public GameObject Porte; // Le game object qui contient l'animation de la porte qui s'ouvre
    private GameObject PorteActuelle; // La porte que le joueur manipulera en temps présent.

    // Les sons
    public AudioClip SonPorteOuvre; // Le son d'ouverture de la porte
    public AudioClip SonFermePorte; // Le son de fermeture de la porte

    // Flag pour éviter que la porte se réouvre ou se referme plusieurs fois
    private bool porteEnTrainDOuvrir = false;

    void Start()
    {
        RigidPersonnage = GetComponent<Rigidbody>();
        AnimPersonnage = GetComponent<Animator>();
        AudioPersonnage = GetComponent<AudioSource>();
    }



    



    /*********************************************************************************************************
                        GESTION DES COLLISIONS DE TYPE TRIGGER ENTER
     ******************************************************************************************************/
    private void OnTriggerEnter(Collider infosCollider)
    {
        // La porte
        if (infosCollider.gameObject.CompareTag("Porte") && !porteEnTrainDOuvrir)
        {
            PorteActuelle = infosCollider.gameObject; // On parle de quelle porte ?

            // Ouvrir la porte
            PorteActuelle.GetComponent<Animator>().SetBool("Ouvre", true);
            AudioPersonnage.PlayOneShot(SonPorteOuvre);

            // Marquer la porte comme étant en train d'ouvrir
            porteEnTrainDOuvrir = true;

            // Fermer la porte après 4 secondes
            Invoke("RefermerPorte", 4f);

            // Désactiver le collider de la porte pour éviter des collisions répétées
            PorteActuelle.GetComponent<Collider>().enabled = false;
        }
    }

    /*********************************************************************************************************
                                      LES FONCTIONS PERSONNALISÉES
    ******************************************************************************************************/
    // Pour que la porte se ferme
    void RefermerPorte()
    {
        // Utiliser la porte actuelle
        if (PorteActuelle != null)
        {
            PorteActuelle.GetComponent<Animator>().SetBool("Ouvre", false);
            PorteActuelle.GetComponent<Collider>().enabled = true; // Réactiver le collider de la porte
            AudioPersonnage.PlayOneShot(SonFermePorte); // Jouer le son de fermeture de porte
        }

        // Réinitialiser le flag pour permettre une nouvelle interaction
        porteEnTrainDOuvrir = false;
    }
}
