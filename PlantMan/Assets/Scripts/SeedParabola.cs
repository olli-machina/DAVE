using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//https://www.youtube.com/watch?v=13KrnisMf14

public class SeedParabola : MonoBehaviour
{
    public float animTime;
    public AnimationCurve animCurve;
    public Vector3 end;

    Vector3 start;

    // Start is called before the first frame update
    void Start()
    {
        start = transform.position;
        //end = new Vector3(9999, 9999, 9999);
        animTime = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        animTime += Time.deltaTime / .5f;
        if (animTime > 1.0f)
        {
            // transform.position = end;
            Vector3 onePointBeforeFinal = Vector3.Lerp(start, end, 1.0f - Time.deltaTime / .5f);
            onePointBeforeFinal.y += animCurve.Evaluate(1.0f - Time.deltaTime / .5f);
            Vector3 finalVel = (end - onePointBeforeFinal) * 20f;
            GetComponent<Rigidbody>().AddForce(finalVel);
            Debug.Log(end);
            Destroy(this);
            return;
        }

        Vector3 pos = Vector3.Lerp(start, end, animTime);
        pos.y += animCurve.Evaluate(animTime);
        transform.position = pos;
    }

    public void UpdateTrajectory()
    {

    }
}
