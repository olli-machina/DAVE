using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantManager : MonoBehaviour
{
    enum PlantType
    {
        Platform,
        LaunchPad
    };



    // Start is called before the first frame update
    void Start()
    {
        PlantType platformPlant = new PlantType();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

public class PlantType
{
    public float timeBetweenChecks = 1.0f;
    public float maxRaycastDistance = 100.0f;

    public bool isInLight = false;

    public bool shouldUseSun;
    public float timeToGrow = 3.0f;
    public float timeToShrink = 2.0f;
    public float timeToDeath = 5.0f;

    private float timer;
    private float growingTimer;
    private float deathTimer;

    private GameObject sun;

    private Vector3 seedScale;

    private int lightValue;

    PlantType(float _timeToGrow, float _timeToShrink, float _timeToDeath)
    {
        timeToGrow = _timeToGrow;
        timeToShrink = _timeToShrink;
        timeToDeath = _timeToDeath;

        timer = 0.0f;
        growingTimer = 0.0f;
        sun = GameObject.Find("Sun");

        lightValue = 0;
    }

    /*
    * Purpose: Determine if the seed is colliding in the sun spot
    * References: Update Function
    * Scripts Called: ---
    * Status: working
    */
    bool CheckIfInSun()
    {
        if (shouldUseSun)
        {
            int layerMask = 1 << 9;

            layerMask = ~layerMask;

            Vector3 directionTowardsSun = -sun.transform.forward;
            //isInLight = !Physics.Raycast(transform.position, directionTowardsSun, maxRaycastDistance, layerMask);
            return true;
        }
        else
        {
            isInLight = lightValue > 0;
            return false;
        }

    }
}
