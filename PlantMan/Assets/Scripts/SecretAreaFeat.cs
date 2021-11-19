using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecretAreaFeat : MonoBehaviour
{
    private int numOfSecretAreas;

    private bool[] secretsFound;

    // Start is called before the first frame update
    void Start()
    {
        numOfSecretAreas = GameObject.FindGameObjectsWithTag("Secret").Length;
        secretsFound = new bool[numOfSecretAreas];
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Collect(SecretAreaData data)
    {
        Debug.Log("Collect! ID:" + data.ID);
        int ID = data.ID;

        secretsFound[ID] = true;

        for (int i = 0; i < numOfSecretAreas; i++)
        {
            if (!secretsFound[i])
                return;
        }

        //Will only get here if all achievements have been collected
        GameObject.Find("FeatManager").GetComponent<FeatManager>().fireFeat("Hidden Areas");
    }

    public bool getSecretState(int ID)
    {
        return secretsFound[ID];
    }
}
