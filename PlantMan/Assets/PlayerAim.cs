using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAim : MonoBehaviour
{

    public float theta = 45;

    public float force;

    public GameObject seedToShoot;
    public GameObject lineRenderer;
    public int numOfLinePoints = 30;

    public Vector3 aimOffset;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
        if(context.started)
        {
            Vector3 dir = new Vector3(transform.forward.x, transform.forward.y + Mathf.Sin(theta * Mathf.Deg2Rad), transform.forward.z);
            dir.Normalize();
            dir *= force;


            GameObject seed = Instantiate(seedToShoot);
            seed.transform.position = gameObject.transform.position;
            seed.GetComponent<Rigidbody>().velocity = dir;
        }
        

    }

    /*
     * Purpose: Set the visual parabola to match the fire path of the seed
     * References: in input controller attached to player obj
     * Scripts Called: None
     * Status: in progress
     * Contributor(s): Brandon L'Abbe
     */
    public void OnAim(InputAction.CallbackContext context)
    {
        lineRenderer.SetActive(true);
        LineRenderer lr = lineRenderer.GetComponent<LineRenderer>();

        float xForce = force * Mathf.Cos(theta * Mathf.Deg2Rad);
        float yForce = force * Mathf.Sin(theta * Mathf.Deg2Rad);
        float time = 2 * yForce / -Physics.gravity.y;

        float timeScale = time / numOfLinePoints;
        Vector3[] pos = new Vector3[numOfLinePoints];

        pos[0] = transform.position - aimOffset;
        for(int i = 1; i< numOfLinePoints; i++)
        {
            float t = timeScale * i;

            Vector3 relPos = transform.forward * (xForce * t);
            relPos.y = (yForce * t) + 0.5f * Physics.gravity.y * t * t;

            pos[i] = transform.position + relPos - aimOffset;
        }

        lr.SetPositions(pos);


        if (context.canceled)
            lineRenderer.SetActive(false);
    }

}
