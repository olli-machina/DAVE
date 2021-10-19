using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public Canvas pauseUI;
    public Canvas seedSwitchUI;

    private bool currentlyActive;
    private Vector2 dir;

    public GameObject[] guiColors;

    private seed seedChoice;

    public PlayerAim playerAimScript;

    public GameObject topSeed;
    public GameObject rightSeed;
    public GameObject bottomSeed;
    public GameObject leftSeed;

    enum seed
    {
        Y,
        G,
        R,
        B
    }

    private void Start()
    {
        currentlyActive = false;
        seedChoice = seed.Y; 

    }


    private void Update()
    {
        
        if (currentlyActive && seedSwitchUI.gameObject.activeInHierarchy) //If in Seed Switching mode
        {
            Vector2 dirNorm = dir.normalized;

            bool yGreater = (Mathf.Abs(dirNorm.y) > Mathf.Abs(dirNorm.x)); //Determine which quadrant our movement selection is in

            bool xGreater = (Mathf.Abs(dirNorm.x) > Mathf.Abs(dirNorm.y));//Determine which quadrant our movement selection is in

            //Y is top, G is right, R is down, B ir left
            if (dirNorm.y > 0.0f && dirNorm.x > 0.0f) //Q1
            {
                if (yGreater)
                {
                    updateSeed(seed.Y);
                }
                else if (xGreater)
                {
                    updateSeed(seed.G);
                }
            }
            else if (dirNorm.y > 0.0f && dirNorm.x < 0.0f) //Q2
            {
                if (yGreater)
                {
                    updateSeed(seed.Y);
                }
                else if (xGreater)
                {
                    updateSeed(seed.B);
                }
            }
            else if (dirNorm.y < 0.0f && dirNorm.x < 0.0f) //Q3
            {
                if (yGreater)
                {
                    updateSeed(seed.R);
                }
                else if (xGreater)
                {
                    updateSeed(seed.B);
                }
            }
            else if (dirNorm.y < 0.0f && dirNorm.x > 0.0f) //Q4
            {
                if (yGreater)
                {
                    updateSeed(seed.R);
                }
                else if (xGreater)
                {
                    updateSeed(seed.G);
                }
            }
            else if (dirNorm.y > 0.0f && dirNorm.x == 0.0f)
            {
                updateSeed(seed.Y);
            }
            else if (dirNorm.y < 0.0f && dirNorm.x == 0.0f)
            {
                updateSeed(seed.R);
            }
            else if (dirNorm.y == 0.0f && dirNorm.x > 0.0f)
            {
                updateSeed(seed.G);
            }
            else if (dirNorm.y == 0.0f && dirNorm.x < 0.0f)
            {
                updateSeed(seed.B);
            }
        }
    }

    /*
    * Purpose: Pauses or unpauses the game, and activates the Pause UI
    * References: called by InputManager attached to player object
    * Scripts Called: None
    * Status: working
    */
    public void Pause(InputAction.CallbackContext context)
    {
        if(Time.timeScale == 1.0f && !currentlyActive)
        {
            Time.timeScale = 0.0f;
            pauseUI.gameObject.SetActive(true);
            currentlyActive = true;
        }
        else if(pauseUI.gameObject.activeInHierarchy && currentlyActive)
        {
            Time.timeScale = 1.0f;
            pauseUI.gameObject.SetActive(false);
            currentlyActive = false;
        }
        
    }

    /*
    * Purpose: Pauses or unpauses the game, and activates the Seed Switch UI. Allows the player to switch the active seed in the UI
    * References: called by InputManager attached to player object
    * Scripts Called: PlayerAttackScript on player object
    * Status: working
    */
    public void SeedSwitch(InputAction.CallbackContext context)
    {
        if(context.started && !currentlyActive)
        {
            Time.timeScale = 0.0f;
            seedSwitchUI.gameObject.SetActive(true);
            currentlyActive = true;
        }
        else if(context.canceled && seedSwitchUI.gameObject.activeInHierarchy && currentlyActive)
        {
            Debug.Log("OFF");
            switch(seedChoice)
            {
                case seed.Y:
                    playerAimScript.seedToShoot = topSeed;
                    break;            
                case seed.G:          
                    playerAimScript.seedToShoot = rightSeed;
                    break;            
                case seed.R:          
                    playerAimScript.seedToShoot = bottomSeed;
                    break;            
                case seed.B:          
                    playerAimScript.seedToShoot = leftSeed;
                    break;            
                default:              
                    playerAimScript.seedToShoot = topSeed;
                    break;
            }

            seedSwitchUI.gameObject.SetActive(false);
            Time.timeScale = 1.0f;
            currentlyActive = false;
        }

    }

    /*
    * Purpose: sets movement direction vector, so that it can be used by the SeedSwitch UI
    * References: called by InputManager attached to player object
    * Scripts Called: None
    * Status: working
    */
    public void Direction(InputAction.CallbackContext context)
    {
        
        dir = context.ReadValue<Vector2>();
        
    }

    /*
    * Purpose: Switches the seed to match the seed chosen in the update function
    * References: called by Update()
    * Scripts Called: None
    * Status: working
    */
    private void updateSeed(seed newSeed)
    {
        

        Color col = guiColors[(int)seedChoice].GetComponent<Image>().color;
        col.a = 0.5f;

        guiColors[(int)seedChoice].GetComponent<Image>().color = col;

        seedChoice = newSeed;

        col = guiColors[(int)seedChoice].GetComponent<Image>().color;
        col.a = 1.0f;

        guiColors[(int)seedChoice].GetComponent<Image>().color = col;
    }
}
