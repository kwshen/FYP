using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;

public class WinLoseUIManager : MonoBehaviour
{
    public GameObject winPanel;
    public GameObject losePanel;
    public GameObject leftRayInteractor;
    public GameObject rightRayInteractor;
    public SceneTransitionManager transitionManager;
    private string mainMenuScene = "MainMenuScene";
    private string levelToLoad;

    public void activePanel(bool isPlayerWin)
    {
        leftRayInteractor.SetActive(true);
        rightRayInteractor.SetActive(true);
        if (isPlayerWin == true)
        {
            winPanel.SetActive(true);
        }
        else
        {
            losePanel.SetActive(true);
        }
    }

    public void nextOrRRestartLevel()
    {
        transitionManager.GoToSceneAsync(levelToLoad);
    }

    public void MainMenu()
    {
        Time.timeScale = 1;
        transitionManager.GoToSceneAsync(mainMenuScene);
    }

    public void setLevelToLoad(string levelToLoad)
    {
        this.levelToLoad = levelToLoad;
    }
}
