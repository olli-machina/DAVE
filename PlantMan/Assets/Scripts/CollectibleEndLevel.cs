using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class CollectibleEndLevel : MonoBehaviour
{
    public string nextLevel;
    public GameObject sceneUI;

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GameObject.Find("FeatManager").GetComponent<FeatManager>().fireFeat("Sniper");
            GameObject.Find("FeatManager").GetComponent<FeatManager>().fireFeat("Survivalist");
            GameObject.Find("FeatManager").GetComponent<FeatManager>().fireFeat("Completion Time");
            sceneUI.SetActive(true);

            gameObject.GetComponent<MeshRenderer>().enabled = false;
            gameObject.GetComponent<BoxCollider>().enabled = false;
        }
    }

    public void NextLevel(InputAction.CallbackContext context)
    {
        SceneManager.LoadScene(nextLevel);
    }
}
