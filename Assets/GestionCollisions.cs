using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestionCollisions : MonoBehaviour
{
    // Time in seconds before the door closes
    public float delayBeforeClosing = 5f;

    // Name of the door GameObject (optional, you can use tags instead)
    public string doorTag = "Door";

    // This function is called when deplacementJoueur collides with another object
    private void OnCollisionEnter(Collision collision)
    {
        // Check if the object that was hit is tagged as "Door"
        if (collision.gameObject.CompareTag(doorTag))
        {
            // Open the door by enabling the door's "PorteOuvre" if it exists
            DoorController doorController = collision.gameObject.GetComponent<DoorController>();
            if (doorController != null && !doorController.PorteOuvre)
            {
                // Open the door
                doorController.PorteOuvre = true;
                Debug.Log("Door is now open!");

                // Start the coroutine to close the door after a delay
                StartCoroutine(CloseDoorAfterDelay(doorController));
            }
        }
    }

    // Coroutine to close the door after a delay
    private IEnumerator CloseDoorAfterDelay(DoorController doorController)
    {
        // Wait for the specified delay time
        yield return new WaitForSeconds(delayBeforeClosing);

        // Close the door
        if (doorController != null)
        {
            doorController.PorteOuvre = false;
            Debug.Log("Door is now closed!");
        }
    }
}

Dispose d’un menu contextuel

