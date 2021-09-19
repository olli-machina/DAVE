using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{

    public float damageAmount; //value to be set by the enemy who shot this projetile.

    private float lifeSpan;
    private float timer;

    // Start is called before the first frame update
    void Start()
    {
        lifeSpan = 20;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer > lifeSpan)
            Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerScript>().Damage(damageAmount);
        }

        Destroy(gameObject);
    }
}
