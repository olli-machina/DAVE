using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CheckTrigger : MonoBehaviour
{
    private bool trigger;
    private new string tag;
    // Start is called before the first frame update
    void Start()
    {
        trigger = false;
        tag = "";
    }
    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        trigger = true;
        tag = other.gameObject.tag;
    }
    private void OnTriggerExit(Collider other)
    {
        trigger = false;
        tag = "";
    }
    private void OnTriggerStay(Collider other)
    {
        trigger = true;
        tag = other.gameObject.tag;
    }

    public bool getTriggerState()
    {
        return trigger;
    }
    public string getCollidingTag()
    {
        return tag;
    }
}