using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchingPlant : PlantType
{
    public float timeToMax = 2.0f;
    public float timeToHide = 5f;
    public float launchForce;
    public Vector2 foliageDimentions;

    public GameObject foliage;
    public GameObject shrinkFoliage;

    private Vector3 foliageScale;
    private Vector3 growScale;
    private float hideTimer;
    private float foliageTimer;
    private float growingTimer;
    private float deathTimer;

    private float timer;

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

    LaunchingPlant() : base() //???
    {
        timer = 0f;
        growingTimer = 0f;
        hideTimer = 0f;

        lightValue = 0;

        growScale = seedScale;
       // growScale.y = growHeight;

    }

    private void Start()
    {
        seedScale = shrinkFoliage.transform.localScale; //?
        foliageScale = foliage.transform.localScale;
    }

    public override void Grow()
    {
        if (growingTimer < 1f)
            return;
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

            deathTimer += Time.deltaTime;

            if (deathTimer > timeToDeath)
                Destroy(gameObject);

            return;
        }
        else
            deathTimer = 0.0f;


        Vector3 scale = Vector3.Lerp(seedScale, growScale, growingTimer);
        Vector3 position = foliage.transform.localPosition;

        float height = scale.y - seedScale.y;
        position.y = height / 2.0f;

        foliage.transform.localScale = scale;
        foliage.transform.localPosition = position;

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
