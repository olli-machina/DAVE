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

    public PlayerAttackScript playerAttackScript;

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
        
        if (currentlyActive && seedSwitchUI.gameObject.activeInHierarchy)
        {
            Vector2 dirNorm = dir.normalized;

            bool yGreater = (Mathf.Abs(dirNorm.y) > Mathf.Abs(dirNorm.x));

            bool xGreater = (Mathf.Abs(dirNorm.x) > Mathf.Abs(dirNorm.y));

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

            switch(seedChoice)
            {
                case seed.Y:
                    playerAttackScript.seedToShoot = topSeed;
                    break;
                case seed.G:
                    playerAttackScript.seedToShoot = rightSeed;
                    break;
                case seed.R:
                    playerAttackScript.seedToShoot = bottomSeed;
                    break;
                case seed.B:
                    playerAttackScript.seedToShoot = leftSeed;
                    break;
                default:
                    playerAttackScript.seedToShoot = topSeed;
                    break;
            }

            seedSwitchUI.gameObject.SetActive(false);
            Time.timeScale = 1.0f;
            currentlyActive = false;
        }

    }

    public void Direction(InputAction.CallbackContext context)
    {
        
        dir = context.ReadValue<Vector2>();
        
    }

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
