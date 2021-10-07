using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchingPlant : PlantType
{
    public float timeToMax = 2.0f;
    public float timeToHide = 5f;
    public float launchForce;
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
    private float cooldownTimer;

    public bool playerInRange;
    private bool fullGrown;
    private bool launching;

    private float timer;

    public override void Update()
    {
        timer += Time.deltaTime;

        if (timer > timeBetweenChecks)
        {
            base.CheckIfInSun();
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

    LaunchingPlant() : base() //???
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
        seedScale = foliage.transform.localScale; //?
        fullGrown = false;
        //foliageScale = foliage.transform.localScale;
    }

    public override void Grow()
    {
        if (growingTimer > 1f)
        {
            fullGrown = true;
            gameObject.layer = 0;
            return;
        }
        else
            growingTimer += Time.deltaTime / timeToGrow;


        Vector3 scale = Vector3.Lerp(seedScale, growScale, growingTimer);
        Vector3 position = foliage.transform.localPosition;

        float height = scale.y - seedScale.y;
        position.y = height / 2.0f;

        foliage.transform.localScale = scale;
        foliage.transform.localPosition = position;
    }

    public override void Shrink()
    {
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

        if (!launching)
        {
            Vector3 scale = Vector3.Lerp(seedScale, growScale, growingTimer);
            Vector3 position = foliage.transform.localPosition;

            float height = scale.y - seedScale.y;
            position.y = height / 2.0f;

            foliage.transform.localScale = scale;
            foliage.transform.localPosition = position;

            growingTimer -= Time.deltaTime;
        }
    }

    private void Hide()
    {
        if (growingTimer < 0.0f)
        {
            hideTimer += Time.deltaTime;

            if (hideTimer > timeToHide)
                Launch();

            //if(!launching)
            //{
            //    cooldownTimer += Time.deltaTime;
            //    if(cooldownTimer > launchCoolDown)
            //    {

            //    }
            //}
            return;
        }
        else
            hideTimer = 0.0f;


        Vector3 scale = Vector3.Lerp(hideScale, growScale, growingTimer);
        Vector3 position = foliage.transform.localPosition;

        float height = scale.y - seedScale.y;
        position.y = height / 2.0f;

        foliage.transform.localScale = scale;
        foliage.transform.localPosition = position;

        growingTimer -= Time.deltaTime;
    }

    private void Launch()
    {
        if (launching)
        {
            foliage.transform.localScale = growScale;
            player.GetComponentInChildren<Rigidbody>().AddForce(0f, 2000f, 0f);
            Debug.Log("Hidden");
            launching = false;
            playerInRange = false;
            hideTimer = 0f;
            growingTimer = 1f;
        }
        else
            return;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Light")
        {
            lightValue++;
        }
       // Debug.Log(other.gameObject.tag);
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
            //if(!launching)
            playerInRange = false;
        }
    }
}
