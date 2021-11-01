using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SapPlant : PlantType
{
    public float timeDrip = 2.0f; /**< Time it takes for sap to reach the ground*/
    private float dripDist; /**< Distance from sap plant to ground or whatever stopping point*/
    public GameObject plant;
    public GameObject sap;

    private Vector3 dripScale; /**< Scale the sap object to full size*/
    private Vector3 startPos; /**< start poisition of the sap*/

    public bool isDripping; /**< is the sap plant dripping sap*/

    private float dripTimer; /**< private timer to count the time it is dripping*/
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

    /**
    * Purpose: if player shoots sap plant on the wall- sap starts dripping in this function \n
    * References: Update() called if isDripping is true \n
    * Scripts Called: None \n
    * Status: working
    */
    public void Drip()
    { 
        //drip for correct duration
        if(dripTimer > timeDrip)
        {
            isDripping = false;
            dripTimer = 0;
            return; //leave function if it is done dripping
        }
        else
        {
            dripTimer += Time.deltaTime;
        }

        Vector3 scale = Vector3.Lerp(startingScale, dripScale, dripTimer); //scale to floor
        Vector3 movePosition = new Vector3(startPos.x, -dripDist/2f, startPos.z); //move y position as it drips because it scales both ways
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
