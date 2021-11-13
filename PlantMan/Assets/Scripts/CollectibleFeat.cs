using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleFeat : MonoBehaviour
{

    private int numOfCollectibles;

    private bool[] collectiblesCollected;

    // Start is called before the first frame update
    void Start()
    {
        numOfCollectibles = GameObject.FindGameObjectsWithTag("Collectible").Length;
        collectiblesCollected = new bool[numOfCollectibles];   
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Collect(CollectibleData data)
    {
        Debug.Log("Collect! ID:" + data.ID);
        int ID = data.ID;

        collectiblesCollected[ID] = true;
        
        for(int i = 0; i< numOfCollectibles; i++)
        {
            if (!collectiblesCollected[i])
                return;
        }

        //Will only get here if all achievements have been collected
        GameObject.Find("FeatManager").GetComponent<FeatManager>().fireFeat("Collectibles");
    }

    public bool getCollectableState(int ID)
    {
        return collectiblesCollected[ID];
    }
}
