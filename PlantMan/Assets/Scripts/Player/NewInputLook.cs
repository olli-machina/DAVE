using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;
using UnityEngine.Animations;

public class NewInputLook : MonoBehaviour
{
    public float turnSpeed = 15;
    public bool invertY = false;
    private CinemachineFreeLook freeLookComponent;
    Vector2 lookMovement;


    // Start is called before the first frame update
    void Start()
    {
        freeLookComponent = GetComponent<CinemachineFreeLook>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Move();
    }

    /**
     * Purpose: If player moves camera, input system calls this and sets direction for player
     * References: in scene attached to cameras, called by player input on player object
     * Scripts Called: PlayerMovement from player obj
     * Status: working 
     */
    public void OnLook(InputAction.CallbackContext context)
    {
        lookMovement = context.ReadValue<Vector2>().normalized; //read values
        lookMovement.y = invertY ? -lookMovement.y : lookMovement.y; //set y camera movement

        lookMovement.x *= 180f; //set x camera movement
    }

    /**
     * Purpose: Moves the player every frame according to direction set in OnLook()
     * References: Update()
     * Scripts Called: ---
     * Status: working 
     */
    public void Move()
    {
        freeLookComponent.m_YAxis.Value += lookMovement.y * turnSpeed * Time.deltaTime;
        freeLookComponent.m_XAxis.Value += lookMovement.x * turnSpeed * Time.deltaTime;
    }
}
