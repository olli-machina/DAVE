using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchingPlant : MonoBehaviour
{


    public float timeBetweenChecks = 1.0f;
    public float maxRaycastDistance = 100.0f;

    public bool isInLight = false;

    public bool shouldUseSun;

    public float timeToGrow = 3.0f;
    public float timeToShrink = 2.0f;
    public float timeToWait = 2.0f;
    public float timeToDeath = 5.0f;

    public float launchForce = 20f;

    public GameObject stalk;
    public GameObject foliage;

    private float timer;
    private float growingTimer;
    private float deathTimer;
    private float waitingTimer;


    private GameObject sun;

    private Vector3 seedScale;
    private Vector3 growScale;

    private Vector3 fullScale;
    private Vector3 foliageScale;

    private int lightValue;
    // Start is called before the first frame update
    void Start()
    {
        timer = 0.0f;
        growingTimer = 0.0f;
        waitingTimer = 0.0f;
        sun = GameObject.Find("Sun");

        lightValue = 0;

        seedScale = stalk.transform.localScale;

        growScale = seedScale;
        // growScale.y = growHeight;

        fullScale = foliage.transform.localScale;
    }

    // Update is called once per frame
    void Update()
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

    /*
     * Purpose: Determine if the seed is colliding in the sun spot
     * References: Update Function
     * Scripts Called: ---
     * Status: working
     */
    void CheckIfInSun()
    {
        if (shouldUseSun)
        {
            int layerMask = 1 << 9;

            layerMask = ~layerMask;

            Vector3 directionTowardsSun = -sun.transform.forward;
            isInLight = !Physics.Raycast(transform.position, directionTowardsSun, maxRaycastDistance, layerMask);
        }
        else
        {
            isInLight = lightValue > 0;
        }

    }

    /*
     * Purpose: Growing process when seed is actively in the light
     * References: Update Function
     * Scripts Called: ---
     * Status: working
     */
    void Grow()
    {
        if (growingTimer > 1.0f)
        {
            if (waitingTimer < 1.0f)
                waitingTimer += Time.deltaTime / timeToWait;
            else
                return;

            Vector3 pScale = Vector3.Lerp(fullScale, new Vector3(foliageScale.x, fullScale.y, foliageScale.y), platformTimer);

            foliage.layer = 0;
            foliage.transform.localScale = pScale;
            foliage.transform.rotation = Quaternion.LookRotation(Vector3.forward, Vector3.up);

        }
        else
        {
            growingTimer += Time.deltaTime / timeToGrow;
        }

        Vector3 scale = Vector3.Lerp(seedScale, growScale, growingTimer);
        Vector3 position = stalk.transform.localPosition;
        Vector3 platPosition = foliage.transform.localPosition;

        float height = scale.y - seedScale.y;
        position.y = height / 2.0f;
        platPosition.y = height;

        stalk.transform.localScale = scale;
        stalk.transform.localPosition = position;

        foliage.transform.localPosition = platPosition;
    }

    /*
     * Purpose: Shrink the seed back down once the seed leaves the light
     * References: Update function
     * Scripts Called: ---
     * Status: working
     */
    void Shrink()
    {
        if (platformTimer > 0.0f)
        {
            Vector3 pScale = Vector3.Lerp(fullScale, new Vector3(foliageScale.x, fullScale.y, foliageScale.y), platformTimer);

            foliage.transform.localScale = pScale;

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
        Vector3 platPosition = foliage.transform.localPosition;

        float height = scale.y - seedScale.y;
        position.y = height / 2.0f;
        platPosition.y = height;

        stalk.transform.localScale = scale;
        stalk.transform.localPosition = position;

        foliage.transform.localPosition = platPosition;

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
