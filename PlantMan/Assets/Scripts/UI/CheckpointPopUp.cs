using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CheckpointPopUp : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
       StartCoroutine("Move");
    }

    private IEnumerator Move()
    {
        transform.DOLocalMoveY(0, 1);
        yield return new WaitForSeconds(2);
        transform.DOLocalMoveY(120, 1);
        yield return new WaitForSeconds(1);
        Destroy(transform.parent.transform.parent.gameObject);
    }
}
