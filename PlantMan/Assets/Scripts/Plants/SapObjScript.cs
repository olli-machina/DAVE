using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//script for the sap obj to send messages to the player
public class SapObjScript : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.GetComponent<PlayerMovement>().sapRun = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            other.GetComponent<PlayerMovement>().sapRun = false;
        }
    }
}
