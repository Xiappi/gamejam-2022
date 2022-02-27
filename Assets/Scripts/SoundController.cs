using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{

    private AudioSource[] audioSources;
    void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Music");
        audioSources = GetComponents<AudioSource>();

        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
    }



    public void PlayJumpSound()
    {
        // TODO: implement
        Debug.Log("TODO: player jump sound");
    }

    public void PlayAttackSound()
    {
        audioSources[3].Play();
    }

    public void PlayDeathSound()
    {
        audioSources[1].Play();
    }

    public void PlaySheepSavedSound()
    {
        audioSources[2].Play();
    }
}