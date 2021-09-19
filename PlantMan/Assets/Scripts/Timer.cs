using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    public TextMeshProUGUI timer;
    public bool end = false;
    public float timerNumber = 300;

    private void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (end == false)
        {
            UpdateUI();
            UpdateTime();
        }
        if(timerNumber <= 0)
        {
            int scene = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(scene, LoadSceneMode.Single);
        }
    }

    void UpdateTime()
    {
        timerNumber -= Time.deltaTime;
    }

    void UpdateUI()
    {
        timer.text = timerNumber.ToString("f0");
    }
}
