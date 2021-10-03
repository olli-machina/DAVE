using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{

    Rigidbody rb;
    public float speed = 10, maxVelocityChange = 10.0f, jumpForce = 800f;
    private Vector3 direction, aimMovement;
    [SerializeField]
    private bool isGrounded;
    public GameObject aimTarget;
    bool changedInput;

    bool isOnWall;

    Vector2 rawInput;
    //GameObject gameManager;

    private void Start()
    {
        //gameManager = GameObject.Find("GameManager");
        rb = gameObject.GetComponent<Rigidbody>();
        isOnWall = false;
    }

    void FixedUpdate()
    {
        OnMove();
        MoveTarget();
    }

    public void SetAimDirection(Vector2 input)
    {
        changedInput = true;

        rawInput = input;

       // Vector3 direction = transform.forward.;

        if (aimTarget.GetComponent<AimBoxScript>().onWall)
        {
            aimMovement = new Vector3(rawInput.x, rawInput.y, 0);
            isOnWall = true;
        }
        else
        {
            aimMovement = new Vector3(rawInput.x, 0, rawInput.y);
            isOnWall = false;
        }
    }

    public void OnQuit(InputAction.CallbackContext context)
    {
        Application.Quit();
    }

    public void OnRestart(InputAction.CallbackContext context)
    {
        SceneManager.LoadScene("MergeTesting");
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

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = true;
        }
    }

    public void MoveTarget()
    {

        //Vector3 playerForward = Vector3.ProjectOnPlane(transform.forward, Vector3.up);
        //Vector3 rotateMovement = new Vector3(aimMovement.x * playerForward.x, 0.0f, aimMovement.z * playerForward.z);
        //Quaternion rotationToCamera = Quaternion.LookRotation(rotateMovement, Vector3.up);
        //                                         //Vector3 rotateMovement = new Vector3(aimMovement.x, 0.0f, aimMovement.z);
        //Vector3 moveDir = new Vector3(rotateMovement.x * aimMovement.normalized.x, 0, rotateMovement.z * aimMovement.normalized.z);

        //if (changedInput)
        //{
        //    rotateMovement = rotationToCamera * rotateMovement;
        //    changedInput = false;
        //}

        //                                     //aimMovement = new Vector3(rotateMovement.x, aimMovement.y, rotateMovement.z);

        //                                     //aimTarget.transform.position += aimMovement * 10f * Time.deltaTime;

        
        float angle = Vector3.Angle(aimTarget.transform.position, transform.position);
        aimTarget.transform.rotation = Quaternion.Euler(0, angle, 0);
        //Vector3 forward = Vector3.ProjectOnPlane(aimTarget.transform.forward, Vector3.up);
        Vector3 moveDir = new Vector3(0, aimMovement.y, aimTarget.transform.forward.z * aimMovement.z);

        aimTarget.transform.localPosition += moveDir * Time.deltaTime * 10f;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.black; //ray facing forward
        Gizmos.DrawRay(aimTarget.transform.position, aimTarget.transform.forward);

    }


    /* Old Attempt
     *         if(aimTarget.GetComponent<AimBoxScript>().onWall ^ isOnWall)
        {
            return;
        }

        //Vector3 cameraForward = Vector3.ProjectOnPlane(Camera.main.transform.forward, Vector3.up);
        Vector3 playerForward = Vector3.ProjectOnPlane(transform.forward, Vector3.up);
                                                 //Vector3 rotateMovement = new Vector3(aimMovement.x * cameraForward.x, 0.0f, aimMovement.z * cameraForward.z);
                                                 //Quaternion rotationToCamera = Quaternion.LookRotation(cameraForward, Vector3.up);
                                                 //Vector3 rotateMovement = new Vector3(aimMovement.x, 0.0f, aimMovement.z);
       // Vector3 moveDir = new Vector3(playerForward.x * aimMovement.normalized.x, 0, playerForward.z * aimMovement.normalized.z);

        if (changedInput)
        {
                                               //   rotateMovement = rotationToCamera * rotateMovement;
            changedInput = false;
        }

                                             //aimMovement = new Vector3(rotateMovement.x, aimMovement.y, rotateMovement.z);

                                             //aimTarget.transform.position += aimMovement * 10f * Time.deltaTime;

       // aimTarget.transform.position += moveDir * Time.deltaTime * 10f;
     */
}
