using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{

    void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Music");

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

    public void PlayShootSound()
    {
        // TODO: implement
        Debug.Log("TODO: player shoot sound");
    }

    public void PlayDeathSound()
    {
        // TODO: implement
        Debug.Log("TODO: player death sound");
    }
}
