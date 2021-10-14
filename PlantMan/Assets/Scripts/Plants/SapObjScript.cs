using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SapObjScript : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Be fast");
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
