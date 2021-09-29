using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimBoxScript : MonoBehaviour
{
    public bool onWall = false;
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
        if (other.gameObject.tag == "Wall")
            onWall = true;

        else if (other.gameObject.tag == "Ground")
            onWall = false;
    }

    //private void OnTriggerStay(Collider other)
    //{
    //    if (other.gameObject.tag == "Wall")
    //        onWall = true;
    //    else if (other.gameObject.tag == "Ground")
    //        onWall = false;
    //}
}
