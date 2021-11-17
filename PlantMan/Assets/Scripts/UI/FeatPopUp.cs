using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FeatPopUp : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("Move");
    }

    IEnumerator Move()
    {
        transform.DOLocalMoveX(0, 1);
        yield return new WaitForSeconds(2);
        transform.DOLocalMoveX(300, 1);
        yield return new WaitForSeconds(1);
        Destroy(transform.parent.transform.parent.gameObject);
    }
}
