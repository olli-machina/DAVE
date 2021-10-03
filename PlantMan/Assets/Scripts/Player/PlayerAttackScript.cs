using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

public class PlayerAttackScript : MonoBehaviour
{
    /*Old Stuff
    public float tridentStabDamage;
    public GameObject tridentStabModel;
    public float tridentStabAnimationTime;
    public float tridentStabCooldown;

    public float tridentSwipeDamage;
    public GameObject tridentSwipeModel;
    public float tridentSwipeAnimationTime;
    public float tridentSwipeCooldown;

    public GameObject throwableTrident;
    public float tridentThrowForce;
    public float tridentThrowDamage;

    private List<GameObject> stabEnemiesInRange;
    private List<GameObject> swipeEnemiesInRange;

    private bool tridentFree;
    private bool tridentStab;
    private bool tridentSwipe;
    */
    private float timer;
    private float animationTimer;

    public GameObject mainCam, aimCam, wideCam;
    public float forceMultiplier = 2;
    private float startYValue;
    private bool isShoot = false;
    public GameObject aimMarker;
    private Vector3 forceV;
    [SerializeField]
    private NewInputLook aimControls;
    private CinemachineFreeLook CM_MainCam, CM_WideCam, CM_AimCam;

    public GameObject seedToShoot, shootingLine;

    // Start is called before the first frame update
    void Start()
    {
        /* Old Stuff
        stabEnemiesInRange = new List<GameObject>();
        swipeEnemiesInRange = new List<GameObject>();
        */
        CM_MainCam = mainCam.GetComponent<CinemachineFreeLook>();
        CM_WideCam = wideCam.GetComponent<CinemachineFreeLook>();
        CM_AimCam = aimCam.GetComponent<CinemachineFreeLook>();

        startYValue = CM_MainCam.m_YAxis.Value;
    }

    // Update is called once per frame
    void Update()
    {
        //cleanupLists();

        timer += Time.deltaTime;

        if (animationTimer > 0)
            animationTimer -= Time.deltaTime;

        if (!isShoot && aimCam.activeInHierarchy)
        {
            //Vector3 range = aimMarker.transform.position - gameObject.transform.position;
            //range.y = 0;

            //float InitialVelocity = Mathf.Sqrt(-Physics.gravity.y * range.magnitude);
            //Debug.Log(InitialVelocity);
            Vector3 forceInit = (aimMarker.transform.position - gameObject.transform.position) * forceMultiplier * 70;

            forceV = (new Vector3(forceInit.x, forceInit.y, forceInit.z));
            //ParabolaPath.Instance.UpdateTrajectory(forceV, gameObject.GetComponent<Rigidbody>(), transform.position);
        }
/* Old Stuff
        if (tridentStab && timer > tridentStabCooldown ||
            tridentSwipe && timer > tridentSwipeCooldown)
        {
            tridentStab = false;
            tridentSwipe = false;
            tridentFree = true;

            timer = 0.0f;
        }

        if (animationTimer <= 0)
        {
            tridentStabModel.SetActive(false);
            tridentSwipeModel.SetActive(false);
        }
*/
    }

    public void OnMelee(InputAction.CallbackContext context)
    {
        /* Old Stuff
        if (tridentFree)
        {
            tridentFree = false;
            tridentStab = true;

            foreach (GameObject e in stabEnemiesInRange)
            {
                e.GetComponent<EnemyScript>().Damage(tridentStabDamage);
            }
            tridentStabModel.SetActive(true);
            animationTimer = tridentStabAnimationTime;

            timer = 0.0f;
        }
        */
    }

