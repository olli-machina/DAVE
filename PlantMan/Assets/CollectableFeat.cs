using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableFeat : MonoBehaviour
{

    public int numOfCollectables = 4;

    private bool[] collectablesCollected;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        collectablesCollected = new bool[numOfCollectables];   
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Collect(int ID)
    {
        collectablesCollected[ID] = true;
        
        for(int i = 0; i<numOfCollectables; i++)
        {
            if (!collectablesCollected[ID])
                return;
        }

        //Will only get here if all achievements have been collected
        GameObject.Find("FeatManager").GetComponent<FeatManager>().fireFeat("Collectables");
    }

    public bool getCollectableState(int ID)
    {
        return collectablesCollected[ID];
    }
}
