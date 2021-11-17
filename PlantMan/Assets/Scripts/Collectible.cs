using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Collectible : MonoBehaviour
{
    public Canvas collectiblePopUp;
    public string audioClipName;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            GameObject.Find("FeatManager").GetComponent<CollectibleFeat>().Collect(GetComponent<CollectibleData>());
            SoundManager sManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
            Canvas newPopUp = Instantiate(collectiblePopUp);

            for (int i = 0; i < sManager.soundNames.Length; i++)
            {
                if (sManager.soundNames[i] == audioClipName)
                {
                    newPopUp.GetComponent<CollectiblePopUp>().length = sManager.soundClips[i].length;
                    break;
                }
            }

            sManager.Play(audioClipName, 1.0f);
            Destroy(gameObject);
        }
    }
}
