using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour
{
    public float startingHealth;
    public float maxArmor;

    private float health;
    private float armor;
    // Start is called before the first frame update
    void Start()
    {
        health = startingHealth;
        armor = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Damage(float amount)
    {
        if(armor > 0)
        {
            armor -= amount;
            if(armor < 0)
            {
                health -= -armor; //Armor is negative the amount that should be deducted from the health. So flip the number to positive and subtract health!
                armor = 0;
            }
        }
        else
        {
            health -= amount;
        }

        if(health <= 0)
        {
            Death();
        }
    }

    void Death()
    {
        int scene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(scene, LoadSceneMode.Single);
    }
}
