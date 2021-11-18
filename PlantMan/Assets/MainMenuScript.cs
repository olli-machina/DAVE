using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    public string lvlName;
    public void StartGame()
    {
        SceneManager.LoadScene(lvlName);
    }
}
