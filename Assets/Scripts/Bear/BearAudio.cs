using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearAudio : MonoBehaviour
{

    AudioSource bearAudioSource;
    public AudioClip walkClip;
    public AudioClip sleepClip;
    private BearController bearController;

    // Start is called before the first frame update
    void Start()
    {
        bearAudioSource = GetComponent<AudioSource>();
        bearController = GetComponent<BearController>();
        bearAudioSource.clip = walkClip;
        bearAudioSource.Play();
    }
    private void Update()
    {
        if((int)bearController.currentState == 3)
        {
            bearAudioSource.clip = sleepClip;
            bearAudioSource.Play();
        }
        else
        {
            bearAudioSource.clip = walkClip;
            bearAudioSource.Play();
        }
    }
}
