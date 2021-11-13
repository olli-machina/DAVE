using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeatManager : MonoBehaviour
{

    public string[] achievementNames;
    public string[] achievementDescriptions;

    private bool[] achievementComplete;
    private bool[] achievementDisabled;
    // Start is called before the first frame update
    void Start()
    {
        achievementComplete = new bool[achievementNames.Length];
        achievementDisabled = new bool[achievementNames.Length];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /*
    * Purpose: Activates an achievement. This should be called when the condition for an achievement should be met
    * References: Called by PlayerMovement [add all places where achievements get fired]
    * Scripts Called: None
    * Status: working
    * Contributers: Brandon L'Abbe
    */
    public void fireFeat(string achievementName)
    {
        //Finds the index of the achievement
        int index;
        for(index = 0; index < achievementNames.Length; index++)
        {
            if(achievementNames[index] == achievementName)
            {
                break;
            }
        }

        //Check if it didn't find the achievement
        if(index == achievementNames.Length)
        {
            Debug.LogError("Achievement fired that does not exist");
            return;
        }

        //Check if already completed, if so, cancel achievement, else mark it completed
        if (achievementComplete[index] && !achievementDisabled[index])
            return;
        else
            achievementComplete[index] = true;

        //Add code here that will fire for ALL achievements
        //I added curly braces for organizational purposes
        {
            Debug.Log("An achievement was fired:" + achievementNames[index]);
        }

        //Add code here to call other functions that execute what should happen when the achievement gets fired.
        //These functions do not need to be in this script
        switch(index)
        {
            case 0:
                HiddenAreasAchievment();
                break;

        }
    }

    public void DisableFeat(string achievementName)
    {
        //Finds the index of the achievement
        int index;
        for (index = 0; index < achievementNames.Length; index++)
        {
            if (achievementNames[index] == achievementName)
            {
                break;
            }
        }

        //Check if it didn't find the achievement
        if (index == achievementNames.Length)
        {
            Debug.LogError("Achievement disabled that does not exist");
            return;
        }

        //Check if already completed, if so, cancel achievement, else mark it completed
        if (achievementComplete[index] || achievementDisabled[index])
            return;
        else
            achievementDisabled[index] = true;

        //Add code here that will fire for ALL achievements
        //I added curly braces for organizational purposes
        {
            Debug.Log("An achievement was disabled:" + achievementNames[index]);
        }

        //Add code here to call other functions that execute what should happen when the achievement gets fired.
        //These functions do not need to be in this script
        switch (index)
        {
            case 0:
                //something could go here
                break;

        }
    }

    /*
    * Purpose: This will serve as an example achievement, until we get actual achievments in place
    * References: None
    * Scripts Called: None
    * Status: working
    * Contributers: Brandon L'Abbe
    */
    void HiddenAreasAchievment()
    {
        Debug.Log("ACHIEVEMENT UNLOCKED: \"Aaaapple\" ");
    }
}
