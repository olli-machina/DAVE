using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{

    Rigidbody rb;
    public float speed = 10, maxVelocityChange = 10.0f, jumpForce = 800f, fallMultiplier = 2.5f;
    private Vector3 direction;
    [SerializeField]
    private bool isGrounded, walkTime;
    public bool sapRun, sapSlow;
    private GameManager gameManager;
    private float originalSpeed;
    private CheckTrigger groundTrigger;

    Animator animator;

    private bool midAir;

    Vector2 rawInput;

    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        rb = gameObject.GetComponent<Rigidbody>();
        sapRun = false;
        originalSpeed = speed;
        midAir = false;
        walkTime = true;

        groundTrigger = gameObject.GetComponentInChildren<CheckTrigger>();
        animator = GetComponentInChildren<Animator>();
    }

    void FixedUpdate()
    {
        OnMove();

        if (rb.velocity.y < 0)
        {
            if(!gameManager.isPaused && !gameManager.isGrappling)
                rb.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
    }

    private void Update()
    {
        updateGroundState();
    }


    /*
     * Purpose: Allow player to exit game by pressing esc through input manager
     * References: Input manager attached to player
     * Scripts Called: ---
     * Status: working
     */
    public void OnQuit(InputAction.CallbackContext context)
    {
        Application.Quit();
    }

    /*
     * Purpose: Allow player to re-load the current level by pressing a button
     * References: Input manager attached to player
     * Scripts Called: ---
     * Status: working
     */
    public void OnRestart(InputAction.CallbackContext context)
    {
        SceneManager.LoadScene("MergeTesting");
    }

    /*
     * Purpose: Control player movement direction plane for sap plant
     * References: Input manager attached to player
     * Scripts Called: ---
     * Status: working
     */
    public void Direction(InputAction.CallbackContext context)
    {

        if(context.started)
        {
            //GameObject.Find("SoundManager").GetComponent<SoundManager>().PlayLoop(1);
        }

        if(context.canceled)
        {
            //GameObject.Find("SoundManager").GetComponent<SoundManager>().StopLoop(1);
        }

        Vector2 inputVec = context.ReadValue<Vector2>();
        
        animator.SetFloat("targetVelocity", gameManager.animDirection);

        if (sapRun) //if running on sap wall, move up
        {
            direction = new Vector3(inputVec.x, inputVec.y, 0f);
        }
        else //if not, move forward
        {
            direction = new Vector3(inputVec.x, 0f, inputVec.y);
        }

    }

    /*
     * Purpose: Read player input to jump and execute action
     * References: Input manager attached to player
     * Scripts Called: ---
     * Status: working
     */
    public void Jump(InputAction.CallbackContext context)
    {
        if(isGrounded && !GetComponent<PlayerAim>().getIsAiming() && !(GameObject.Find("GameManager").GetComponent<GameManager>().getIsPaused()))
        {
            midAir = true;
            rb.AddForce(new Vector3(0, jumpForce, 0));
            GameObject.Find("SoundManager").GetComponent<SoundManager>().Play(0);
        }
    }

    /*
     * Purpose: Update player movement every frame for smooth controls
     * References: Update()
     * Scripts Called: ---
     * Status: working
     * Contributers: Christian Roby, Brandon L'Abbe
     */
    public void OnMove()
    {
        bool aiming = GetComponent<PlayerAim>().getIsAiming();

        if (!aiming)
        {
            if (walkTime && rb.velocity.magnitude > 1 && isGrounded)
            {
                walkTime = false;
                int walkSound = Random.Range(6, 12);
                GameObject.Find("SoundManager").GetComponent<SoundManager>().Play(walkSound, .05f);
                StartCoroutine("WalkTimeReset");
            }

            //get target velocity for player based on movement direction in Direction()
            Vector3 targetVelocity = direction;
            targetVelocity = transform.TransformDirection(targetVelocity);
            targetVelocity = targetVelocity.normalized * speed * Time.deltaTime;

            // Apply a force that attempts to reach our target velocity
            Vector3 velocity = rb.velocity;
            Vector3 velocityChange = (targetVelocity - velocity);
            velocityChange.x = Mathf.Clamp(velocityChange.x, -maxVelocityChange, maxVelocityChange);

            //if they are running up a wall of sap, z = 0
            if (sapRun)
            {
                velocityChange.y = Mathf.Clamp(velocityChange.y, -maxVelocityChange, maxVelocityChange);
                velocityChange.z = 0;
            }
            //otherwise, y = 0
            else
            {
                velocityChange.z = Mathf.Clamp(velocityChange.z, -maxVelocityChange, maxVelocityChange);
                velocityChange.y = 0;
            }
            //add the force to the player
            rb.AddForce(velocityChange, ForceMode.VelocityChange);
        }
        else
            rb.velocity = Vector3.zero;
       
    }

    
    private void updateGroundState()
    {
        if (groundTrigger.getTriggerState() && groundTrigger.getGroundCheck())
        {
            //groundTrigger.resetGround();
            isGrounded = true;
            midAir = false;
        }
        else
            isGrounded = false;
    }

    /*
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Ground")
            isGrounded = true;
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Ground")
            isGrounded = false;
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Ground")
            isGrounded = true;
    }*/

    IEnumerator WalkTimeReset()
    {
        yield return new WaitForSeconds(.5f);
        walkTime = true;
    }

    public bool IsGrounded()
    {
        return isGrounded;
    }

    public bool IsMidAir()
    {
        return midAir;
    }

}
