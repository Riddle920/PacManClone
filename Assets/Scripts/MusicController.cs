using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{

    public AudioSource currentlyPlaying;
    public AudioClip levelStart;
    public AudioClip normalState;
    
    // Start is called before the first frame update
    void Start()
    {
        currentlyPlaying.clip = levelStart;
        currentlyPlaying.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (!currentlyPlaying.isPlaying)
        {
            currentlyPlaying.clip = normalState;
            currentlyPlaying.Play();
        }
    }
}
