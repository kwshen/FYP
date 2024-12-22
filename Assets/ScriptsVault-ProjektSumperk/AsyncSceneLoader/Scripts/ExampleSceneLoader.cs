using UnityEngine;

namespace ProjektSumperk
{
    public class ExampleSceneLoader : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            // Load scene "Object Pool Pattern" asynchronously with loading screen and allow scene activation
            AsyncSceneLoader.Instance.OnSceneLoaded += OnSceneLoaded;
            AsyncSceneLoader.Instance.LoadSceneAsync("Object Pool Pattern", showLoadingScreen: true, allowSceneActivation: true);
        }

        // Callback method for when scene is loaded
        void OnSceneLoaded(string sceneName)
        {
            Debug.Log("Scene " + sceneName + " has finished loading!");
            // Do any post-loading logic here
        }

        // Update is called once per frame
        void Update()
        {
            // Check loading progress of "Object Pool Pattern" scene
            float progress = AsyncSceneLoader.Instance.GetSceneLoadingProgress("Object Pool Pattern");
            if (progress >= 0f)
            {
                Debug.Log("Loading progress of Object Pool Pattern: " + (progress * 100f) + "%");
            }
        }
    }
}