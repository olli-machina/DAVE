using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SapPlant : PlantType
{
    public float timeDrip = 2.0f;
    private float dripDist;
    public GameObject plant;
    public GameObject sap;

    private Vector3 dripScale;
    private Vector3 startPos;

    public bool isDripping;

    private float dripTimer;
    private float timer;

    public override void Update()
    {
        timer += Time.deltaTime;

        if (timer > timeBetweenChecks)
        {
            timer = 0.0f;
        }

        if(isDripping)
        {
            Drip();
        }

    }

    SapPlant() : base()
    {
        timer = 0f;
        dripTimer = 0f;
        isDripping = false;
    }

    private void Start()
    {
        dripTimer = 0;
        dripDist = transform.position.y + 1.5f;
        dripScale = new Vector3(dripDist, 0.01f, 1f);
        startPos = sap.transform.localPosition;
    }

    /*
    * Purpose: if player shoots sap plant on the wall- sap starts dripping in this function
    * References: Update() called if isDripping is true
    * Scripts Called: None
    * Status: working
    */
    public void Drip()
    { 
        if(dripTimer > timeDrip)
        {
            isDripping = false;
            dripTimer = 0;
            return;
        }
        else
        {
            dripTimer += Time.deltaTime;
        }

        Vector3 scale = Vector3.Lerp(startingScale, dripScale, dripTimer);
        Vector3 movePosition = new Vector3(startPos.x, -dripDist/2f, startPos.z);
        Vector3 position = Vector3.Lerp(startPos, movePosition, dripTimer);

        sap.transform.localScale = scale;
        sap.transform.localPosition = position;
    }


  
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Seed")
        {
            isDripping = true;
        }
    }
}
