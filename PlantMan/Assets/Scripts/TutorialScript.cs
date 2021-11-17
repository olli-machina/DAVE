using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Playables;
using DG.Tweening;

public class TutorialScript : MonoBehaviour
{    
    public Canvas tutorialCanvas;
    public TextMeshProUGUI title, description;
    public string titleText, descrText;
    static GameObject obj;
    GameObject gamemanager;
    public GameObject timelineObj1, timelineObj2, hint;
    bool canClose;

    // Start is called before the first frame update
    void Start()
    {
        gamemanager = GameObject.Find("GameManager");
    }

    /*
    * Purpose: Updates the tutorial UI for the corresponding collider in the world, then pauses the game
    * References: None
    * Scripts Called: None
    * Status: 
    * Contributor(s): Carter Ivancic, Christian Roby, debugged by Olli
    */
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (timelineObj1 != null)
            {
                timelineObj1.GetComponent<PlayableDirector>().Play();
            }
            obj = gameObject;
            gamemanager.GetComponent<GameManager>().gameUI.gameObject.SetActive(false);
            tutorialCanvas.gameObject.SetActive(true);
            gamemanager.GetComponent<GameManager>().isPaused = true;
            Time.timeScale = 0.0f;
            title.text = titleText;
            description.text = descrText;
            hint.SetActive(false);
            canClose = false;
            StartCoroutine("Delay");
        }
        
    }

    /*
    * Purpose: Closes the tutorial UI, destroys the collider and resumes the game
    * References: called by InputManager attached to player object
    * Scripts Called: None
    * Status: 
    * Contributor(s): Carter Ivancic, Christian Roby
    */
    public void CloseTutorial(InputAction.CallbackContext context)
    {
        if (Time.timeScale == 0.0f && canClose)
        {
            if (timelineObj2 != null)
            {
                timelineObj2.GetComponent<PlayableDirector>().Play();
            }
            tutorialCanvas.gameObject.SetActive(false);
            gamemanager.GetComponent<GameManager>().gameUI.gameObject.SetActive(true);
            gamemanager.GetComponent<GameManager>().isPaused = false;
            Time.timeScale = 1.0f;
            Destroy(obj);
        }
    }

    IEnumerator Delay()
    {
        Debug.Log("Start delay");
        yield return new WaitForSecondsRealtime(3);
        hint.SetActive(true);
        canClose = true;
    }
}
