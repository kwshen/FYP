using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Collections;
using System.Collections.Generic;

namespace ProjektSumperk
{
    public class AsyncSceneLoader : MonoBehaviour
    {
        // Singleton instance for easy access
        private static AsyncSceneLoader instance;
        public static AsyncSceneLoader Instance
        {
            get
            {
                if (instance == null)
                {
                    GameObject loaderObj = new GameObject("AsyncSceneLoader");
                    instance = loaderObj.AddComponent<AsyncSceneLoader>();
                }
                return instance;
            }
        }

        // Dictionary to store active async operations
        private Dictionary<string, AsyncOperation> asyncOperations = new Dictionary<string, AsyncOperation>();

        // Events for scene loading progress and completion
        public event Action<string, float> OnSceneLoadingProgress;
        public event Action<string> OnSceneLoaded;

        /// <summary>
        /// Load a scene asynchronously.
        /// </summary>
        /// <param name="sceneName">Name of the scene to load.</param>
        /// <param name="showLoadingScreen">Whether to show loading screen while loading.</param>
        /// <param name="allowSceneActivation">Whether to allow scene activation after loading.</param>
        public void LoadSceneAsync(string sceneName, bool showLoadingScreen = true, bool allowSceneActivation = true)
        {
            StartCoroutine(LoadSceneAsyncCoroutine(sceneName, showLoadingScreen, allowSceneActivation));
        }

        private IEnumerator LoadSceneAsyncCoroutine(string sceneName, bool showLoadingScreen, bool allowSceneActivation)
        {
            AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);

            // Set allowSceneActivation flag
            operation.allowSceneActivation = allowSceneActivation;

            // Add to active async operations dictionary
            asyncOperations[sceneName] = operation;

            // Wait until the operation is done
            while (!operation.isDone)
            {
                // Dispatch progress event
                OnSceneLoadingProgress?.Invoke(sceneName, operation.progress);

                yield return null;
            }

            // Dispatch completion event
            OnSceneLoaded?.Invoke(sceneName);

            // Remove from active async operations dictionary
            asyncOperations.Remove(sceneName);
        }

        /// <summary>
        /// Check if a scene is currently being loaded asynchronously.
        /// </summary>
        /// <param name="sceneName">Name of the scene.</param>
        /// <returns>True if scene is being loaded asynchronously, false otherwise.</returns>
        public bool IsSceneLoadingAsync(string sceneName)
        {
            return asyncOperations.ContainsKey(sceneName);
        }

        /// <summary>
        /// Get the progress of scene loading.
        /// </summary>
        /// <param name="sceneName">Name of the scene.</param>
        /// <returns>Progress value (0 to 1) if scene is loading asynchronously, -1 otherwise.</returns>
        public float GetSceneLoadingProgress(string sceneName)
        {
            if (asyncOperations.ContainsKey(sceneName))
            {
                return asyncOperations[sceneName].progress;
            }
            else
            {
                return -1f;
            }
        }

        /// <summary>
        /// Allow scene activation after loading.
        /// </summary>
        /// <param name="sceneName">Name of the scene.</param>
        public void AllowSceneActivation(string sceneName)
        {
            if (asyncOperations.ContainsKey(sceneName))
            {
                asyncOperations[sceneName].allowSceneActivation = true;
            }
        }
    }
}