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
    private GameObject aimTarget;
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
            Vector3 cameraForward = Vector3.ProjectOnPlane(aimCam.transform.forward, Vector3.up);
            Quaternion rotationToCamera = Quaternion.LookRotation(cameraForward, Vector3.up);

            Vector3 rotateMovement = new Vector3(aimMovement.x, 0.0f, aimMovement.y);
            rotateMovement = rotationToCamera * rotateMovement;

            aimMovement = new Vector2(rotateMovement.x, rotateMovement.z);

            aimTarget.transform.localPosition += (new Vector3(-aimMovement.x, 0, -aimMovement.y).normalized) * .25f;
        }
    }

    public void Move()
    {
        freeLookComponent.m_XAxis.Value += lookMovement.x * turnSpeed * Time.deltaTime;
        freeLookComponent.m_YAxis.Value += lookMovement.y * turnSpeed * Time.deltaTime;
    }
}
