using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchingPlant : PlantType
{
    public float timeToMax = 2.0f;
    public float timeToHide = 5f;
    public float launchForce, sideLaunchMultiplier;
    public Vector2 foliageDimentions;
    public float growMultiplier;
    public float launchCoolDown;

    public GameObject foliage;
    public GameObject shrinkFoliage;

    private GameObject player;

    private Vector3 hideScale;
    private Vector3 growScale;


    private float hideTimer;
    private float growingTimer;
    private float deathTimer;

    public bool playerInRange;
    private bool fullGrown;
    private bool launching;

    private float timer;

    public override void Update()
    {
        timer += Time.deltaTime;

        if (timer > timeBetweenChecks)
        {
            CheckIfInSun();
            timer = 0.0f;
        }

        if (playerInRange && fullGrown)
        {
            launching = true;
            Hide();
        }

        else if (isInLight)
        {
            if(!fullGrown)
                Grow();
        }
        else
        {
            Shrink();
        }


    }

    LaunchingPlant() : base()
    {
        timer = 0f;
        growingTimer = 0f;
        hideTimer = 0f;

        lightValue = 0;

        growScale = new Vector3(1.5f, 0.1f, 1.5f) ;
        hideScale = new Vector3(0.75f, 0.1f, 0.75f);

        fullGrown = false;

    }

    private void Start()
    {
        startingScale = foliage.transform.localScale;
        fullGrown = false;
    }

    /*
    * Purpose: grow the launch plant after shot and when the player leaves the range
    * References: Update() if isInLight, 
    * Scripts Called: None
    * Status: working
    */
    public override void Grow()
    {
        //mark that the plant is full grown for after player leaves range
        //Plant should not reset- just grow back to the full size
        if (growingTimer > 1f)
        {
            fullGrown = true;
            gameObject.layer = 0;
            return;
        }
        else
            growingTimer += Time.deltaTime / timeToGrow;


        Vector3 scale = Vector3.Lerp(startingScale, growScale, growingTimer);
        Vector3 position = foliage.transform.localPosition;

        float height = scale.y - startingScale.y;
        position.y = height / 2.0f;

        foliage.transform.localScale = scale;
        foliage.transform.localPosition = position;
    }

    /*
    * Purpose: shrink the plant if out of light or if player is in range
    * References: Update() if !isInLight
    * Scripts Called: None
    * Status: working
    */
    public override void Shrink()
    {
        //if it's been out of the light long enough to shrink all the way, reset the seed growth overall
        if (growingTimer < 0.0f)
        {
            fullGrown = false;
            deathTimer += Time.deltaTime;

            if (deathTimer > timeToDeath)
                Destroy(gameObject);

            return;
        }
        else
            deathTimer = 0.0f;

        //if it's not launching, then it is shrinking from no light
        if (!launching)
        {
            Vector3 scale = Vector3.Lerp(startingScale, growScale, growingTimer);
            Vector3 position = foliage.transform.localPosition;

            float height = scale.y - startingScale.y;
            position.y = height / 2.0f;

            foliage.transform.localScale = scale;
            foliage.transform.localPosition = position;

            growingTimer -= Time.deltaTime;
        }
    }

    /*
    * Purpose: shrink the plant when player is in range
    * References: Update() if isInLight
    * Scripts Called: None
    * Status: working
    */
    private void Hide()
    {
        //it is in hiding, waiting to launch player
        if (growingTimer < 0.0f)
        {
            hideTimer += Time.deltaTime;

            //hiding done, launch player
            if (hideTimer > timeToHide)
                Launch();

            return;
        }
        else
            hideTimer = 0.0f;


        Vector3 scale = Vector3.Lerp(hideScale, growScale, growingTimer);
        Vector3 position = foliage.transform.localPosition;

        float height = scale.y - startingScale.y;
        position.y = height / 2.0f;

        foliage.transform.localScale = scale;
        foliage.transform.localPosition = position;

        growingTimer -= Time.deltaTime;
    }

    /*
    * Purpose: launch the player out from the launch plant
    * References: Hide() when time to launch 
    * Scripts Called: None
    * Status: working
    */
    private void Launch()
    {
        if (launching)
        {
            foliage.transform.localScale = growScale;
            Vector3 forceDirection = transform.up * launchForce;
            player.GetComponentInChildren<Rigidbody>().AddForce(forceDirection * sideLaunchMultiplier); 
            launching = false;
            playerInRange = false; //make sure player is not still in range after launching, may need to delete later**
            hideTimer = 0f;
            growingTimer = 1f;
        }
        else //failsafe for if launch is called when it shouldn't be, only launch once
            return;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Light")
        {
            lightValue++;
        }

        if (other.gameObject.tag == "Player")
        {
            playerInRange = true;
            player = other.gameObject;
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

        if (other.gameObject.tag == "Player")
        {
            playerInRange = false;
        }
    }
}
