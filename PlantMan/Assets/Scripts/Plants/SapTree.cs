using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SapTree : PlantType
{
    public float timeDrip = 2.0f;
    public float dripRad = 5.0f;
    public GameObject plant;
    public GameObject sap;

    private Vector3 dripScale;
    private Vector3 startPos;

    public bool isDripping;
    public float newSpeed;

    private float dripTimer;

    public override void Update()
    {
        if(isDripping)
        {
            Drip();
        }

    }

    SapTree() : base()
    {
        dripTimer = 0f;
        isDripping = false;
    }

    private void Start()
    {
        dripTimer = 0;
        dripScale = new Vector3(dripRad, 0.01f, dripRad);//might be issue
        startPos = sap.transform.localPosition;
    }

    /*
    * Purpose: if player shoots sap tree, drip the sap pool
    * References: Update() called if isDripping is true
    * Scripts Called: None
    * Status: ---
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

        sap.transform.localScale = scale;
    }

  
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Seed")
        {
            isDripping = true;
        }
    }
}
