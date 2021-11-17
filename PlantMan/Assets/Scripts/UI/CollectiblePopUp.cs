using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class CollectiblePopUp : MonoBehaviour
{
    public RawImage audioBar;
    public Texture[] sprites;
    public float length;
    bool isPlaying;
    int i;

    // Start is called before the first frame update
    void Start()
    {
        i = 0;
        StartCoroutine("AnimateOpen");
        StartCoroutine("AnimateClose");
    }

    // Update is called once per frame
    void Update()
    {
        if (!isPlaying)
        {
            StartCoroutine("AnimateBar");
        }
    }

    IEnumerator AnimateBar()
    {
        isPlaying = true;
        if (i == 0)
        {
            i++;
            audioBar.texture = sprites[0];
        }
        else
        {
            i--;
            audioBar.texture = sprites[1];
        }
        yield return new WaitForSeconds(0.5f);
        isPlaying = false;
    }

    IEnumerator AnimateOpen()
    {
        gameObject.transform.DOLocalMoveX(0, 1);
        yield return new WaitForSeconds(1);
    }

    IEnumerator AnimateClose()
    {
        yield return new WaitForSeconds(length);
        gameObject.transform.DOLocalMoveX(100, 1);
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }
}
