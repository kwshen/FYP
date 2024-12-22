using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public Sound[] musicSounds, sfxSounds;
    public AudioSource musicSource, sfxSource;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayMusic(string clipName)
    {
        Sound s = Array.Find(musicSounds, x => x.name == clipName);

        if (s == null)
        {
            Debug.Log("Sound not found");
        }
        else
        {
            musicSource.clip = s.clip;
            musicSource.Play();
        }
    }

    public void PlaySFX(string clipName)
    {
        if (!sfxSource.enabled)
        {
            Debug.LogWarning("SFX AudioSource is disabled and cannot play sounds.");
            return;
        }

        Sound s = Array.Find(sfxSounds, x => x.name == clipName);

        if (s == null)
        {
            Debug.Log("Sound not found");
        }
        else
        {
            sfxSource.PlayOneShot(s.clip);

        }
    }

    public void StopSFX(string clipName)
    {
        Sound s = Array.Find(sfxSounds, x => x.name == clipName);

        if (s == null)
        {
            Debug.Log("Sound not found");
        }
        else
        {
            sfxSource.Stop();

        }
    }

    public void MusicVolume(float volume)
    {
        musicSource.volume = Mathf.Clamp01(volume);
    }

    public void SFXVolume(float volume)
    {
        sfxSource.volume = Mathf.Clamp01(volume);
    }
}



//// AudioManager.cs
//using UnityEngine;
//using UnityEngine.UI;
//using System;

//public class AudioManager : MonoBehaviour
//{
//    public static AudioManager Instance;
//    public Sound[] musicSounds, sfxSounds;
//    public AudioSource musicSource, sfxSource;

//    private void Awake()
//    {
//        if (Instance == null)
//        {
//            Instance = this;
//            DontDestroyOnLoad(gameObject);
//            InitializeAudioSources();
//        }
//        else
//        {
//            Destroy(gameObject);
//        }
//    }

//    private void InitializeAudioSources()
//    {
//        // Ensure audio sources exist
//        if (musicSource == null)
//            musicSource = gameObject.AddComponent<AudioSource>();
//        if (sfxSource == null)
//            sfxSource = gameObject.AddComponent<AudioSource>();

//        // Configure sources
//        musicSource.playOnAwake = false;
//        sfxSource.playOnAwake = false;
//    }

//    public void PlayMusic(string clipName)
//    {
//        Sound s = Array.Find(musicSounds, x => x.name == clipName);
//        if (s == null)
//        {
//            Debug.LogWarning($"Music '{clipName}' not found!");
//            return;
//        }

//        musicSource.clip = s.clip;
//        musicSource.volume = s.volume;
//        musicSource.pitch = s.pitch;
//        musicSource.Play();
//    }

//    public void PlaySFX(string clipName)
//    {
//        Sound s = Array.Find(sfxSounds, x => x.name == clipName);
//        if (s == null)
//        {
//            Debug.LogWarning($"SFX '{clipName}' not found!");
//            return;
//        }

//        sfxSource.volume = s.volume;
//        sfxSource.pitch = s.pitch;
//        sfxSource.PlayOneShot(s.clip);
//    }

//    public void StopMusic()
//    {
//        musicSource.Stop();
//    }

//    public void StopSFX()
//    {
//        sfxSource.Stop();
//    }

//    public void MusicVolume(float volume)
//    {
//        musicSource.volume = Mathf.Clamp01(volume);
//    }

//    public void SFXVolume(float volume)
//    {
//        sfxSource.volume = Mathf.Clamp01(volume);
//    }
//}