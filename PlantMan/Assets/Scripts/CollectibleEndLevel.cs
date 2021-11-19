using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class CollectibleEndLevel : MonoBehaviour
{
    public string nextLevel;
    public GameObject sceneUI;
    bool triggered;

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GameObject.Find("FeatManager").GetComponent<FeatManager>().fireFeat("Sniper");
            GameObject.Find("FeatManager").GetComponent<FeatManager>().fireFeat("Survivalist");
            GameObject.Find("FeatManager").GetComponent<FeatManager>().fireFeat("Completion Time");
            Instantiate(sceneUI);

            gameObject.GetComponent<MeshRenderer>().enabled = false;
            gameObject.GetComponent<BoxCollider>().enabled = false;
        }
        triggered = true;
    }

    public void NextLevel(InputAction.CallbackContext context)
    {
        if (triggered)
        SceneManager.LoadScene(nextLevel);
    }
}
