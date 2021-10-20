using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/**
* Subclass for PlantType to create Platform Plants
*/
class PlatformPlant : PlantType
{
    public float timeToPlatform = 2.0f; /**< Time it takes to fully grow platform flat surface*/
    public float growHeight = 2.0f; /**< How tall the platform should be*/
    public Vector2 platformDimentions; /**< size of platform flat surface*/
    public GameObject stalk;
    public GameObject platform;

    private float platformTimer; /**<private timer to keep track of platform flat surface growth*/
    private Vector3 growScale; /**< keep track of how big platform structure should get*/
    private Vector3 platScale; /**< keep track of how big platform flat surface should get*/
    private float timer; /**< keep track of when to check for light source*/

    private float growingTimer; /**< private timer to keep track of platform structure growth*/
    private float deathTimer; /**< private timer for plant to die when not in sunlight*/

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

    /**
    * Purpose: Handles the animation for the plant growing \n
    * References: called by Update() if isInLight \n
    * Scripts Called: None \n
    * Status: working \n
    * Contributor(s): Brandon L'Abbe
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


    /**
    * Purpose: Handles the animation for the plant shrinking \n
    * References: called by Update() if not isInLight \n
    * Scripts Called: None \n
    * Status: working \n
    * Contributor(s): Brandon L'Abbe
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
