using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject settingMenu;
    public GameObject leftRayInteractor;
    public GameObject rightRayInteractor;
    public SceneTransitionManager transitionManager;

    private string mainMenuScene = "MainMenuScene";

    public bool activePauseMenu = true;

    // Start is called before the first frame update
    void Start()
    {
        DisplayPauseMenu();
    }

    public void PauseButtonPressed(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            DisplayPauseMenu();
        }
    }

    public void DisplayPauseMenu()
    {
        if(activePauseMenu == true)
        {
            leftRayInteractor.SetActive(false);
            rightRayInteractor.SetActive(false);
            pauseMenu.SetActive(false);
            activePauseMenu = false;
            Time.timeScale = 1;
        }
        else if(activePauseMenu == false)
        {
            leftRayInteractor.SetActive(true);
            rightRayInteractor.SetActive(true);
            pauseMenu.SetActive(true);
            activePauseMenu = true;
            Time.timeScale = 0;
        }
    }

    public void ResumeGame()
    {
        if (activePauseMenu == true)
        {
            pauseMenu.SetActive(false);
            activePauseMenu = false;
            Time.timeScale = 1;
        }
    }
    
    public void MainMenu()
    {
        Time.timeScale = 1;
        transitionManager.GoToSceneAsync(mainMenuScene);
    }

    public void SettingMenu()
    {
        pauseMenu.SetActive(false);
        settingMenu.SetActive(true);
    }

    public void Back()
    {
        pauseMenu.SetActive(true) ;
        settingMenu.SetActive(false) ;
    }
}
