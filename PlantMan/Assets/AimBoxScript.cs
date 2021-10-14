using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimBoxScript : MonoBehaviour
{
    public bool onWall = false;
    public Vector3 rotationMovement;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        rotationMovement = Quaternion.Euler(0, 45, 0) * Vector3.forward;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Wall")
        {
            onWall = true;
            gameObject.transform.rotation = other.transform.rotation;
        }

        else if (other.gameObject.tag == "Ground")
        {
            onWall = false;
            gameObject.transform.position = new Vector3(gameObject.transform.position.x, 4.25f, gameObject.transform.position.z);
        }
    }

}
