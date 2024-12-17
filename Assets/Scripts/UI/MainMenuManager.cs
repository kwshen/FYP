//using UnityEngine;
//using UnityEngine.SceneManagement;
//using UnityEngine.UI;

//public class MainMenuManager : MonoBehaviour
//{
//    public GameObject startPanel;
//    public GameObject gameModePanel;
//    public GameObject levelPanel;
//    public GameObject settingPanel;

//    private string selectedGameMode;
//    private string selectedLevel;

//public Button casualModeButton;
//    public Button heartRateModeButton;
//    public Button startButton;

//    private Button selectedButton;

//    void Start()
//    {
//        ShowPanel(startPanel);
//    }

//    public void ShowPanel(GameObject panelToShow)
//    {
//        // Hide all panels
//        startPanel.SetActive(false);
//        gameModePanel.SetActive(false);
//        levelPanel.SetActive(false);
//        settingPanel.SetActive(false);

//        // Show the panel
//        panelToShow.SetActive(true);
//        Debug.Log("showing " + panelToShow);
//    }

//    public void QuitGame()
//    {
//        Application.Quit();
//    }

//    public void SelectGameMode(string mode)
//    {
//        selectedGameMode = mode;
//        ShowPanel(levelPanel); // Move to level selection
//    }

//    public void SelectLevel(string level)
//    {
//        selectedLevel = level;

//        // Check the game mode and load the appropriate scene
//        if (selectedGameMode == "Casual")
//        {
//            //SceneManager.LoadScene("CasualScene"); // Replace with your scene name
//        }
//        else if (selectedGameMode == "Heartrate")
//        {
//            //SceneManager.LoadScene("HeartRateScene"); // Replace with your scene name
//        }
//    }
//}



//using UnityEngine;
//using UnityEngine.EventSystems;
//using UnityEngine.SceneManagement;
//using UnityEngine.UI;

//public class MainMenuManager : MonoBehaviour
//{
//    public GameObject startPanel;
//    public GameObject gameModePanel;
//    public GameObject levelPanel;
//    public GameObject settingPanel;

//    private string selectedGameMode;
//    private string selectedLevel;

//    public Button casualModeButton;
//    public Button heartRateModeButton;
//    public Button startButton;

//    private Button selectedGameModeButton;
//    private Button selectedLevelButton;

//    void Start()
//    {
//        ShowPanel(startPanel);

//        // Initialize game mode buttons
//        casualModeButton.onClick.AddListener(() => SelectGameModeButton(casualModeButton, "Casual"));
//        heartRateModeButton.onClick.AddListener(() => SelectGameModeButton(heartRateModeButton, "HeartRate"));

//        // Disable the Start button until both game mode and level are selected
//        startButton.interactable = false;
//    }

//    public void ShowPanel(GameObject panelToShow)
//    {
//        // Hide all panels
//        startPanel.SetActive(false);
//        gameModePanel.SetActive(false);
//        levelPanel.SetActive(false);
//        settingPanel.SetActive(false);

//        // Show the panel
//        panelToShow.SetActive(true);
//    }

//    public void QuitGame()
//    {
//        Application.Quit();
//    }

//    private void SelectGameModeButton(Button button, string mode)
//    {
//        // Reset appearance of all game mode buttons
//        ResetButtonAppearance(casualModeButton);
//        ResetButtonAppearance(heartRateModeButton);

//        // Highlight the selected button
//        HighlightButton(button);

//        selectedGameModeButton = button;
//        selectedGameMode = mode;

//        Debug.Log("Game mode selected: " + mode);
//        ValidateStartButton();
//    }

//    //public void SelectLevel(Button button, string level)
//    //{
//    //    // Reset appearance of all level buttons
//    //    ResetButtonAppearance(levelPanel.GetComponentsInChildren<Button>());

//    //    // Highlight the selected level button
//    //    HighlightButton(button);

//    //    selectedLevelButton = button;
//    //    selectedLevel = level;

//    //    Debug.Log("Level selected: " + level);
//    //    ValidateStartButton();
//    //}

//    public void SelectLevel(string level)
//    {
//        // Get the currently clicked button
//        Button clickedButton = EventSystem.current.currentSelectedGameObject.GetComponent<Button>();

//        // Reset the appearance of other level buttons
//        ResetButtonAppearance(levelPanel.GetComponentsInChildren<Button>());

//        // Highlight the clicked button
//        HighlightButton(clickedButton);

//        selectedLevelButton = clickedButton;
//        selectedLevel = level;

//        Debug.Log("Level selected: " + level);
//        ValidateStartButton();
//    }

//    private void ResetButtonAppearance(Button button)
//    {
//        ColorBlock cb = button.colors;//
//        cb.normalColor = Color.white; // Reset to original color
//        button.colors = cb;
//    }

//    private void ResetButtonAppearance(Button[] buttons)
//    {
//        foreach (Button button in buttons)
//        {
//            ResetButtonAppearance(button);
//        }
//    }

//    private void HighlightButton(Button button)
//    {
//        ColorBlock cb = button.colors;
//        cb.normalColor = new Color(0.7f, 0.7f, 0.7f, 1f); // Darker color for selected
//        button.colors = cb;
//    }

//    private void ValidateStartButton()
//    {
//        // Enable the Start button if both game mode and level are selected
//        startButton.interactable = (selectedGameModeButton != null && selectedLevelButton != null);
//    }
//}



//============================================================================================================================

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class MainMenuManager : MonoBehaviour
{
    public GameObject leftRayInteractor;
    public GameObject rightRayInteractor;

    public GameObject startPanel;
    public GameObject gameModePanel;
    public GameObject levelPanel;
    public GameObject settingPanel;

    public Button casualModeButton;
    public Button heartRateModeButton;
    public Button nextButtonGameMode; // Renamed from startButtonGameMode
    public Button startButtonLevelSelect; // "Start" button in Level Select panel

    private string selectedGameMode; // Tracks the selected game mode
    private string selectedLevel; // Tracks the selected level

    private Color originalButtonColor = new Color(0f, 0.78f, 1f); // 00C8FF in RGB
    private Color highlightedColor = new Color(0f, 0.6f, 0.8f); // Darker shade for highlight

    public SceneTransitionManager transitionManager;

    void Start()
    {
        // Initialize panels
        ShowPanel(startPanel);

        leftRayInteractor.SetActive(true);
        rightRayInteractor.SetActive(true);

        // Disable buttons initially
        nextButtonGameMode.interactable = false;
        startButtonLevelSelect.interactable = false;
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
                    sceneName = "Casual_Level1"; // Replace with actual scene name
                else if (selectedLevel == "Level2")
                    sceneName = "Casual_Level2"; // Replace with actual scene name
            }
            else if (selectedGameMode == "Heartrate")
            {
                if (selectedLevel == "Level1")
                    sceneName = "Heartrate_Level_1"; // Replace with actual scene name
                else if (selectedLevel == "Level2")
                    sceneName = "Heartrate_Level2"; // Replace with actual scene name
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
}

