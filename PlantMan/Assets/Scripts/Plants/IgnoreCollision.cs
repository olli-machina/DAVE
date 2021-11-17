using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgnoreCollision : MonoBehaviour
{
    public GameObject stem;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.parent.gameObject.layer == 10)
        {
            Physics.IgnoreCollision(gameObject.GetComponent<Collider>(), collision.collider);
        }

        if(gameObject.tag == "Ground")
        {
            Vector3 newPos = new Vector3(stem.transform.position.x, stem.transform.position.y + 2f, stem.transform.position.z);
            gameObject.GetComponent<Collider>().transform.position = newPos;

        }
    }
}
