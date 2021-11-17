using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MeleeColliderCheck : MonoBehaviour
{
    public bool plantInRange;
    public GameObject otherPlant;
    private float deathTimer;
    public float timeToDeath = 3f;


    private void Start()
    {
        deathTimer = 0f;
    }
    private void Update()
    {
        deathTimer += Time.deltaTime;
        if (deathTimer > timeToDeath)
        {
            Destroy(gameObject);
        }
    }

    /*
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
        if(gameObject.tag == "Seed")
        {
            if(plantInRange)
            {
                Destroy(otherPlant);
                Destroy(gameObject);
            }

        }
        else
        {
            plantInRange = false;
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
