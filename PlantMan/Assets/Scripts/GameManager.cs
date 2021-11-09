using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{

    public Canvas pauseUI;
    public Canvas seedSwitchUI;
    public Canvas gameUI;

    private Vector2 dir;

    public GameObject[] slice, highlight;
    public string[] names, descriptions;
    public TextMeshProUGUI seedName, description;

    public Sprite[] icons;
    public Image seedIcon;

    public bool isPaused;
    public bool isGrappling;

    private seed seedChoice;

    public PlayerAim playerAimScript;
    public GameObject player;

    public GameObject topSeed;
    public GameObject rightSeed;
    public GameObject bottomSeed;
    public GameObject leftSeed;

    private Vector3 checkpoint;
    private int checkpointPriority;
    private Scene currentScene;

    private GameObject[] resetObjects;
    private Transform[] resetTransforms;

    enum seed
    {
        Y,
        G,
        R,
        B
    }

    private void Start()
    {
        isPaused = false;
        seedChoice = seed.Y;
        checkpointPriority = -1;
        currentScene = SceneManager.GetActiveScene();
    }


    private void Update()
    {
        isGrappling = player.GetComponent<PlayerGrapple>().grappleToObj;
        if (isPaused && seedSwitchUI.gameObject.activeInHierarchy) //If in Seed Switching mode
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
                Debug.Log("Q2");
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
        if(Time.timeScale == 1.0f && !isPaused)
        {
            isPaused = true;
            Time.timeScale = 0.0f;
            gameUI.gameObject.SetActive(false);
            pauseUI.gameObject.SetActive(true);
        }
        else if(pauseUI.gameObject.activeInHierarchy && isPaused)
        {
            isPaused = false;
            Time.timeScale = 1.0f;
            gameUI.gameObject.SetActive(true);
            pauseUI.gameObject.SetActive(false);
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
        if(context.started && !isPaused)
        {
            isPaused = true;
            Time.timeScale = 0.0f;
            gameUI.gameObject.SetActive(false);
            seedSwitchUI.gameObject.SetActive(true);
        }
        else if(context.canceled && seedSwitchUI.gameObject.activeInHierarchy && isPaused)
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
            isPaused = false;
            gameUI.gameObject.SetActive(true);
            seedSwitchUI.gameObject.SetActive(false);
            Time.timeScale = 1.0f;
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
       // Debug.Log(seedChoice);
        slice[(int)seedChoice].SetActive(true);
        highlight[(int)seedChoice].SetActive(false);

        seedChoice = newSeed;

        slice[(int)seedChoice].SetActive(false);
        highlight[(int)seedChoice].SetActive(true);
        seedName.text = names[(int)seedChoice];
        description.text = descriptions[(int)seedChoice];
        seedIcon.sprite = icons[(int)seedChoice]; 

    }

    /*
    * Purpose: Handles Death when a player touches a trap.
    * References: Called by TrapScript
    * Scripts Called: None
    * Status: in progress
    * Contributers: Brandon L'Abbe
    */
    public void PlayerDeath()
    {

        if(checkpoint == null)
        {
            player.transform.position = new Vector3(0, 0, 0);
        }
        else
        {
            player.transform.position = checkpoint;
        }

        if(resetObjects != null)
            for (int i = 0; i < resetObjects.Length; i++)
            {
                resetObjects[i].transform.position = resetTransforms[i].position;
                resetObjects[i].transform.rotation = resetTransforms[i].rotation;
                resetObjects[i].transform.localScale = resetTransforms[i].localScale;
            }

        KillAllPlants();
    }

    void KillAllPlants()
    {
        GameObject[] plants = GameObject.FindGameObjectsWithTag("Seed");
        Debug.Log("HERE");
        for (int i = 0; i < plants.Length; i++)
        {
            Destroy(plants[i]);
        }
    }

    /*
    * Purpose: Set a new checkpoint when the player passes one. Uses a priority system so the player cannot go backwards in checkpoints.
    * References: Called by CheckpointScript
    * Scripts Called: None
    * Status: in progress
    * Contributers: Brandon L'Abbe
    */
    public void SetCheckpoint(Vector3 loc, int priority)
    {
        if(priority > checkpointPriority)
        {
            checkpoint = loc;
            checkpointPriority = priority;
        }
    }

    public void SetCheckpoint(Vector3 loc, int priority, GameObject[] objs)
    {
        if (priority > checkpointPriority)
        {
            checkpoint = loc;
            checkpointPriority = priority;
            resetObjects = objs;
            resetTransforms = new Transform[objs.Length];

            for(int i = 0; i<objs.Length; i++)
            {
                resetTransforms[i] = new GameObject().transform;
                resetTransforms[i].position = objs[i].transform.position;
                resetTransforms[i].rotation = objs[i].transform.rotation;
                resetTransforms[i].localScale = objs[i].transform.localScale;
            }

        }
    }

    public bool getIsPaused()
    {
        return isPaused;
    }
}
