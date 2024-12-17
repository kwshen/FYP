using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class SceneTransitionManager : MonoBehaviour
{
    public FadeScreen fadeScreen;
    [SerializeField] private GameObject progressPanel;
    [SerializeField] private Slider progressBar;
    [SerializeField] private TextMeshProUGUI progressValue;


    //public void GoToScene(string sceneName)
    //{
    //    StartCoroutine(GoToSceneRoutine(sceneName));
    //}

    //IEnumerator GoToSceneRoutine(string sceneName)
    //{
    //    fadeScreen.FadeOut();
    //    yield return new WaitForSeconds(fadeScreen.fadeDuration);

    //    //launch the new scene
    //}

    private void Awake()
    {
        progressPanel.SetActive(false);
    }

    public void GoToSceneAsync(string sceneName)
    {
        Debug.Log("load scene in gotosceneasync");
        StartCoroutine(GoToSceneAsyncRoutine(sceneName));
    }

    IEnumerator GoToSceneAsyncRoutine(string sceneName)
    {
        
        fadeScreen.FadeOut();
        yield return new WaitForSeconds(fadeScreen.fadeDuration);
        progressPanel.SetActive(true);
        //launch the new scene
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
        operation.allowSceneActivation = false;

        float timer = 0;
        while(timer <= fadeScreen.fadeDuration && !operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress);

            progressBar.value = progress;
            progressValue.text = (progress * 100).ToString("F0") + "%";

            timer += Time.deltaTime;
            yield return null;
        }

        operation.allowSceneActivation = true;
    }
}
