using System.Collections;

using UnityEngine;

using UnityEngine.SceneManagement;

public class GestionJoueur : MonoBehaviour
{
  //  public float VitesseDeplacementPersonnage;//vitesse à la quelle le personnage se déplacera
  //  public float VitesseRotationPersonnage;// vitesse à laquelle le personnage tournera

    private Rigidbody RigidPersonnage;
    private Animator AnimPersonnage;
    private AudioSource AudioPersonnage;



    //mes objets
   
    public GameObject Porte; //le game object qui contient l'animation de la porte qui s'ouvre
    private GameObject PorteActuelle; // La porte que le joueur manipulera en temps présent.



    //Les sons
  
    public AudioClip SonPorteOuvre; //le son d'ouverture de la porte
    public AudioClip SonFermePorte; // le son de fermeture de las porte
 




    // Start is called before the first frame update
    void Start()
    {
        RigidPersonnage = GetComponent<Rigidbody>();
        AnimPersonnage = GetComponent<Animator>();
        AudioPersonnage = GetComponent<AudioSource>();

  

    }


    void Update()
    {







        /***********************************************************************************************************
                                           GESTION DE LA COURSE 
        ********************************************************************************************************


        if (Input.GetKeyDown(KeyCode.Space))// si je pèsse sur la barre d'espacement
        {
            VitesseDeplacementPersonnage = VitesseDeplacementPersonnage * 2; //je multiplie la vitesse x 2
            AudioPersonnage.pitch = 1.30f; // le son marche devient plus rapide
            Stamina = true; // il court, sa stamina commence à baiser

            if (RefStamina.GetComponent<GestionBarreStamina>().CompteurValeurActuelle == 0) //si ç'atteint 0, tu marches mon gars
            {
                VitesseDeplacementPersonnage = VitesseDeplacementPersonnage / 2; //la vitesse redevient à la normale
                AudioPersonnage.pitch = 1;//le son marche revient à la normale
                Stamina = false;// sa Stamina remonte
            }
        }
        else if (Input.GetKeyUp(KeyCode.Space)) // si je relâche la barre d'espacement
        {
            VitesseDeplacementPersonnage = VitesseDeplacementPersonnage / 2; //la vitesse redevient à la normale
            AudioPersonnage.pitch = 1;//le son marche revient à la normale
            Stamina = false;// sa Stamina remonte
        }



   

        ***/

    }
    /*********************************************************************************************************  
                                        GESTIONNARE DE COLLISIONS DE TYPE TRIGGER EMTER
     ******************************************************************************************************/


    private void OnTriggerEnter(Collider infosCollider)
    {


        //la porte

        if (infosCollider.gameObject.tag == "Porte") // si le personnage rentre en contact avec le tag porte
            PorteActuelle = infosCollider.gameObject; // On parle de quelle porte?
        print("Porte actuelle : " + PorteActuelle.gameObject.name);
        {
                PorteActuelle.GetComponent<Animator>().SetBool("Ouvre", true);
                GetComponent<AudioSource>().PlayOneShot(SonPorteOuvre);
               // Invoke("RefermerPorte", 3f);
                PorteActuelle.GetComponent<Collider>().enabled = false; // Désactivation du collider
                // si je laisse le collider, si on reste collé à la porte, la collision se produit et le son ouvrir porte joue à l'infini
            }

        


   
    }



    /*********************************************************************************************************  
                                      LES FUNCTIONS PERSONNALISÉES
    ****************************************************************************************************





    // pour que les portes se ferme... ceci ne s'applique pas aux mannequins, car leurs mouvements sont silencieux.

    void RefermerPorte()
    {
        // Utiliser la porte actuelle
        if (PorteActuelle != null)
        {
            PorteActuelle.GetComponent<Animator>().SetBool("Ouvre", false);
            PorteActuelle.GetComponent<Collider>().enabled = true; // Réactivation du collider
            GetComponent<AudioSource>().PlayOneShot(SonPorteFerme);//je fais jouer le son de fermeture de porte
        }
    }




    }**/
}