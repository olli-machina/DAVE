using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class PauseMenuScript : MonoBehaviour
{
    GameObject gameManager;
    public Button[] optionButtons, settingButtons;
    public GameObject[] pointers;
    public GameObject infoPanel, settingsPanel, controlsPanel, gamePanel, videoPanel, audioPanel, featsPanel;
    GameObject openPanel, prevPanel;
    int numOpen;

    SpriteState selected;
    

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager");
        optionButtons[0].Select();
        numOpen = 0;
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
        optionButtons[0].Select();
        infoPanel.SetActive(false);
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
    public void CloseOpenPanel(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            openPanel.SetActive(false);
            if (numOpen > 1)
            {        
            
                CloseControls();
                CloseGameplay();
                CloseVideo();
                CloseAudio();

                openPanel = prevPanel;
            }
            else
            {
                CloseInfo();
                prevPanel = null;
            }
            numOpen--;
        }
    }

    void CloseInfo()
    {
        infoPanel.SetActive(false);
        for (int i = 0; i < optionButtons.Length; i++)
        {
            optionButtons[i].interactable = true;
            if (pointers[i] != null)
            {
                pointers[i].SetActive(false);
            }
        }
        optionButtons[0].Select();
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
        for (int i = 0; i < optionButtons.Length; i++)
        {
            if (optionButtons[i] != clicked)
            {
                optionButtons[i].interactable = false;
            }
            //Turns on the correct arrow from the corresponding button
            else if (pointers[i] != null)
            {
                pointers[i].SetActive(true);
            }
        }
    }

    public void OpenSettings()
    {
        numOpen++;
        settingsPanel.SetActive(true);
        settingButtons[0].Select();
        openPanel = settingsPanel;
        prevPanel = settingsPanel;
    }

    public void ClickedSetting(Button clicked)
    {
        for (int i = 0; i < settingButtons.Length; i++)
        {
            if (settingButtons[i] != clicked)
            {
                settingButtons[i].interactable = false;
            }
        }
    }

    /*
     * Purpose: Opens the controls image of the info panel
     * References: ---
     * Scripts Called: ---
     * Status: working
     * Contributers: Carter Ivancic
     */
    public void OpenControls()
    {
        numOpen++;
        controlsPanel.SetActive(true);
        openPanel = controlsPanel;
    }

    /*
     * Purpose: Closes the controls image of the info panel
     * References: ---
     * Scripts Called: ---
     * Status: working
     * Contributers: Carter Ivancic
     */
    void CloseControls()
    {
        controlsPanel.SetActive(false);
        for (int i = 0; i < settingButtons.Length; i++)
        {
            settingButtons[i].interactable = true;
        }        
        settingButtons[0].Select();
    }

    public void OpenGameplay()
    {

    }

    void CloseGameplay()
    {

    }

    public void OpenVideo()
    {

    }

    void CloseVideo()
    {

    }

    public void OpenAudio()
    {

    }

    void CloseAudio()
    {

    }

    /*
    * Purpose: Opens the feats page of the info panel
    * References: ---
    * Scripts Called: ---
    * Status: working
    * Contributers: Carter Ivancic
    */
    public void OpenFeats()
    {
        numOpen++;
        featsPanel.SetActive(true);
        openPanel = featsPanel;
    }
}
