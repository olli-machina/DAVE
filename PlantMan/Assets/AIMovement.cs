using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIMovement : MonoBehaviour
{

    NavMeshAgent navAgent;

    GameObject player;

    public float FOV; //Represents the FOV of the enemy.
                      //0 represents a semi-circle, the Enemy can see anything in front of it
                      //0.5 represents a quarter of a circle in front of the enemy, most similar to how they actually see
                      //1.0 represents a line in front of the enemy, can only see things in this line, which isn't possible, so nothing will happen
                      //This number could become negative... it would then start expanding to be able to see things behind

    public float viewDistance;

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

        if (Vector3.Dot(projection, gameObject.transform.forward) > 0.5 && dir.magnitude < viewDistance)
            navAgent.destination = player.transform.position;
    }
}
