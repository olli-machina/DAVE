using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

public class NewInputLook : MonoBehaviour
{
    public float turnSpeed = 15;
    public bool invertY = false;
    private CinemachineFreeLook freeLookComponent;
    Vector2 lookMovement;
    public bool aiming;
    [SerializeField]
    private GameObject aimTarget, player;
    [SerializeField]
    private CinemachineVirtualCamera aimCam;


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

    public void OnLook(InputAction.CallbackContext context)
    {
        if (!aiming)
        {
            lookMovement = context.ReadValue<Vector2>().normalized;
            lookMovement.y = invertY ? -lookMovement.y : lookMovement.y;

            lookMovement.x *= /*lookMovement.x **/ 180f;
        }

        else
        {
            Vector2 aimMovement = context.ReadValue<Vector2>();//.normalized;
            player.GetComponent<PlayerMovement>().SetAimDirection(aimMovement);
        }
    }

    public void Move()
    {
        freeLookComponent.m_XAxis.Value += lookMovement.x * turnSpeed * Time.deltaTime;
        freeLookComponent.m_YAxis.Value += lookMovement.y * turnSpeed * Time.deltaTime;
    }
}
