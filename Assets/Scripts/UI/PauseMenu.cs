using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject settingMenu;
    public GameObject leftRayInteractor;
    public GameObject rightRayInteractor;
    public SceneTransitionManager transitionManager;

    private string mainMenuScene = "MainMenuScene";

    public bool activePauseMenu = true;

    public Slider musicSlider;
    public Slider sfxSlider;

    public PaddleManager paddleManagerScript;
    void Start()
    {
        DisplayPauseMenu();
    }

    public void PauseButtonPressed(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            paddleManagerScript.setEnableOrNot(false);
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
            leftRayInteractor.SetActive(false);
            rightRayInteractor.SetActive(false);
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

    public void MusicVolume()
    {
        AudioManager.Instance.MusicVolume(musicSlider.value);
    }
    public void SFXVolume()
    {
        AudioManager.Instance.SFXVolume(sfxSlider.value);
    }
}
