using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//script for the sap obj to send messages to the player
public class SapObjScript : MonoBehaviour
{
    public bool wallSap;
    public float newDrag;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (wallSap)
            {
                other.GetComponent<PlayerMovement>().sapRun = true;
            }
            else
            {
                if (other.GetComponent<Rigidbody>() != null)
                {
                    other.GetComponent<Rigidbody>().drag = newDrag;
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            if (wallSap)
            {
                other.GetComponent<PlayerMovement>().sapRun = false;
            }
            else
            {
                if(other.GetComponent<Rigidbody>() != null)
                {
                    other.GetComponent<Rigidbody>().drag = 0f;
                }
            }
        }
    }
}
