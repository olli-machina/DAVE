using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerGrapple : MonoBehaviour
{
    public float maxAngle, maxRadius, timeToGrapple = 0.2f;
    public Material glowMat, baseMat;
    private bool isInFOV = false;
    public bool grappleToObj = false; 
    private Vector3 startPosition; //CANNOT BE A TRANSFORM
    private float grappleTime = 0f;
    private GameObject seenGrapple;
    public GameObject grappleLine;

    // Start is called before the first frame update
    void Start()
    {
        seenGrapple = null;
    }

    /*
    * Purpose: Draw the gizmos for in-editor
    * References: ---
    * Scripts Called: ---
    * Status: working
    */
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

    /*
    * Purpose: Determine if the grapple point is in the proper FOV
    * References: Update Function 
    * Scripts Called: --- 
    * Status: working
    */
    public bool inFOV(Transform checkingObj, float maxRadius)
    {
        Collider[] overlaps = new Collider[100]; //everything in FOV
        int count = Physics.OverlapSphereNonAlloc(checkingObj.position, maxRadius, overlaps);

        for (int i = 0; i < count; i++)
        {
            if (overlaps[i] != null)
            {
                if (overlaps[i].tag == "GrapplePoint") 
                {
                    GameObject target = overlaps[i].gameObject;
                    seenGrapple = target;
                    Vector3 directionBetween = (target.transform.position - checkingObj.position).normalized;
                    //directionBetween.y *= 0; //height not a factor

                    float angle = Vector3.Angle(checkingObj.forward, directionBetween);
                    if (Mathf.Abs(angle) <= maxAngle) //if in the FOV angle zone
                    {
                        return true;
                    }

                }
            }
        }

        return false;
    }

    /*
    * Purpose: Grapple function in response to button input 
    * References: Input manager in engine 
    * Scripts Called: ---
    * Status: working
    */
    public void OnGrapple(InputAction.CallbackContext context)
    {
        if(context.started)
        {
            startPosition = transform.position;
            if (seenGrapple != null)
            {
                grappleToObj = true;
                GameObject.Find("SoundManager").GetComponent<SoundManager>().Play(2, .5f);
            }
            else
            {
                grappleToObj = false;
            }
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!grappleToObj)
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

        LineRenderUpdate();
        if(grappleToObj)
        {
            if (grappleTime < 1.0f)
            {
                grappleTime += Time.deltaTime/timeToGrapple;
                gameObject.GetComponent<Rigidbody>().useGravity = false;
                Vector3 newPos = seenGrapple.transform.position + (seenGrapple.transform.forward * -1f) + (seenGrapple.transform.up * 1.5f);
                transform.position = Vector3.Lerp(startPosition, newPos, grappleTime);
            }
            else
            {
                gameObject.GetComponent<Rigidbody>().useGravity = true;
                grappleToObj = false;
                grappleTime = 0f;
                return;
            }
        }
    }

    // Changed the point[0] to have a hard coded transform so the line wouldn't spawn in DAVES crotch
    public void LineRenderUpdate()
    {
        if(grappleToObj)
        {
            grappleLine.SetActive(true);
            LineRenderer grappleLinerender = grappleLine.GetComponent<LineRenderer>();

            Vector3[] points = new Vector3[2];

            points[0] = new Vector3(transform.position.x -.75f, transform.position.y + .35f, transform.position.z);
            points[1] = seenGrapple.transform.position;

            grappleLinerender.SetPositions(points);

        }

        else
        {
            grappleLine.SetActive(false);
        }
    }
}