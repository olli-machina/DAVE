using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallStick : MonoBehaviour
{

    public bool startForce;
    // Start is called before the first frame update
    void Start()
    {
        if(startForce)
            GetComponent<Rigidbody>().AddForce(Vector3.right * 1000);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Wall") //Make this object stick to a wall
        {
            Debug.Log("wall" + collision.transform.right);
            Rigidbody rb = GetComponent<Rigidbody>();
            rb.velocity = Vector3.zero;
            rb.useGravity = false;
            rb.mass = 999999999; //If the mass is relatively huge, it will not be able to be moved by forces.
            transform.rotation = Quaternion.Euler(90f * collision.transform.right);
            //Vector3 vel = collision.contacts[0].normal * -90; //Makes the seed face the normal vector to the wall.
            //rb.rotation = Quaternion.Euler(vel.y, vel.z, vel.x);//Makes the seed face the normal vector to the wall.
        }
    }
}
