using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

public class PlayerAttackScript : MonoBehaviour
{
    private float timer;
    private float animationTimer;

    public GameObject mainCam, aimCam, wideCam;
    public float forceMultiplier = 2;
    private float startYValue;
    private bool isShoot = false, changeCam = false;
    public GameObject aimMarker;
    private Vector3 forceV;
    [SerializeField]
    private NewInputLook aimControls;
    private CinemachineFreeLook CM_MainCam, CM_WideCam, CM_AimCam;

    private CinemachineOrbitalTransposer CMT_WideCam, CMT_AimCam, CMT_MainCam;

    public GameObject seedToShoot, shootingLine;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (animationTimer > 0)
            animationTimer -= Time.deltaTime;

        if (!isShoot && aimCam.activeInHierarchy)
        {
            Vector3 forceInit = (aimMarker.transform.position - gameObject.transform.position) * forceMultiplier * 70;

            forceV = (new Vector3(forceInit.x, forceInit.y, forceInit.z));
            //ParabolaPath.Instance.UpdateTrajectory(forceV, gameObject.GetComponent<Rigidbody>(), transform.position);
        }

        if(changeCam)
        {
            CinemachineOrbitalTransposer CM_transpose = mainCam.GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineOrbitalTransposer>();
        }
    }

    public void OnMelee(InputAction.CallbackContext context)
    {

    }

    /*
     * Purpose: Reads controller input when player tries to shoot seed
     * References: in input controller attached to player obj
     * Scripts Called: SeedParabola
     * Status: working 
     */
    public void OnShoot(InputAction.CallbackContext context)
    {
        if(context.started)
        {
            //make seed, parabola, and find aiming target box
            GameObject seed = Instantiate(seedToShoot);
            SeedParabola pathScript = seed.GetComponent<SeedParabola>();
            GameObject aimObj = GameObject.Find("Aim");

            //Set the seed at the player position and parabola end position at the aiming box
            seed.transform.position = gameObject.transform.position;
            pathScript.end = aimObj.transform.position;
        }

        
    }

    /*
     * Purpose: Reads controller input when player tries to aim in game
     * References: in input controller attached to player obj
     * Scripts Called: AimBoxScript(), NewInputLook through aimControls var
     * Status: working
     */
    public void OnAim(InputAction.CallbackContext context)
    {
        //snap aim box to be front left of player
        Vector3 moveInFront = Vector3.forward * 1.5f;
        aimMarker.transform.position = new Vector3(transform.localPosition.x + moveInFront.x, 4.75f, transform.localPosition.z + moveInFront.z + 3f);

        //change controls accordingly
        aimControls.aiming = true;

        if (context.canceled)
        {
            GameObject.Find("Aim").GetComponent<AimBoxScript>().onWall = false;
            aimControls.aiming = false;
        }
    }
}
