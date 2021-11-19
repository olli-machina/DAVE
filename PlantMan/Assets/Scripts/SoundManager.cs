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
    public void Play(string name, float volume = 1.0f)
    {
        for(int i = 0; i < soundNames.Length; i++)
        {
            if (soundNames[i] == name)
            {
                Play(i, volume);
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

    public void Play(string name, GameObject target, float volume = 1.0f)
    {
        for (int i = 0; i < soundNames.Length; i++)
        {
            if (soundNames[i] == name)
            {
                Play(i, target, volume);
                Debug.Log("CRY");
                break;
            }
        }
    }

    public void Play(int id, GameObject target, float volume = 1.0f)
    {
        AudioSource pas = target.GetComponent<AudioSource>();

        if(pas == null)
        {
            Debug.LogError("Attempted to play " + soundNames[id] + " on the GameObject " + target.name + ", but it doesn't have an audio source!");
        }

        pas.Stop();
        pas.clip = soundClips[id];
        pas.loop = false;
        pas.volume = volume;
        pas.Play();

    }

    /*
    * Purpose: Find the sound loop using the name string
    * References: None
    * Scripts Called: None
    * Status: 
    * Contributor(s): Brandon L'Abbe
    */
    public void PlayLoop(string name, float volume = 1.0f)
    {
        for (int i = 0; i < soundNames.Length; i++)
        {
            if (soundNames[i] == name)
            {
                PlayLoop(i, volume);
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

    public void PlayLoop(string name, GameObject target, float volume = 1.0f)
    {
        for (int i = 0; i < soundNames.Length; i++)
        {
            if (soundNames[i] == name)
            {
                PlayLoop(i, target, volume);
                break;
            }
        }
    }

    public void PlayLoop(int id, GameObject target, float volume = 1.0f)
    {
        AudioSource pas = target.GetComponent<AudioSource>();

        if (pas == null)
        {
            Debug.LogError("Attempted to play " + soundNames[id] + " on the GameObject " + target.name + ", but it doesn't have an audio source!");
        }

        pas.Stop();
        pas.clip = soundClips[id];
        pas.loop = true;
        pas.volume = volume;
        pas.Play();

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


    /*
    * Purpose: Stop the sound loop using the id
    * References: StopLoop(int id)
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

    public void SetVolume(GameObject target, float volume)
    {
        AudioSource pas = target.GetComponent<AudioSource>();

        if (pas == null)
        {
            Debug.LogError("Attempted to set volume on the GameObject " + target.name + ", but it doesn't have an audio source!");
        }

        pas.volume = volume;

    }    
    
    public void SetVolumeDelay(GameObject target, float volume, float delayTime, float returnVolume)
    {
        AudioSource pas = target.GetComponent<AudioSource>();

        if (pas == null)
        {
            Debug.LogError("Attempted to set volume on the GameObject " + target.name + ", but it doesn't have an audio source!");
        }

        pas.volume = volume;


        IEnumerator coroutine = VolumeDelay(delayTime, pas, returnVolume);
        StartCoroutine(coroutine);



    }

    IEnumerator VolumeDelay(float delayTime, AudioSource audio, float returnVolume)
    {
        yield return new WaitForSeconds(delayTime + 0.5f); //0.5 is a small buffer
        audio.volume = returnVolume;
    }

}
