using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FeatWindow : MonoBehaviour
{
    public GameObject parent;
    FeatManager featManager;
    public GameObject featPrefab;
    public GameObject[] featInfo;

    // Start is called before the first frame update
    void Start()
    {
        featManager = GameObject.Find("FeatManager").GetComponent<FeatManager>();
        featInfo = new GameObject[featManager.achievementNames.Length];
        for (int i = 0; i < featManager.achievementNames.Length; i++)
        {
            featInfo[i] = Instantiate(featPrefab, this.gameObject.transform);
            featInfo[i].GetComponent<FeatInfo>().featName.text = featManager.achievementNames[i];
            featInfo[i].GetComponent<FeatInfo>().description.text = featManager.achievementDescriptions[i];
        }
        parent.SetActive(false);
        transform.parent.transform.parent.transform.parent.gameObject.SetActive(false);
    }

    public void UpdateSticker(int num)
    {
        featInfo[num].GetComponent<FeatInfo>().sticker.sprite = featManager.stickers[num];
    }
}
