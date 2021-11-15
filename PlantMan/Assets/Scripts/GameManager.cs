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

    public float animDirection;

    public bool isPaused;
    public bool isGrappling;

    public PlayerAim playerAimScript;
    public GameObject player;

    public GameObject[] seedPrefabs;

    private Vector3 checkpoint;
    private int checkpointPriority;
    private Scene currentScene;

    private GameObject[] resetObjects;
    private Transform[] resetTransforms;

    private float levelTimer;
    public float completionTimeFeatTime;

    private int seedChoice;

    private int seedSelection;

    private bool hasSelection;

    private void Start()
    {
        isPaused = false;
        seedChoice = 0;
        checkpointPriority = -1;
        currentScene = SceneManager.GetActiveScene();
        levelTimer = 0.0f;
        hasSelection = false;
    }


    private void Update()
    {
        isGrappling = player.GetComponent<PlayerGrapple>().grappleToObj;
        if (isPaused && seedSwitchUI.gameObject.activeInHierarchy) //If in Seed Switching mode
        {
            if(dir == Vector2.zero)
            {
                updateSeed(-1);
            }
            else
            {
                Vector2 dirNorm = dir.normalized;

                bool yGreater = (Mathf.Abs(dirNorm.y) > Mathf.Abs(dirNorm.x)); //Determine which quadrant our movement selection is in

                bool xGreater = (Mathf.Abs(dirNorm.x) > Mathf.Abs(dirNorm.y));//Determine which quadrant our movement selection is in

                float deg = ((-(Mathf.Atan2(dirNorm.y, dirNorm.x)) * Mathf.Rad2Deg) + 180);
                deg -= 67.5f;
                if (deg < 0.0f)
                    deg += 360;

                seedSelection = (int)(deg / 45.0f);

                updateSeed(seedSelection);
            }

        }

        levelTimer += Time.deltaTime;

        //if(dir.y == 0)
        //{
        //    isIdle = true;
        //}

        //if(levelTimer > completionTimeFeatTime)
        //    GameObject.Find("FeatManager").GetComponent<FeatManager>().DisableFeat("Completion Time");
    }

    /*
    * Purpose: Pauses the game and activates the Pause UI
    * References: called by InputManager attached to player object
    * Scripts Called: None
    * Status: working
    */
    public void Pause(InputAction.CallbackContext context)
    {
        if (Time.timeScale == 1.0f)
        {
            isPaused = true;
            Time.timeScale = 0.0f;
            gameUI.gameObject.SetActive(false);
            pauseUI.gameObject.SetActive(true);
            pauseUI.gameObject.GetComponent<PauseMenuScript>().SetButton();
            player.GetComponent<PlayerInput>().SwitchCurrentActionMap("UI");
        }

    }

    public void Play()
    {
        if (Time.timeScale == 0.0f)
        {
            Time.timeScale = 1.0f;
            gameUI.gameObject.SetActive(true);
            isPaused = false;
            player.GetComponent<PlayerInput>().SwitchCurrentActionMap("PlayerControlScheme");
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
            player.GetComponent<PlayerInput>().SwitchCurrentActionMap("UI");
            isPaused = true;
            Time.timeScale = 0.0f;
            gameUI.gameObject.SetActive(false);
            seedSwitchUI.gameObject.SetActive(true);
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

        animDirection = context.ReadValue<Vector2>().normalized.y;

        dir = context.ReadValue<Vector2>();

        if (context.canceled)
        {
            dir = Vector2.zero;
        }
        
    }

    public void SelectSeed(InputAction.CallbackContext context)
    {
        Debug.Log("CALLED");
        if (context.started && isPaused && seedSwitchUI.gameObject.activeInHierarchy && hasSelection) //If in Seed Switching mode
        {

            playerAimScript.seedToShoot = seedPrefabs[seedChoice];
            seedIcon.sprite = icons[seedChoice];

            player.GetComponent<PlayerInput>().SwitchCurrentActionMap("PlayerControlScheme");

            isPaused = false;
            gameUI.gameObject.SetActive(true);
            seedSwitchUI.gameObject.SetActive(false);
            Time.timeScale = 1.0f;

        }
    }

    /*
    * Purpose: Switches the seed to match the seed chosen in the update function
    * References: called by Update()
    * Scripts Called: None
    * Status: working
    */
    private void updateSeed(int newSeed)
    {
       // Debug.Log(seedChoice);
        slice[seedChoice].SetActive(true);
        highlight[seedChoice].SetActive(false);

        if(newSeed != -1)
        {
            hasSelection = true;
            seedChoice = seedSelection;

            slice[seedChoice].SetActive(false);
            highlight[seedChoice].SetActive(true);
            seedName.text = names[seedChoice];
            description.text = descriptions[seedChoice];
        }
        else
        {
            hasSelection = false;
        }
        

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

        GameObject.Find("FeatManager").GetComponent<FeatManager>().DisableFeat("Survivalist");

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
