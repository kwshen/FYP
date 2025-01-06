using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterCrabAudio : MonoBehaviour
{
    private AudioSource crabAudioSource;
    public AudioClip jumpClip;

    // Start is called before the first frame update
    void Start()
    {
        crabAudioSource = GetComponent<AudioSource>();
    }

    public void playJumpSound()
    {
        crabAudioSource.clip = jumpClip;
        crabAudioSource.Play();
    }
}
