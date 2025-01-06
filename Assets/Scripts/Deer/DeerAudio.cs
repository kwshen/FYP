using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeerAudio : MonoBehaviour
{

    AudioSource deerAudioSource;
    public AudioClip clip;

    // Start is called before the first frame update
    void Start()
    {
        deerAudioSource = GetComponent<AudioSource>();
        deerAudioSource.clip = clip;
        deerAudioSource.Play();
    }
}
