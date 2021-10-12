using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{

    public Canvas pauseUI;

    private void Start()
    {
        
    }

    private void Update()
    {

    }

    public void Pause(InputAction.CallbackContext context)
    {
        if(Time.timeScale == 1.0f)
        {
            Time.timeScale = 0.0f;
            pauseUI.gameObject.SetActive(true);
        }
        else
        {
            Time.timeScale = 1.0f;
            pauseUI.gameObject.SetActive(false);
        }
        
    }

}
