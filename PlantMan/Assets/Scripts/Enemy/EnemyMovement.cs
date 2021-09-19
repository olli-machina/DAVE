using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{

    public float speed;
    public float desiredDistance;
    public float farthestDistance;
    public float rotateSpeed;

    private GameObject player;
    private bool tracking;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        tracking = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        Vector3 dir = player.transform.position - gameObject.transform.position;

       

        if (dir.magnitude >= desiredDistance && tracking)
        {
            dir.y = 0;
            dir.Normalize();

            transform.Translate(dir * speed * Time.deltaTime);

            Quaternion lookRotation = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotateSpeed);

        }
        else if (dir.magnitude >= farthestDistance && !tracking)
        {
            tracking = true;
        }
        else
        {
            tracking = false;
        }

        
        



    }

    public bool isTracking()
    {
        return tracking;
    }
}
