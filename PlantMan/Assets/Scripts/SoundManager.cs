using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{

    public string[] soundNames;
    public AudioClip[] soundClips;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

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
