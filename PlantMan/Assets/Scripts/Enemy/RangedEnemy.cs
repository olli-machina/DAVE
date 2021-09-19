using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : MonoBehaviour
{
    public float ShootCooldown;
    public GameObject bullet;
    public float shotForce;
    public float damage;

    private EnemyMovement movementScript;
    private GameObject player;
    private float timer;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        movementScript = GetComponent<EnemyMovement>();
    }

    // Update is called once per frame
    void Update()
    {

        if (timer < ShootCooldown)
            timer += Time.deltaTime;

        if(!movementScript.isTracking() && timer >= ShootCooldown)
        {
            timer = 0;
            Shoot();
        }
    }

    void Shoot()
    {
        Vector3 dir = player.transform.position - transform.position;
        dir.Normalize();

        GameObject shot = Instantiate(bullet);
        shot.transform.position = transform.position;
        shot.GetComponent<Rigidbody>().AddForce(shotForce * dir);

        shot.GetComponent<ProjectileScript>().damageAmount = damage;
    }
}
