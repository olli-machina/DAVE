using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantDrag : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Ground")
            Debug.Log(other.gameObject.name + GetComponent<Collider>().GetType());
        if(GetComponent<Collider>().GetType() == typeof(SphereCollider) && other.gameObject.layer == 8)
        {
            gameObject.GetComponent<Rigidbody>().drag = 30f;
        }

        if(other.gameObject.tag == "Seed" || other.gameObject.tag == "Plant")
        {
            Debug.Log(other.gameObject.name);
            Physics.IgnoreCollision(gameObject.GetComponentInChildren<MeshCollider>(), other);
        }

    }
}
