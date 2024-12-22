using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinLoseUIManager : MonoBehaviour
{
    public GameObject winPanel;
    public GameObject losePanel;
    public GameObject leftRayInteractor;
    public GameObject rightRayInteractor;
    private SceneTransitionManager transitionManager;
    private string mainMenuScene = "MainMenuScene";
    private string levelToLoad;
    private bool isPanelActive = false;

    private void Start()
    {
        FindReferences();
        ResetPanels();
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        FindReferences();
        ResetPanels();
    }

    private void FindReferences()
    {
        try
        {
            if (winPanel == null)
                winPanel = GameObject.Find("WinPanel");
            if (losePanel == null)
                losePanel = GameObject.Find("LosePanel");
            if (leftRayInteractor == null)
                leftRayInteractor = GameObject.Find("LeftRayInteractor");
            if (rightRayInteractor == null)
                rightRayInteractor = GameObject.Find("RightRayInteractor");
            if (transitionManager == null)
            {
                GameObject transObj = GameObject.Find("TransitionManager");
                if (transObj != null)
                    transitionManager = transObj.GetComponent<SceneTransitionManager>();
            }
        }
        catch (System.Exception e)
        {
            Debug.LogError("Error finding references: " + e.Message);
        }
    }

    public void activePanel(bool isPlayerWin)
    {
        if (isPanelActive) return;

        FindReferences();

        try
        {
            if (leftRayInteractor != null)
                leftRayInteractor.SetActive(true);
            if (rightRayInteractor != null)
                rightRayInteractor.SetActive(true);

            if (isPlayerWin)
            {
                if (winPanel != null)
                    winPanel.SetActive(true);
                if (losePanel != null)
                    losePanel.SetActive(false);
            }
            else
            {
                if (losePanel != null)
                    losePanel.SetActive(true);
                if (winPanel != null)
                    winPanel.SetActive(false);
            }

            isPanelActive = true;
        }
        catch (System.Exception e)
        {
            Debug.LogError("Error activating panel: " + e.Message);
        }
    }

    public void nextOrRRestartLevel()
    {
        AudioManager.Instance.PlaySFX("ButtonClick");
        Time.timeScale = 1;
        ResetPanels();
        if (transitionManager != null && !string.IsNullOrEmpty(levelToLoad))
        {
            transitionManager.GoToSceneAsync(levelToLoad);
        }
    }

    public void MainMenu()
    {
        AudioManager.Instance.PlaySFX("ButtonClick");
        Time.timeScale = 1;
        ResetPanels();
        if (transitionManager != null)
        {
            transitionManager.GoToSceneAsync(mainMenuScene);
        }
    }

    public void setLevelToLoad(string levelToLoad)
    {
        this.levelToLoad = levelToLoad;
    }

    public void ResetPanels()
    {
        try
        {
            isPanelActive = false;

            if (winPanel != null)
                winPanel.SetActive(false);
            if (losePanel != null)
                losePanel.SetActive(false);
            if (leftRayInteractor != null)
                leftRayInteractor.SetActive(false);
            if (rightRayInteractor != null)
                rightRayInteractor.SetActive(false);
        }
        catch (System.Exception e)
        {
            Debug.LogError("Error resetting panels: " + e.Message);
        }
    }
}

//using UnityEngine;
//using UnityEngine.SceneManagement;

//public class WinLoseUIManager : MonoBehaviour
//{
//    public GameObject winPanel;
//    public GameObject losePanel;
//    public GameObject leftRayInteractor;
//    public GameObject rightRayInteractor;
//    private SceneTransitionManager transitionManager;
//    private string mainMenuScene = "MainMenuScene";
//    private string levelToLoad;
//    private bool isPanelActive = false;

//    void Start()
//    {
//        FindReferences();
//        ResetPanels();
//    }

//    void OnEnable()
//    {
//        SceneManager.sceneLoaded += OnSceneLoaded;
//    }

//    void OnDisable()
//    {
//        SceneManager.sceneLoaded -= OnSceneLoaded;
//    }

//    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
//    {
//        FindReferences();
//        ResetPanels();
//        isPanelActive = false;
//    }

//    void FindReferences()
//    {
//        try
//        {
//            if (winPanel == null)
//                winPanel = GameObject.Find("WinPanel");
//            if (losePanel == null)
//                losePanel = GameObject.Find("LosePanel");
//            if (leftRayInteractor == null)
//                leftRayInteractor = GameObject.Find("LeftRayInteractor");
//            if (rightRayInteractor == null)
//                rightRayInteractor = GameObject.Find("RightRayInteractor");
//            if (transitionManager == null)
//            {
//                var transObj = GameObject.Find("TransitionManager");
//                if (transObj != null)
//                    transitionManager = transObj.GetComponent<SceneTransitionManager>();
//            }
//        }
//        catch (System.Exception e)
//        {
//            Debug.LogError($"Error finding references: {e.Message}");
//        }
//    }

//    public void activePanel(bool isPlayerWin)
//    {
//        if (isPanelActive) return;

//        FindReferences();

//        try
//        {
//            if (leftRayInteractor != null)
//                leftRayInteractor.SetActive(true);
//            if (rightRayInteractor != null)
//                rightRayInteractor.SetActive(true);

//            if (isPlayerWin)
//            {
//                if (winPanel != null)
//                    winPanel.SetActive(true);
//                if (losePanel != null)
//                    losePanel.SetActive(false);
//            }
//            else
//            {
//                if (losePanel != null)
//                    losePanel.SetActive(true);
//                if (winPanel != null)
//                    winPanel.SetActive(false);
//            }

//            isPanelActive = true;
//        }
//        catch (System.Exception e)
//        {
//            Debug.LogError($"Error activating panel: {e.Message}");
//        }
//    }

//    public void nextOrRRestartLevel()
//    {
//        Time.timeScale = 1;
//        isPanelActive = false;
//        ResetPanels();

//        if (transitionManager != null && !string.IsNullOrEmpty(levelToLoad))
//        {
//            transitionManager.GoToSceneAsync(levelToLoad);
//        }
//    }

//    public void MainMenu()
//    {
//        Time.timeScale = 1;
//        isPanelActive = false;
//        ResetPanels();

//        if (transitionManager != null)
//        {
//            transitionManager.GoToSceneAsync(mainMenuScene);
//        }
//    }

//    public void setLevelToLoad(string levelToLoad)
//    {
//        this.levelToLoad = levelToLoad;
//    }

//    public void ResetPanels()
//    {
//        try
//        {
//            if (winPanel != null)
//                winPanel.SetActive(false);
//            if (losePanel != null)
//                losePanel.SetActive(false);
//            if (leftRayInteractor != null)
//                leftRayInteractor.SetActive(false);
//            if (rightRayInteractor != null)
//                rightRayInteractor.SetActive(false);
//        }
//        catch (System.Exception e)
//        {
//            Debug.LogError($"Error resetting panels: {e.Message}");
//        }
//    }
//}