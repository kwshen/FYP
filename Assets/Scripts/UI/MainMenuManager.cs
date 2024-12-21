using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.InputSystem;

public class MainMenuManager : MonoBehaviour
{
    [Header("Interactor")]
    public GameObject leftRayInteractor;
    public GameObject rightRayInteractor;

    [Header("Menu Panel")]
    public GameObject startPanel;
    public GameObject gameModePanel;
    public GameObject levelPanel;
    public GameObject settingPanel;

    [Header("Button")]
    public Button casualModeButton;
    public Button heartRateModeButton;
    public Button nextButtonGameMode; // Renamed from startButtonGameMode
    public Button startButtonLevelSelect; // "Start" button in Level Select panel

    private string selectedGameMode; // Tracks the selected game mode
    private string selectedLevel; // Tracks the selected level

    private Color originalButtonColor = new Color(0f, 0.78f, 1f); // 00C8FF in RGB
    private Color highlightedColor = new Color(0f, 0.6f, 0.8f); // Darker shade for highlight

    public SceneTransitionManager transitionManager;

    public Slider musicSlider;
    public Slider sfxSlider;

    void Start()
    {
        AudioManager.Instance.PlayMusic("MainMenuBGM");

        // Initialize panels
        ShowPanel(startPanel);

        leftRayInteractor.SetActive(true);
        rightRayInteractor.SetActive(true);

        // Disable buttons initially
        nextButtonGameMode.interactable = false;
        startButtonLevelSelect.interactable = false;

        musicSlider.value = AudioManager.Instance.musicSource.volume;
        sfxSlider.value = AudioManager.Instance.sfxSource.volume;
    }

    public void ShowPanel(GameObject panelToShow)
    {
        // Hide all panels
        startPanel.SetActive(false);
        gameModePanel.SetActive(false);
        levelPanel.SetActive(false);
        settingPanel.SetActive(false);

        // Show the selected panel
        panelToShow.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void SelectGameMode(string mode)
    {
        // Update the selected game mode
        selectedGameMode = mode;

        // Reset all button colors
        ResetButtonColors();

        // Highlight the selected button
        if (mode == "Casual")
        {
            HighlightButton(casualModeButton);
        }
        else if (mode == "Heartrate")
        {
            HighlightButton(heartRateModeButton);
        }

        // Enable the "Next" button since a mode is selected
        nextButtonGameMode.interactable = true;
    }

    public void SelectLevel(string level)
    {
        // Update the selected level
        selectedLevel = level;

        // Reset all button colors in the level panel
        ResetButtonColorsInLevelPanel();

        // Highlight the selected level button
        Button selectedLevelButton = GameObject.Find(level + "Button").GetComponent<Button>();
        HighlightButton(selectedLevelButton);

        // Enable the "Start" button in Level Select panel
        startButtonLevelSelect.interactable = true;
    }

    public void ProceedToLevelSelection()
    {
        if (!string.IsNullOrEmpty(selectedGameMode))
        {
            ShowPanel(levelPanel); // Show level selection panel
        }
    }

    public void StartGame()
    {
        if (!string.IsNullOrEmpty(selectedGameMode) && !string.IsNullOrEmpty(selectedLevel))
        {
            string sceneName = "";

            // Determine which scene to load based on game mode and level
            if (selectedGameMode == "Casual")
            {
                if (selectedLevel == "Level1")
                    sceneName = "Casual_Level_1"; // Replace with actual scene name
                else if (selectedLevel == "Level2")
                    sceneName = "Casual_Level_2"; // Replace with actual scene name
            }
            else if (selectedGameMode == "Heartrate")
            {
                if (selectedLevel == "Level1")
                    sceneName = "Heartrate_Level_1"; // Replace with actual scene name
                else if (selectedLevel == "Level2")
                    sceneName = "Heartrate_Level_2"; // Replace with actual scene name
            }

            // Load the selected scene
            if (!string.IsNullOrEmpty(sceneName))
            {
                //StartCoroutine(LoadSceneAsync(sceneName));
                transitionManager.GoToSceneAsync(sceneName);
            }
        }
    }


    private void ResetButtonColors()
    {
        // Reset the colors of both buttons
        ResetButtonColor(casualModeButton);
        ResetButtonColor(heartRateModeButton);
    }

    private void ResetButtonColorsInLevelPanel()
    {
        // Reset the colors of all level buttons
        Button[] levelButtons = levelPanel.GetComponentsInChildren<Button>();
        foreach (Button button in levelButtons)
        {
            ResetButtonColor(button);
        }
    }

    private void ResetButtonColor(Button button)
    {
        ColorBlock colorBlock = button.colors;
        colorBlock.normalColor = originalButtonColor;
        button.colors = colorBlock;
    }

    private void HighlightButton(Button button)
    {
        ColorBlock colorBlock = button.colors;
        colorBlock.normalColor = highlightedColor;
        button.colors = colorBlock;
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

