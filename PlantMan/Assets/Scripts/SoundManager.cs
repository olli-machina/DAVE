using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{

    public string[] soundNames;
    public AudioClip[] soundClips;

    /*
    * Purpose: Find the sound effect using the name string
    * References: None
    * Scripts Called: None
    * Status: 
    * Contributor(s): Brandon L'Abbe
    */
    public void Play(string name)
    {
        for(int i = 0; i < soundNames.Length; i++)
        {
            if (soundNames[i] == name)
            {
                Play(i);
                break;
            }
        }
    }

    /*
    * Purpose: Play the sound effect using id
    * References: Play(string name)
    * Scripts Called: GameManager
    * Status: 
    * Contributor(s): Brandon L'Abbe
    */
    public void Play(int id, float volume = 1.0f)
    {
        AudioSource[] pas = GameObject.Find("GameManager").GetComponent<GameManager>().player.GetComponents<AudioSource>();
        for (int i = 0; i < pas.Length; i++)
        {
            if (pas[i].isPlaying)
                continue;
            else
            {
                pas[i].clip = soundClips[id];
                pas[i].loop = false;
                pas[i].volume = volume;
                pas[i].Play();
                break;
            }

        }
        
    }

    /*
    * Purpose: Find the sound loop using the name string
    * References: None
    * Scripts Called: None
    * Status: 
    * Contributor(s): Brandon L'Abbe
    */
    public void PlayLoop(string name)
    {
        for (int i = 0; i < soundNames.Length; i++)
        {
            if (soundNames[i] == name)
            {
                PlayLoop(i);
                break;
            }
        }
    }

    /*
    * Purpose: play the sound loop using the loop id
    * References: PlayLoop(string name)
    * Scripts Called: GameManager
    * Status: 
    * Contributor(s): Brandon L'Abbe
    */
    public void PlayLoop(int id, float volume = 1.0f)
    {
        AudioSource[] pas = GameObject.Find("GameManager").GetComponent<GameManager>().player.GetComponents<AudioSource>();
        for (int i = 0; i < pas.Length; i++)
        {
            if (pas[i].isPlaying)
                continue;
            else
            {
                pas[i].clip = soundClips[id];
                pas[i].loop = true;
                pas[i].volume = volume;
                pas[i].Play();
                break;
            }

        }

    }

    /*
    * Purpose: Find the sound loop using the loop name
    * References: None
    * Scripts Called: None
    * Status: 
    * Contributor(s): Brandon L'Abbe
    */
    public void StopLoop(string name)
    {
        for (int i = 0; i < soundNames.Length; i++)
        {
            if (soundNames[i] == name)
            {
                StopLoop(i);
                break;
            }
        }
    }

    /*
    * Purpose: Stop the sound loop using the id
    * References: StopLoop(string name)
    * Scripts Called: GameManager
    * Status: 
    * Contributor(s): Brandon L'Abbe
    */
    public void StopLoop(int id)
    {
        AudioSource[] pas = GameObject.Find("GameManager").GetComponent<GameManager>().player.GetComponents<AudioSource>();
        for (int i = 0; i < pas.Length; i++)
        {
            if (!pas[i].isPlaying)
                continue;
            else
            {
                if(pas[i].loop && pas[i].clip == soundClips[id])
                {
                    pas[i].Stop();
                    break;
                }
            }

        }

    }
}
