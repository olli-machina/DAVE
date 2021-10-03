using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIMovement : MonoBehaviour
{

    NavMeshAgent navAgent;

    GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        navAgent = GetComponent<NavMeshAgent>();

        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 dir = player.transform.position - gameObject.transform.position;

        Vector3 projection = Vector3.Project(dir.normalized, gameObject.transform.forward);

        if (Vector3.Dot(projection, gameObject.transform.forward) < 0)
            Debug.Log("IN FRONT OF ME");
            //navAgent.destination = player.transform.position;
    }
}
