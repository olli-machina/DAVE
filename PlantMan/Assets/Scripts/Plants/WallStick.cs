using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallStick : MonoBehaviour
{

    public bool startForce;
    public Animator animator;
    public BoxCollider wallCollider;
    bool IsGrowing;
    public GameObject platformCollider;
    public GameObject wallPoisonCollider, groundPoisonCollider;

    // Start is called before the first frame update
    void Start()
    {
        if(startForce)
            GetComponent<Rigidbody>().AddForce(Vector3.right * 1000);

        animator = GetComponent<Animator>();
        IsGrowing = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Wall" && !IsGrowing) //Make this object stick to a wall
        {
            wallCollider.enabled = true;
            animator.SetBool("OnWall", true);

            wallPoisonCollider.GetComponent<BoxCollider>().enabled = true;
            groundPoisonCollider.GetComponent<BoxCollider>().enabled = false;

            Rigidbody rb = GetComponent<Rigidbody>();
            rb.velocity = Vector3.zero;
            rb.useGravity = false;
            rb.mass = 999999999; //If the mass is relatively huge, it will not be able to be moved by forces.

            float multiplier = other.transform.right.z * -90;
            transform.rotation = Quaternion.Euler(other.transform.up * multiplier);

            platformCollider.GetComponent<BoxCollider>().center = new Vector3(-0.0337826f, 1.12f, 1.21f);

            IsGrowing = true;
        }
    }
}
