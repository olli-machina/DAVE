using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/**
* Subclass for PlantType to create Platform Plants
*/
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

    private bool showGrowingModels;
    private Animator animator;
    public GameObject platformObj;
    public GameObject seed;

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

        showGrowingModels = false;
        animator = GetComponent<Animator>();
    }

    /*
    * Purpose: Handles the animation for the plant growing 
    * References: called by Update() if isInLight
    * Scripts Called: None
    * Status: working
    * Contributor(s): Brandon L'Abbe
    */
    public override void Grow()
    {
        platformObj.SetActive(true);
        //Vector3.Lerp(seed.transform.localScale, Vector3.zero, growingTimer);

        animator.enabled = true;
        if (growingTimer > 1.0f) //If the stalk is done growing
        {
            if (platformTimer < 1.0f)
                platformTimer += Time.deltaTime / timeToPlatform;
            else
            {
                seed.GetComponent<MeshRenderer>().enabled = false;
                animator.SetBool("FullyGrown", true);
                return; //Growing completely done
            }

            //GetComponent<WallStick>().enabled = false;

            Vector3 pScale = Vector3.Lerp(platScale, new Vector3(platformDimentions.x, platScale.y, platformDimentions.y), platformTimer);

            platform.layer = 0;
           // platform.transform.localScale = pScale;
            platform.transform.rotation = Quaternion.LookRotation(Vector3.forward, Vector3.up);

        }
        else
        {
            growingTimer += Time.deltaTime / timeToGrow;

            if(!showGrowingModels)
            {
                showGrowingModels = true;
                //stalk.GetComponent<MeshRenderer>().enabled = true;
                //platform.GetComponent<MeshRenderer>().enabled = true;
            }
        }

        Vector3 scale = Vector3.Lerp(startingScale, growScale, growingTimer);
        Vector3 position = stalk.transform.localPosition;
        Vector3 platPosition = platform.transform.localPosition;

        float height = scale.y - startingScale.y;
        position.y = height / 2.0f;
        platPosition.y = height;

        //stalk.transform.localScale = scale;
       // stalk.transform.localPosition = position;

        platform.transform.localPosition = platPosition;
    }


    /*
    * Purpose: Handles the animation for the plant shrinking 
    * References: called by Update() if not isInLight
    * Scripts Called: None
    * Status: working
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
            if (showGrowingModels)
            {
                showGrowingModels = false;
               // stalk.GetComponent<MeshRenderer>().enabled = false;
                //platform.GetComponent<MeshRenderer>().enabled = false;
            }

            deathTimer += Time.deltaTime;

            if (deathTimer > timeToDeath)
            {
                GameObject.Find("FeatManager").GetComponent<FeatManager>().DisableFeat("Sniper");
                Destroy(gameObject);
            }
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
