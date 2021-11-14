using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CheckTrigger : MonoBehaviour
{
    private bool trigger;
    private new string tag;
    private bool isGround;
    // Start is called before the first frame update
    void Start()
    {
        trigger = false;
        tag = "";
        isGround = false;
    }
    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        trigger = true;
        tag = other.gameObject.tag;
        if (tag == "Ground")
            isGround = true;

    }

    private void OnTriggerExit(Collider other)
    {
        trigger = false;
        tag = "";
        if (other.gameObject.tag == "Ground")
            isGround = false;
    }

    private void OnTriggerStay(Collider other)
    {
        trigger = true;
        tag = other.gameObject.tag;
        if (tag == "Ground")
            isGround = true;
    }

    public bool getTriggerState()
    {
        return trigger;
    }
    public string getCollidingTag()
    {
        return tag;
    }

    public bool getGroundCheck()
    {
        return isGround;
    }
}