using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MeleeColliderCheck : MonoBehaviour
{
    public bool plantInRange; /**< Is there a plant for the player to destroy*/
    public GameObject otherPlant; /**< Hold the reference to the plant parent*/

    /**
     * Purpose: if the player presses melee input and there is a plant, destroy it \n
     * References: in input controller attached to player obj \n
     * Scripts Called: --- \n
     * Status: working \n
     * Contributor(s): Olli Machina
     */
    public void DestroyPlant(InputAction.CallbackContext context)
    {
        if(plantInRange)
        {
            Destroy(otherPlant);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Plant")
        {
            plantInRange = true;
            otherPlant = other.transform.parent.gameObject; //get the parent to destroy the whole plant
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Plant")
        {
            plantInRange = false;
            otherPlant = null;
        }
    }
}
