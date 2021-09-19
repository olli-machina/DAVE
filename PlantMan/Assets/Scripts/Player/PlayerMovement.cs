using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{

    Rigidbody rb;
    public float speed = 10, maxVelocityChange = 10.0f;
    private Vector3 direction;
    //public bool end = false;
    //GameObject gameManager;

    private void Start()
    {
        //gameManager = GameObject.Find("GameManager");
        rb = gameObject.GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        OnMove();

        /*
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        myRB.velocity = new Vector3(horizontal * speed, myRB.velocity.y, vertical * speed);
        */

        //Vector3 targetVelocity = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        //targetVelocity = transform.TransformDirection(targetVelocity);
        //targetVelocity = targetVelocity.normalized * speed * Time.deltaTime;

        //// Apply a force that attempts to reach our target velocity
        //Vector3 velocity = myRB.velocity;
        //Vector3 velocityChange = (targetVelocity - velocity);
        //velocityChange.x = Mathf.Clamp(velocityChange.x, -maxVelocityChange, maxVelocityChange);
        //velocityChange.z = Mathf.Clamp(velocityChange.z, -maxVelocityChange, maxVelocityChange);
        //velocityChange.y = 0;
        //myRB.AddForce(velocityChange, ForceMode.VelocityChange);
    }

    public void Direction(InputAction.CallbackContext context)
    {
        Vector2 inputVec = context.ReadValue<Vector2>();

        direction = new Vector3(inputVec.x, 0f, inputVec.y);        
    }

    public void OnMove()
    {
        //Vector3 newVelocity = direction * speed;
        //rb.velocity = newVelocity;
        //rb.velocity = new Vector3(horizontal * speed, myRB.velocity.y, vertical * speed);

        /*
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        myRB.velocity = new Vector3(horizontal * speed, myRB.velocity.y, vertical * speed);
        */

        Vector3 targetVelocity = direction;//new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        targetVelocity = transform.TransformDirection(targetVelocity);
        targetVelocity = targetVelocity.normalized * speed * Time.deltaTime;

        // Apply a force that attempts to reach our target velocity
        Vector3 velocity = rb.velocity;
        Vector3 velocityChange = (targetVelocity - velocity);
        velocityChange.x = Mathf.Clamp(velocityChange.x, -maxVelocityChange, maxVelocityChange);
        velocityChange.z = Mathf.Clamp(velocityChange.z, -maxVelocityChange, maxVelocityChange);
        velocityChange.y = 0;
        rb.AddForce(velocityChange, ForceMode.VelocityChange);
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if(other.name == "TimerDone")
    //    {
    //        end = true;
    //        gameManager.GetComponent<Timer>().end = true;
    //    }
    //}
}
