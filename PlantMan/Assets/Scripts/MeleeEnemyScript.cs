using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemyScript : MonoBehaviour
{

    public float attackCooldown;
    public float damageAmount;
    public GameObject weaponModel;
    public float animationTime;

    private EnemyMovement movementScript;
    private GameObject player;
    private float timer;
    private bool inRange;

    // Start is called before the first frame update
    void Start()
    {
        movementScript = GetComponent<EnemyMovement>();
        player = GameObject.FindGameObjectWithTag("Player");
        inRange = false;
        weaponModel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (timer < attackCooldown)
            timer += Time.deltaTime;

        if (timer > animationTime)
            weaponModel.SetActive(false);

        if (!movementScript.isTracking() && inRange && timer >= attackCooldown)
        {
            timer = 0;
            Attack();
        }
    }

    void Attack()
    {
        weaponModel.SetActive(true);
        player.GetComponent<PlayerScript>().Damage(damageAmount);
    }

    public void setInRange(bool val)
    {
        inRange = val;
    }
}