    public void OnShoot(InputAction.CallbackContext context)
    {
        /* Old Stuff
        if (tridentFree)
        {
            tridentFree = false;
            //isShoot = true;

            GameObject trident = Instantiate(throwableTrident);
            trident.transform.position = transform.position + (2 * transform.forward);
            trident.transform.rotation = transform.rotation;
            trident.transform.Rotate(new Vector3(90, 0, 0));
            trident.GetComponent<Rigidbody>().AddForce(tridentThrowForce * gameObject.transform.forward);

            trident.GetComponent<TridentPickupScript>().damageAmount = tridentThrowDamage;
            isShoot = false;
        }*/

        if(context.started)
        {
            GameObject seed = Instantiate(seedToShoot);
            SeedParabola pathScript = seed.GetComponent<SeedParabola>();

            seed.transform.position = gameObject.transform.position;
            pathScript.end = GameObject.Find("Aim").transform.position;
        }
        
    }

    public void OnAim(InputAction.CallbackContext context)
    {
        aimControls.aiming = true;
        CM_AimCam.m_Orbits.CopyTo(CM_MainCam.m_Orbits, 0);
        CM_MainCam.m_XAxis.m_MaxSpeed = 50;
        CM_MainCam.m_YAxis.m_MaxValue = 0;

        if (context.canceled)
        {
            CM_WideCam.m_Orbits.CopyTo(CM_MainCam.m_Orbits, 0);
            CM_MainCam.m_XAxis.m_MaxSpeed = 450;
            CM_MainCam.m_YAxis.m_MaxValue = 1;
            CM_MainCam.m_YAxis.Value = startYValue;
            aimControls.aiming = false;
        }
    }

    /* Previous Attempt
     *        // if(!aimCam.activeInHierarchy)
       // {
            //mainCam.SetActive(false);
            //aimCam.transform.rotation = mainCam.transform.rotation;
            //aimCam.SetActive(true);
            aimControls.aiming = true;
            shootingLine.SetActive(true);
        //mainCam.GetComponent<CinemachineFreeLook>().m_Orbits.CopyTo(tempCam.GetComponent<CinemachineFreeLook>().m_Orbits, 0);
        aimCam.GetComponent<CinemachineFreeLook>().m_Orbits.CopyTo(mainCam.GetComponent<CinemachineFreeLook>().m_Orbits, 0);
        mainCam.GetComponent<CinemachineFreeLook>().m_XAxis.m_MaxSpeed = 50;
        //  aimCam.GetComponentInChildren<GameObject>().SetActive(true);
        //UpdateTrajectory();
        //}

        if (context.canceled)
        {
            //mainCam.transform.rotation = aimCam.transform.rotation;
            //mainCam.transform.position = aimCam.transform.position;
            //mainCam.SetActive(true);
            //aimCam.SetActive(false);
            wideCam.GetComponent<CinemachineFreeLook>().m_Orbits.CopyTo(mainCam.GetComponent<CinemachineFreeLook>().m_Orbits, 0);
            mainCam.GetComponent<CinemachineFreeLook>().m_XAxis.m_MaxSpeed = 450;
            aimControls.aiming = false;
            shootingLine.SetActive(false);
         //   aimCam.GetComponentInChildren<GameObject>().SetActive(false);
        }
     */


    /* Old Stuff
        public void AddEnemyToTridentStab(GameObject obj)
        {
            stabEnemiesInRange.Add(obj);
        }

        public void RemoveEnemyFromTridentStab(GameObject obj)
        {
            stabEnemiesInRange.Remove(obj);
        }

        public void AddEnemyToTridentSwipe(GameObject obj)
        {
            swipeEnemiesInRange.Add(obj);
        }

        public void RemoveEnemyFromTridentSwipe(GameObject obj)
        {
            swipeEnemiesInRange.Remove(obj);
        }

        void cleanupLists()
        {
            foreach (GameObject e in stabEnemiesInRange)
            {

                if (e == null)
                {
                    stabEnemiesInRange.Remove(e);
                    break;
                }

            }

            foreach (GameObject e in swipeEnemiesInRange)
            {

                if (e == null)
                {
                    swipeEnemiesInRange.Remove(e);
                    break;
                }

            }
        }

        public void returnTrident()
        {
            tridentFree = true;
        }
    */
}
