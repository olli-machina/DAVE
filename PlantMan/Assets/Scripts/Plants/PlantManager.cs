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

}

public class PlantType : MonoBehaviour
{

    public float timeBetweenChecks = 1.0f; /**< Time between checking if plant is in sunlight*/
    public float maxRaycastDistance = 100.0f; /**< Maximum raycast for the light*/

    public bool isInLight = false; /**< is the plant in the light*/

    public bool shouldUseSun;
    public float timeToGrow = 3.0f; /**< Time it takes to fully grow*/
    public float timeToShrink = 2.0f; /**< Time it takes to shrink back to seed*/
    public float timeToDeath = 5.0f; /**< Time it takes for plant to disappear after not in the sun*/


    // Update is called once per frame
    public virtual void Update()
    {
    }

    public PlantType()
    { }

    public PlantType(float _timeToGrow, float _timeToShrink, float _timeToDeath)
    {
        timeToGrow = _timeToGrow;
        timeToShrink = _timeToShrink;
        timeToDeath = _timeToDeath;

        sun = GameObject.Find("Sun");

        lightValue = 0;
    }

    /**
    * Purpose: Determine if the seed is colliding in the sun spot \n
    * References: Update Function \n
    * Scripts Called: --- \n
    * Status: working
    */
    public void CheckIfInSun()
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

    public virtual void Grow() { }
    public virtual void Shrink() { }


    protected Vector3 startingScale;
    protected int lightValue;
    protected GameObject sun;
}