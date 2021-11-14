using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAim : MonoBehaviour
{

    public float startingTheta = 45;
    public float maximumAngleOffset;

    public float maxForce;

    public float timeToCharge;

    public GameObject seedToShoot;
    public GameObject aimLine;
    public GameObject chargeLine;
    public int numOfLinePoints = 30;

    public Vector3 aimOffset;

    public GameObject activeCamera;
    public float perspectiveMultiply;

    private GameManager gameManager;

    private bool isAiming;
    private float theta;
    private float force;
    private float timer;
    private bool isCharging;

    public float aimLineStartPointAlpha = 0.6f;
    public float aimLineEndPointAlpha = 0f;

    // Start is called before the first frame update
    void Start()
    {
        isAiming = false;
        theta = startingTheta;
        force = 0f;
        timer = 0f;
        isCharging = false;

        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        ChargeShot();
        UpdateLine();

        if(gameManager.isPaused)
        {
            isAiming = false;
            isCharging = false;
            timer = 0f;
            if(aimLine != null)
                aimLine.SetActive(false);
        }
    }

    /*
    * Purpose: When the player is shooting, charge a shot to be fired.
    * References: Update()
    * Scripts Called: None
    * Status: in progress
    * Contributor(s): Brandon L'Abbe
    */
    void ChargeShot()
    {
        if (!gameManager.isPaused)
        {
            if (isCharging)
            {
                if (timer < timeToCharge)
                    timer += Time.deltaTime;

                if (timer > timeToCharge)
                    timer = timeToCharge;

                force = maxForce * (timer / timeToCharge);
            }
        }
    }

    /*
     * Purpose: When the player shoots, fire a seed using the angle and force
     * References: in input controller attached to player obj
     * Scripts Called: None
     * Status: in progress
     * Contributor(s): Brandon L'Abbe
     */
    public void OnShoot(InputAction.CallbackContext context)
    {
        if (!gameManager.isPaused)
        {
            if (isAiming)
            {
                if (context.performed)
                {
                    isCharging = true;
                }

                if (context.canceled)
                {
                    GameObject.Find("SoundManager").GetComponent<SoundManager>().Play(1, .5f);
                    Vector3 dir = new Vector3(transform.forward.x * Mathf.Cos(theta * Mathf.Deg2Rad), Mathf.Sin(theta * Mathf.Deg2Rad), transform.forward.z * Mathf.Cos(theta * Mathf.Deg2Rad));
                    dir.Normalize();
                    dir *= force;


                    GameObject seed = Instantiate(seedToShoot);
                    seed.transform.position = gameObject.transform.position + getOffset();
                    seed.GetComponent<Rigidbody>().velocity = dir;

                    isCharging = false;
                    timer = 0f;
                }
            }
        }

    }

    /*
     * Purpose: Determine whether the player is aiming.
     * References: in input controller attached to player obj
     * Scripts Called: None
     * Status: in progress
     * Contributor(s): Brandon L'Abbe
     */
    public void OnAim(InputAction.CallbackContext context)
    {
        if (!gameManager.isPaused)
        {
            if (GetComponent<PlayerMovement>().IsGrounded())
            {
                isAiming = true;
                TurnOnLine();
            }

            if (context.canceled)
            {
                isAiming = false;
                isCharging = false;
                timer = 0f;
                aimLine.SetActive(false);
            }
        }
    }

    void TurnOnLine()
    {
        if (!gameManager.isPaused)
        {
            if (isAiming)
            {
                //Get Theta Value
                float angleOffset = (activeCamera.GetComponent<CinemachineFreeLook>().m_YAxis.Value - 0.5f) * 2 * maximumAngleOffset;
                theta = startingTheta - angleOffset;

                aimLine.SetActive(true);
                LineRenderer lr = aimLine.GetComponent<LineRenderer>();

                float xForce = maxForce * Mathf.Cos(theta * Mathf.Deg2Rad);
                float yForce = maxForce * Mathf.Sin(theta * Mathf.Deg2Rad);
                float time = 2 * yForce / -Physics.gravity.y;

                float timeScale = time / numOfLinePoints;
                Vector3[] pos = new Vector3[numOfLinePoints + 5];

                pos[0] = transform.position + getOffset();
                for (int i = 1; i < numOfLinePoints + 5; i++)
                {
                    float t = timeScale * i;

                    Vector3 relPos = transform.forward * (xForce * t);
                    relPos.y = (yForce * t) + 0.5f * Physics.gravity.y * t * t;

                    pos[i] = transform.position + relPos + getOffset();
                }


                lr.SetPositions(pos);
            }
        }
    }

    /*
     * Purpose: Set the visual parabola to match the fire path of the seed
     * References: in input controller attached to player obj
     * Scripts Called: None
     * Status: in progress
     * Contributor(s): Brandon L'Abbe
     */
    public void OnLook(InputAction.CallbackContext context)
    {
        if(isAiming)
        {
            //Get Theta Value
            float angleOffset = (activeCamera.GetComponent<CinemachineFreeLook>().m_YAxis.Value - 0.5f) * 2 * maximumAngleOffset;
            theta = startingTheta - angleOffset;

            aimLine.SetActive(true);
            LineRenderer lr = aimLine.GetComponent<LineRenderer>();

            float xForce = maxForce * Mathf.Cos(theta * Mathf.Deg2Rad);
            float yForce = maxForce * Mathf.Sin(theta * Mathf.Deg2Rad);
            float time = 2 * yForce / -Physics.gravity.y;

            float timeScale = time / numOfLinePoints;
            Vector3[] pos = new Vector3[numOfLinePoints + 5];

            pos[0] = transform.position + getOffset();
            for (int i = 1; i < numOfLinePoints + 5; i++)
            {
                float t = timeScale * i;

                Vector3 relPos = transform.forward * (xForce * t);
                relPos.y = (yForce * t) + 0.5f * Physics.gravity.y * t * t;

                pos[i] = transform.position + relPos + getOffset();
            }


            lr.SetPositions(pos);
        }
        else
        {
            aimLine.SetActive(false);
        }
    }

    /*
    * Purpose: Set the visual parabola to match the fire path of the seed
    * References: None
    * Scripts Called: None
    * Status: in progress
    * Contributor(s): Brandon L'Abbe
    */
    void UpdateLine()
    {
        if (isCharging)
        {
            OnLook(new InputAction.CallbackContext());

            //Get Theta Value
            float angleOffset = (activeCamera.GetComponent<CinemachineFreeLook>().m_YAxis.Value - 0.5f) * 2 * maximumAngleOffset;
            theta = startingTheta - angleOffset;

            LineRenderer alr = aimLine.GetComponent<LineRenderer>();
            Color c = alr.startColor;
            c.a = aimLineStartPointAlpha;
            alr.startColor = c;

            c = alr.endColor;
            c.a = aimLineEndPointAlpha;
            alr.endColor = c;

            chargeLine.SetActive(true);
            LineRenderer lr = chargeLine.GetComponent<LineRenderer>();

            float xForce = force * Mathf.Cos(theta * Mathf.Deg2Rad);
            float yForce = force * Mathf.Sin(theta * Mathf.Deg2Rad);
            float time = 2 * yForce / -Physics.gravity.y;

            float timeScale = time / numOfLinePoints;
            Vector3[] pos = new Vector3[numOfLinePoints + 5];

            pos[0] = transform.position + getOffset();
            for (int i = 1; i < numOfLinePoints + 5; i++)
            {
                float t = timeScale * i;

                Vector3 relPos = transform.forward * (xForce * t);
                relPos.y = (yForce * t) + 0.5f * Physics.gravity.y * t * t;

                pos[i] = transform.position + relPos + getOffset();
            }


            lr.SetPositions(pos);
        }
        else
        {
            if(chargeLine != null)
                chargeLine.SetActive(false);
        }
    }

    /*
     * Purpose: Getter function for private isAiming variable
     * References: None
     * Scripts Called: None
     * Status: working
     * Contributor(s): Brandon L'Abbe
     */
    public bool getIsAiming()
    {
        return isAiming;
    }

    Vector3 getOffset()
    {
        Vector3 x = transform.right;
        x *= aimOffset.x;
        Vector3 y = transform.up;
        y *= aimOffset.y;
        Vector3 z = transform.forward;
        z *= aimOffset.z;
        return x + y + z;
    }

}
