using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class PlatformPlant : PlantType
{
    public float timeToPlatform = 2.0f;
    public float growHeight = 10.0f;
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
            Debug.Log(timer);

        if (timer > timeBetweenChecks)
        {
            base.CheckIfInSun();
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

    PlatformPlant() : base() //???
    {
        timer = 0f;
        growingTimer = 0f;
        platformTimer = 0f;

        lightValue = 0;

        growScale = seedScale;
        growScale.y = growHeight;

        
    }

    private void Start()
    {
        seedScale = stalk.transform.localScale; //?
        platScale = platform.transform.localScale;
    }

    public override void Grow()
    {
        if (growingTimer > 1.0f)
        {
            if (platformTimer < 1.0f)
                platformTimer += Time.deltaTime / timeToPlatform;
            else
                return;

            Vector3 pScale = Vector3.Lerp(platScale, new Vector3(platformDimentions.x, platScale.y, platformDimentions.y), platformTimer);

            platform.layer = 0;
            platform.transform.localScale = pScale;
            platform.transform.rotation = Quaternion.LookRotation(Vector3.forward, Vector3.up);

        }
        else
        {
            growingTimer += Time.deltaTime / timeToGrow;
        }

        Vector3 scale = Vector3.Lerp(seedScale, growScale, growingTimer);
        Vector3 position = stalk.transform.localPosition;
        Vector3 platPosition = platform.transform.localPosition;

        float height = scale.y - seedScale.y;
        position.y = height / 2.0f;
        platPosition.y = height;

        stalk.transform.localScale = scale;
        stalk.transform.localPosition = position;

        platform.transform.localPosition = platPosition;
    }

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


        Vector3 scale = Vector3.Lerp(seedScale, growScale, growingTimer);
        Vector3 position = stalk.transform.localPosition;
        Vector3 platPosition = platform.transform.localPosition;

        float height = scale.y - seedScale.y;
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
