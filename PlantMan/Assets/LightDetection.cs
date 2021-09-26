using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightDetection : MonoBehaviour
{
    public float timeBetweenChecks = 1.0f;
    public float maxRaycastDistance = 100.0f;

    public bool isInLight = false;

    public bool shouldUseSun;

    public float timeToGrow = 3.0f;
    public float timeToShrink = 2.0f;
    public float timeToPlatform = 2.0f;
    public float timeToDeath = 5.0f;

    public float growHeight = 10.0f;

    public Vector2 platformDimentions;

    public GameObject stalk;
    public GameObject platform;

    private float timer;
    private float growingTimer;
    private float platformTimer;
    private float deathTimer;

    private GameObject sun;

    private Vector3 seedScale;
    private Vector3 growScale;

    private Vector3 platScale;

    private int lightValue;
    // Start is called before the first frame update
    void Start()
    {
        timer = 0.0f;
        growingTimer = 0.0f;
        platformTimer = 0.0f;
        sun = GameObject.Find("Sun");

        lightValue = 0;

        seedScale = stalk.transform.localScale;

        growScale = seedScale;
        growScale.y = growHeight;

        platScale = platform.transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if(timer > timeBetweenChecks)
        {
            CheckIfInSun();
            timer = 0.0f;
        }

        if(isInLight)
        {
            Grow();
        }
        else
        {
            Shrink();
        }
    }

    void CheckIfInSun()
    {
        if(shouldUseSun)
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

    void Grow()
    {
        

        if(growingTimer > 1.0f)
        {
            if(platformTimer < 1.0f)
                platformTimer += Time.deltaTime / timeToPlatform;

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

    void Shrink()
    {
        if(platformTimer > 0.0f)
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
        if(other.gameObject.tag == "Light")
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
