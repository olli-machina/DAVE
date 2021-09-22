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
        if(collision.gameObject.tag == "Wall")
        {
            Rigidbody rb = GetComponent<Rigidbody>();
            rb.velocity = Vector3.zero;
            rb.useGravity = false;
            rb.mass = 999999999;
            Vector3 vel = collision.contacts[0].normal * -90;
            //vel = new Vector3(0.0f, 0.0f, 90.0f);
            rb.rotation = Quaternion.Euler(vel.y, vel.z, vel.x);
        }
    }
}
