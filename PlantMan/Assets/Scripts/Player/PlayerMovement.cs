using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{

    Rigidbody rb;
    public float speed = 10, maxVelocityChange = 10.0f, jumpForce = 800f;
    private Vector3 direction;
    private bool isGrounded;
    //GameObject gameManager;

    private void Start()
    {
        //gameManager = GameObject.Find("GameManager");
        rb = gameObject.GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        OnMove();
    }

    public void Direction(InputAction.CallbackContext context)
    {
        Vector2 inputVec = context.ReadValue<Vector2>();

        direction = new Vector3(inputVec.x, 0f, inputVec.y);
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if(isGrounded)
            rb.AddForce(new Vector3(0, jumpForce, 0));
    }

    public void OnMove()
    {
       
        Vector3 targetVelocity = direction;
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

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = false;
        }
    }

}
