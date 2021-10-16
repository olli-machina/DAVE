using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class PlatformPlant : PlantType
{
    public float timeToPlatform = 2.0f;
    public float growHeight = 2.0f;
    public Vector2 platformDimentions;
    public GameObject stalk;
    public GameObject platform;

    private float platformTimer;
    private Vector3 growScale;
    private Vector3 platScale;
    private float timer;

    private float growingTimer;
    private float deathTimer;

    public override void Update()
    {
        timer += Time.deltaTime;

        if (timer > timeBetweenChecks)
        {
            CheckIfInSun();
            timer = 0.0f;
        }

        if (isInLight)
        {
            Grow();
        }
        else
        {
            Shrink();
        }

    }

    PlatformPlant() : base()
    {
        timer = 0f;
        growingTimer = 0f;
        platformTimer = 0f;

        lightValue = 0;

    }

    private void Start()
    {
        startingScale = stalk.transform.localScale; //Starting scale
        growScale = startingScale;
        growScale.y = growHeight;
        platScale = platform.transform.localScale;
    }

    /*
    * Purpose: Handles the animation for the plant growing
    * References: called by Update() if isInLight
    * Scripts Called: None
    * Status: working
    */
    public override void Grow()
    {
        if (growingTimer > 1.0f) //If the stalk is done growing
        {
            if (platformTimer < 1.0f) //If platform is not done growing
                platformTimer += Time.deltaTime / timeToPlatform;
            else
                return; //Growing completely done

            Vector3 pScale = Vector3.Lerp(platScale, new Vector3(platformDimentions.x, platScale.y, platformDimentions.y), platformTimer); //Grow platform

            platform.layer = 0;
            platform.transform.localScale = pScale;
            platform.transform.rotation = Quaternion.LookRotation(Vector3.forward, Vector3.up); //Make platform face upwards

        }
        else
        {
            growingTimer += Time.deltaTime / timeToGrow;
        }

        Vector3 scale = Vector3.Lerp(startingScale, growScale, growingTimer);
        Vector3 position = stalk.transform.localPosition;
        Vector3 platPosition = platform.transform.localPosition;

        float height = scale.y - startingScale.y;
        position.y = height / 2.0f;
        platPosition.y = height;

        stalk.transform.localScale = scale;
        stalk.transform.localPosition = position;

        platform.transform.localPosition = platPosition;
    }


    /*
    * Purpose: Handles the animation for the plant shrinking
    * References: called by Update() if not isInLight
    * Scripts Called: None
    * Status: working
    */
    public override void Shrink()
    {
        if (platformTimer > 0.0f)
        {
            Vector3 pScale = Vector3.Lerp(platScale, new Vector3(platformDimentions.x, platScale.y, platformDimentions.y), platformTimer);

            platform.transform.localScale = pScale;

            platformTimer -= Time.deltaTime;
            return;
        }


        if (growingTimer < 0.0f)
        {

            deathTimer += Time.deltaTime;

            if (deathTimer > timeToDeath)
                Destroy(gameObject);

            return;
        }
        else
            deathTimer = 0.0f;


        Vector3 scale = Vector3.Lerp(startingScale, growScale, growingTimer);
        Vector3 position = stalk.transform.localPosition;
        Vector3 platPosition = platform.transform.localPosition;

        float height = scale.y - startingScale.y;
        position.y = height / 2.0f;
        platPosition.y = height;

        stalk.transform.localScale = scale;
        stalk.transform.localPosition = position;

        platform.transform.localPosition = platPosition;

        growingTimer -= Time.deltaTime;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Light")
        {
            lightValue++;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Light")
        {   
            lightValue--;

            if (lightValue < 0)
                lightValue = 0;
        }
    }
}
