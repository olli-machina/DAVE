using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public float startingHealth;

    private float health;

    // Start is called before the first frame update
    void Start()
    {
        health = startingHealth;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Damage(float dmg)
    {
        health -= dmg;
        if (health <= 0)
            Death();
    }

    void Death()
    {
        Destroy(gameObject);
    }
}
