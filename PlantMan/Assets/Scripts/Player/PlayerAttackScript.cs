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
        /* Old Stuff
        stabEnemiesInRange = new List<GameObject>();
        swipeEnemiesInRange = new List<GameObject>();
        */
        //CM_MainCam = mainCam.GetComponent<CinemachineFreeLook>();
        //CM_WideCam = wideCam.GetComponent<CinemachineFreeLook>();
        //CM_AimCam = aimCam.GetComponent<CinemachineFreeLook>();

        //startYValue = CM_MainCam.m_YAxis.Value;
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

        if(changeCam)
        {
            CinemachineOrbitalTransposer CM_transpose = mainCam.GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineOrbitalTransposer>();
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
            GameObject aimObj = GameObject.Find("Aim");

            seed.transform.position = gameObject.transform.position;
            pathScript.end = aimObj.transform.position;
        }

        
    }

    public void OnAim(InputAction.CallbackContext context)
    {
        //aimControls.aiming = true;
        //CM_AimCam.m_XAxis = CM_WideCam.m_XAxis;
        //CM_AimCam.m_YAxis = CM_WideCam.m_YAxis;
        //CM_AimCam.Priority = 11;

        //if (context.canceled)
        //{
        //    CM_WideCam.m_XAxis = CM_AimCam.m_XAxis;
        //    CM_WideCam.m_YAxis = CM_AimCam.m_YAxis;
        //    CM_AimCam.Priority = 9;
        //    aimControls.aiming = false;
        Vector3 moveInFront = Vector3.forward * 1.5f;
        aimMarker.transform.position = new Vector3(transform.localPosition.x + moveInFront.x, 4.75f, transform.localPosition.z + moveInFront.z + 3f);

        aimControls.aiming = true;
        //aimCam.SetActive(true);
        //wideCam.SetActive(false);

        if (context.canceled)
        {
            //wideCam.SetActive(true);
            //aimCam.SetActive(false);
            GameObject.Find("Aim").GetComponent<AimBoxScript>().onWall = false;
            aimControls.aiming = false;
        }
    }
    private void LateUpdate()
    {
        if (changeCam)
        {
            //CM_MainCam.m_YAxis.Value = startYValue;
            CM_MainCam.m_YAxis.Value = Quaternion.Lerp(Quaternion.Euler(0, CM_MainCam.m_YAxis.Value, 0), Quaternion.Euler(0, startYValue, 0), 5 * Time.deltaTime).y;

            if (Mathf.Abs(CM_MainCam.m_YAxis.Value - startYValue) < 0.1f)
            {
                changeCam = false;
            }

        }
    }
    //private void LateUpdate()
    //{
    //    if (changeCam)
    //    {
    //        //CM_MainCam.m_YAxis.Value = startYValue;
    //        CM_MainCam.m_YAxis.Value = Quaternion.Lerp(Quaternion.Euler(0, CM_MainCam.m_YAxis.Value, 0), Quaternion.Euler(0, startYValue, 0), 5 * Time.deltaTime).y;

    //        if (Mathf.Abs(CM_MainCam.m_YAxis.Value - startYValue) < 0.1f)
    //        {
    //            changeCam = false;
    //        }
    //    }
    //}


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
