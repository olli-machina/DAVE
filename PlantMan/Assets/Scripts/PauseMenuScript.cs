using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class PauseMenuScript : MonoBehaviour
{
    GameObject gameManager;
    public Button[] options, settings;
    public GameObject[] pointers;
    public GameObject infoPanel, controls, feats;
    GameObject openPanel, prevPanel;
    

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager");
        options[0].Select();
    }

    /*
     * Purpose: Close the game
     * References: ---
     * Scripts Called: ---
     * Status: working
     *  Contributers: Carter Ivancic
     */
    public void Quit()
    {
        Application.Quit();
    }

    /*
     * Purpose: Selects the first button
     * References: ---
     * Scripts Called: ---
     * Status: working
     * Contributers: Carter Ivancic
     */
    public void SetButton()
    {
        options[0].Select();
    }

    /*
     * Purpose: Resume playing the game
     * References: ---
     * Scripts Called: ---
     * Status: working
     * Contributers: Carter Ivancic
     */
    public void Resume()
    {
        gameManager.GetComponent<GameManager>().Play();
        gameObject.SetActive(false);
    }

    /*
     * Purpose: Closes the info panel of the pause menu
     * References: ---
     * Scripts Called: ---
     * Status: working
     * Contributers: Carter Ivancic
     */
    public void CloseInfo(InputAction.CallbackContext context)
    {
        Debug.Log("Closing info panel");
        CloseControls();
        infoPanel.SetActive(false);
        for(int i = 0; i < options.Length; i++)
        {
            options[i].interactable = true;
        }
        options[0].Select();
    }

    /*
     * Purpose: Opens the info panel of the pause menu
     * References: ---
     * Scripts Called: ---
     * Status: working
     * Contributers: Carter Ivancic
     */
    public void OpenInfo(Button clicked)
    {
        infoPanel.SetActive(true);
        for (int i = 0; i < options.Length; i++)
        {
            if (options[i] != clicked)
            {
                options[i].interactable = false;
            }
            //Turns on the correct arrow from the corresponding button
            else if (pointers[i] != null)
            {
                pointers[i].SetActive(true);
            }
        }
    }

    public void OpenSettings(Button clicked)
    {
        settings[0].Select();
    }

    /*
     * Purpose: Opens the controls image of the info panel
     * References: ---
     * Scripts Called: ---
     * Status: working
     * Contributers: Carter Ivancic
     */
    public void ShowControls()
    {
        controls.SetActive(true);
    }

    /*
     * Purpose: Closes the controls image of the info panel
     * References: ---
     * Scripts Called: ---
     * Status: working
     * Contributers: Carter Ivancic
     */
    public void CloseControls()
    {
        controls.SetActive(false);
    }

    /*
    * Purpose: Opens the feats page of the info panel
    * References: ---
    * Scripts Called: ---
    * Status: working
    * Contributers: Carter Ivancic
    */
    public void ShowFeats()
    {
        feats.SetActive(true);
    }

    /*
    * Purpose: Opens the feats page of the info panel
    * References: ---
    * Scripts Called: ---
    * Status: working
    * Contributers: Carter Ivancic
    */
    public void CloseFeats()
    {
        feats.SetActive(false);
    }
}
