using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttackScript : MonoBehaviour
{

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

    private float timer;
    private float animationTimer;

    public GameObject mainCam, aimCam;
    public float forceMultiplier = 2;
    private bool isShoot = false;
    public GameObject aimMarker;
    private Vector3 forceV;
    [SerializeField]
    private NewInputLook aimControls;

    // Start is called before the first frame update
    void Start()
    {
        stabEnemiesInRange = new List<GameObject>();
        swipeEnemiesInRange = new List<GameObject>();
        tridentFree = true;
    }

    // Update is called once per frame
    void Update()
    {

        cleanupLists();

        timer += Time.deltaTime;

        if (animationTimer > 0)
            animationTimer -= Time.deltaTime;


        if (!isShoot && aimCam.activeInHierarchy)
        {
            Vector3 forceInit = (aimMarker.transform.position - gameObject.transform.position) * 50f;
            forceV = (new Vector3(forceInit.x, forceInit.y, forceInit.z)) * forceMultiplier;
            ParabolaPath.Instance.UpdateTrajectory(forceV, gameObject.GetComponent<Rigidbody>(), transform.position);
        }

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
    }

    public void OnMelee(InputAction.CallbackContext context)
    {
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
    }

    public void OnShoot(InputAction.CallbackContext context)
    {
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
        }
       // isShoot = false;
    }

    public void OnAim(InputAction.CallbackContext context)
    {
        if(!aimCam.activeInHierarchy)
        {
            mainCam.SetActive(false);
            aimCam.SetActive(true);
            aimControls.aiming = true;

            //UpdateTrajectory();
        }

        if(context.canceled)
        {
            mainCam.SetActive(true);
            aimCam.SetActive(false);
            aimControls.aiming = false;
            ParabolaPath.Instance.HideLine();
        }

    }




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

}
