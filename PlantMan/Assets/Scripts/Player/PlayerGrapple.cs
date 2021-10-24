using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerGrapple : MonoBehaviour
{
    public float maxAngle, maxRadius, timeToGrapple = 0.2f;
    public Material glowMat, baseMat;
    [SerializeField]
    private bool isInFOV = false, grappleToObj = false;
    private Transform /*seenGrapple*/ startPosition;
    private float grappleTime = 0f;
    private GameObject seenGrapple;

    // Start is called before the first frame update
    void Start()
    {
        seenGrapple = null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow; //draw sphere of detection
        Gizmos.DrawWireSphere(transform.position, maxRadius);

        Vector3 fovLine1 = Quaternion.AngleAxis(maxAngle, transform.up) * transform.forward * maxRadius;
        Vector3 fovLine2 = Quaternion.AngleAxis(-maxAngle, transform.up) * transform.forward * maxRadius;

        Gizmos.color = Color.blue; //both upper and lower FOV bounds
        Gizmos.DrawRay(transform.position, fovLine1);
        Gizmos.DrawRay(transform.position, fovLine2);

        if (seenGrapple != null)
        {
            if (!isInFOV)
                Gizmos.color = Color.red; //ray to player if not seen
            else
                Gizmos.color = Color.green; //ray to player if seen
            Gizmos.DrawRay(transform.position, (seenGrapple.transform.position - transform.position).normalized * maxRadius);
        }

        Gizmos.color = Color.black; //ray facing forward
        Gizmos.DrawRay(transform.position, transform.forward * maxRadius);
    }

    public bool inFOV(Transform checkingObj, float maxRadius)
    {
        Collider[] overlaps = new Collider[10]; //everything in FOV
        int count = Physics.OverlapSphereNonAlloc(checkingObj.position, maxRadius, overlaps);

        for (int i = 0; i < count; i++) //for behind the hunter
        {
            if (overlaps[i] != null) //for in front of the hunter
            {
                if (overlaps[i].tag == "GrapplePoint") //if the target is in the FOV
                {
                    GameObject target = overlaps[i].gameObject;
                    seenGrapple = target;
                    Vector3 directionBetween = (target.transform.position - checkingObj.position).normalized;
                    directionBetween.y *= 0; //height not a factor

                    float angle = Vector3.Angle(checkingObj.forward, directionBetween);
                    if (Mathf.Abs(angle) <= maxAngle) //if in the FOV angle zone
                    {
                        //Ray ray = new Ray(checkingObj.position, target.position - checkingObj.position);
                        return true;
                    }
                    //else
                    //{
                    //    seenGrapple = null; //watch for this
                    //}
                }
            }
        }

        return false;
    }

    public void OnGrapple(InputAction.CallbackContext context)
    {
        startPosition = transform;
        if(seenGrapple != null)
        {
            grappleToObj = true;
            //transform.position = Vector3.Lerp(startPosition, seenGrapple.position, Time.deltaTime);
        }
        else
        {
            grappleToObj = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        isInFOV = inFOV(transform, maxRadius);

        if (isInFOV)
            seenGrapple.GetComponent<MeshRenderer>().material = glowMat;
        else
        {
            if (seenGrapple != null)
            {
                seenGrapple.GetComponent<MeshRenderer>().material = baseMat;
                seenGrapple = null;
            }
        }

        if(grappleToObj)
        {
            if (grappleTime < 1.0f)
            {
                grappleTime += Time.deltaTime/timeToGrapple;
                gameObject.GetComponent<Rigidbody>().useGravity = false;
                Vector3 newPos = seenGrapple.transform.position + (seenGrapple.transform.forward * -3f) + (seenGrapple.transform.up * 3);
                transform.position = Vector3.Lerp(startPosition.position, seenGrapple.transform.position, grappleTime);
            }
            else
            {
                grappleToObj = false;
                seenGrapple = null;
                gameObject.GetComponent<Rigidbody>().useGravity = true;
                grappleTime = 0f;
                return;
            }
        }
    }
}

/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HunterFOV : MonoBehaviour
{
    public Transform player;
    GameManager manager;
    public float maxAngle, maxRadius, maxBackAngle, maxBackRadius;
    private bool isInFOV = false;

    private void Start()
    {
        manager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow; //draw sphere of detection
        Gizmos.DrawWireSphere(transform.position, maxRadius);

        Gizmos.color = Color.blue; //draw sphere of backwards detection
        Gizmos.DrawWireSphere(transform.position, maxBackRadius);

        Vector3 fovLine1 = Quaternion.AngleAxis(maxAngle, transform.up) * transform.forward * maxRadius;
        Vector3 fovLine2 = Quaternion.AngleAxis(-maxAngle, transform.up) * transform.forward * maxRadius;

        Vector3 backFovLine1 = Quaternion.AngleAxis(maxBackAngle, transform.up) * transform.forward * maxBackRadius;
        Vector3 backFovLine2 = Quaternion.AngleAxis(-maxBackAngle, transform.up) * transform.forward * maxBackRadius;


        Gizmos.color = Color.blue; //both upper and lower FOV bounds
        Gizmos.DrawRay(transform.position, fovLine1);
        Gizmos.DrawRay(transform.position, fovLine2);

        Gizmos.color = Color.cyan; //upper and lower back FOV bounds
        Gizmos.DrawRay(transform.position, backFovLine1);
        Gizmos.DrawRay(transform.position, backFovLine2);

        if(!isInFOV)
            Gizmos.color = Color.red; //ray to player if not seen
        else
            Gizmos.color = Color.green; //ray to player if seen
        Gizmos.DrawRay(transform.position, (player.position - transform.position).normalized * maxRadius);

        Gizmos.color = Color.black; //ray facing forward
        Gizmos.DrawRay(transform.position, transform.forward * maxRadius);
    }

    public static bool inFOV (Transform checkingObj, Transform target, float maxAngle, float maxBackAngle, float maxRadius, float maxBackRadius)
    {
        Collider[] overlaps = new Collider[10]; //everything in FOV
        Collider[] behind = new Collider[10]; //everything in behind FOV
        int count = Physics.OverlapSphereNonAlloc(checkingObj.position, maxRadius, overlaps);
        int behindCount = Physics.OverlapSphereNonAlloc(checkingObj.position, maxBackRadius, behind);

        for(int i = 0; i < count; i++) //for behind the hunter
        {
            if(behind[i] != null)
            {
                Vector3 directionBetween = (target.position - checkingObj.position).normalized;
                directionBetween.y *= 0; //height not a factor

                float angle = Vector3.Angle(checkingObj.forward, directionBetween);
                if (angle >= maxBackAngle)
                {
                    if (behind[i].transform == target)
                    {
                        Ray ray = new Ray(checkingObj.position, target.position - checkingObj.position);
                        RaycastHit hit;
                        if (Physics.Raycast(ray, out hit, maxBackRadius)) //if not behind something
                        {
                            if (hit.transform == target) //if it's the target/player
                            {
                                return true;
                            }
                        }
                    }
                }
            }
            else if(overlaps[i] != null) //for in front of the hunter
            {
                if(overlaps[i].transform == target) //if the target is in the FOV
                {
                    Vector3 directionBetween = (target.position - checkingObj.position).normalized;
                    directionBetween.y *= 0; //height not a factor

                    float angle = Vector3.Angle(checkingObj.forward, directionBetween);
                    if(angle <= maxAngle) //if in the FOV angle zone
                    {
                        Ray ray = new Ray(checkingObj.position, target.position - checkingObj.position);
                        RaycastHit hit;
                        if(Physics.Raycast(ray, out hit, maxRadius)) //if not behind something
                        {
                            if(hit.transform == target) //if it's the target/player
                            {
                                return true;
                            }
                        }
                    }
                }
            }
        }

        return false;
    }

    private void Update()
    {
        if (player == null)
            player = GameObject.Find("Player").GetComponent<Transform>();

        isInFOV = inFOV(transform, player, maxAngle, maxBackAngle, maxRadius, maxBackRadius);
    }

    public void FollowingAbility()
    {
        maxBackRadius += .5f;
    }
}*/