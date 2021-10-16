using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimBoxScript : MonoBehaviour
{
    public bool onWall = false;
    public Vector3 rotationMovement;

    // Update is called once per frame
    void Update()
    {
        //for which direction cube moves, default forward
        rotationMovement = Quaternion.Euler(0, 45, 0) * Vector3.forward;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Wall") //if on the wall, change rotation to match the wall rotation
        {
            onWall = true;
            gameObject.transform.rotation = other.transform.rotation;
        }

        else if (other.gameObject.tag == "Ground") //get off wall and reset to floor height
        {
            onWall = false;
            gameObject.transform.position = new Vector3(gameObject.transform.position.x, 4.75f, gameObject.transform.position.z);
        }
    }

}
