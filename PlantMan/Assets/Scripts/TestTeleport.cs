using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestTeleport : MonoBehaviour
{

    public float teleportCooldown;

    private float timer;

    // Start is called before the first frame update
    void Start()
    {
        timer = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if(timer > teleportCooldown)
        {
            timer = 0;
            Teleport();
        }

    }

    void Teleport()
    {
        int rand = Random.Range(0, 4);
        Vector3 dir = Vector3.zero;

        switch(rand)
        {
            case 0:
                dir = Vector3.left;
                break;
            case 1:
                dir = Vector3.right;
                break;
            case 2:
                dir = Vector3.forward;
                break;
            case 3:
                dir = Vector3.back;
                break;
        }

        gameObject.transform.position = gameObject.transform.position + dir;
    }
}
