using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Video;

namespace ProjektSumperk
{
    [System.Serializable]
    public class PersonData
    {
        public string name;
        public int age;
        public string city;
    }

    public class LoadFromResourceExampleUsage : MonoBehaviour
    {
        public Image image;
        public AudioSource audioSource;
        public TMP_Text resultText;

        public void LoadSprite()
        {
            // Load an image (Sprite) named "paint2" from the "Images" subfolder (if any).
            Sprite imageSprite = LoadFromResource.Load<Sprite>("paint2", "Images");

            //Sprite imageSprite = LoadFromResource.Load<Sprite>("paint");

            // Assign the loaded image to an Image component in Unity.
            if (imageSprite != null)
            {
                image.sprite = imageSprite;
            }

        }

        public void LoadAudio()
        {
            // Load an audio clip named "gameover" from the "Audio" subfolder (if any).
            AudioClip audioClip = LoadFromResource.Load<AudioClip>("gameover");

            //AudioClip audioClip = LoadFromResource.Load<AudioClip>("gameover", "Audios");

            // Play the loaded audio clip using an AudioSource component.
            if (audioClip != null)
            {
                audioSource.clip = audioClip;
                audioSource.Play();
            }
        }

        public void LoadPrefabs()
        {
            // Load the prefab named "ResourcesCube2" from the "Subfolder" folder
            GameObject loadedPrefab = LoadFromResource.Load<GameObject>("ResourcesCube2", "Subfolder");


            //GameObject loadedPrefab = LoadFromResource.Load<GameObject>("ResourcesCube");

            if (loadedPrefab != null)
            {
                // Instantiate the loaded prefab into the scene
                Instantiate(loadedPrefab);
            }
            else
            {
                Debug.LogError("Failed to load the prefab.");
            }
        }

        public void LoadJSON()
        {
            PersonData personData = LoadFromResource.LoadJSON<PersonData>("mydata");
            if (personData != null)
            {
                // Successfully loaded and deserialized the JSON data
                Debug.Log("Successfully loaded and deserialized the JSON data.");

                // Set the JSON text to the TMP_Text component for display
                resultText.text = personData.name + " / " + personData.age + " / " + personData.city;
            }
            else
            {
                // Handle the error, e.g., show an error message or use default values
                Debug.LogError("Failed to load and deserialize the JSON data.");
            }
        }

        public void LoadText()
        {
            // Load a text asset named "mytext" from the "TextFolder" subfolder (if any).
            //TextAsset textAsset = LoadFromResource.Load<TextAsset>("mytext", "TextFolder");

            TextAsset textAsset = LoadFromResource.Load<TextAsset>("mytext");


            // Access and use the loaded text asset's content.
            if (textAsset != null)
            {
                resultText.text = textAsset.text;
                // Use the text content as needed.
            }
        }

        public void LoadVideo()
        {
            // Load a video clip named "myVideo" from the "Videos" subfolder (if any).
            VideoClip videoClip = LoadFromResource.Load<VideoClip>("myVideo", "Videos");

            //VideoClip videoClip = LoadFromResource.Load<VideoClip>("myVideo");

            // Assign the loaded video clip to a Unity VideoPlayer component.
            if (videoClip != null)
            {
                //videoPlayer.clip = videoClip;
                //videoPlayer.Play();
            }

        }

        public void ClearChache()
        {
            // Clear the resource cache.
            LoadFromResource.ClearCache();
        }
    }
}