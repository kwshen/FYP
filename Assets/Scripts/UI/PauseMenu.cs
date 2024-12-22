using UnityEngine;
using UnityEngine.InputSystem;
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
        if(transitionManager == null)
            transitionManager = GameObject.Find("TransitionManager").GetComponent<SceneTransitionManager>();
        DisplayPauseMenu();
        musicSlider.value = AudioManager.Instance.musicSource.volume;
        sfxSlider.value = AudioManager.Instance.sfxSource.volume;
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
            //paddleManagerScript.setEnableOrNot(true);
            leftRayInteractor.SetActive(false);
            rightRayInteractor.SetActive(false);
            pauseMenu.SetActive(false);
            activePauseMenu = false;
            settingMenu.active = false;
            Time.timeScale = 1;
        }
        else if(activePauseMenu == false)
        {
            //paddleManagerScript.setEnableOrNot(false);
            leftRayInteractor.SetActive(true);
            rightRayInteractor.SetActive(true);
            pauseMenu.SetActive(true);
            activePauseMenu = true;
            Time.timeScale = 0;
        }
    }

    public void ResumeGame()
    {
        AudioManager.Instance.PlaySFX("ButtonClick");
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
        AudioManager.Instance.PlaySFX("ButtonClick");
        Time.timeScale = 1;
        transitionManager.GoToSceneAsync(mainMenuScene);
    }

    public void SettingMenu()
    {
        AudioManager.Instance.PlaySFX("ButtonClick");
        pauseMenu.SetActive(false);
        settingMenu.SetActive(true);
    }

    public void Back()
    {
        AudioManager.Instance.PlaySFX("ButtonClick");
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

    public bool getActivePauseMenu()
    {
        return activePauseMenu;
    }
}
