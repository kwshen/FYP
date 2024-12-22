using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HeartrateGameController : MonoBehaviour
{
    private WinCollider winColliderScript;
    public WinLoseUIManager winLoseUIManagerScript;
    private PlayerController playerControllerScript;
    private PauseMenu pauseMenuScript;
    private PaddleManager paddleManagerScript;
    [SerializeField]
    private string level1Scene;
    [SerializeField]
    private string level2Scene;
    public string levelBGM;
    private bool isTransitioning = false;

    void Start()
    {
        InitializeReferences();
        SetupLevel();
    }

    void InitializeReferences()
    {
        try
        {
            if (winLoseUIManagerScript == null)
            {
                GameObject uiManager = GameObject.Find("WinLoseUIManager");
                if (uiManager != null)
                    winLoseUIManagerScript = uiManager.GetComponent<WinLoseUIManager>();
            }

            GameObject winColliderObj = GameObject.Find("WinCollider");
            if (winColliderObj != null)
                winColliderScript = winColliderObj.GetComponent<WinCollider>();

            GameObject kayakObj = GameObject.Find("Kayak");
            if (kayakObj != null)
                playerControllerScript = kayakObj.GetComponent<PlayerController>();

            GameObject leftControllerObj = GameObject.Find("Left Controller");
            if (leftControllerObj != null)
                pauseMenuScript = leftControllerObj.GetComponent<PauseMenu>();

            GameObject paddleObj = GameObject.Find("Paddle");
            if (paddleObj != null)
                paddleManagerScript = paddleObj.GetComponent<PaddleManager>();
        }
        catch (System.Exception e)
        {
            Debug.LogError("Error initializing references: " + e.Message);
        }
    }

    void SetupLevel()
    {
        if (winLoseUIManagerScript != null)
        {
            winLoseUIManagerScript.ResetPanels();
            string currentScene = SceneManager.GetActiveScene().name;
            if (currentScene == level1Scene)
            {
                winLoseUIManagerScript.setLevelToLoad(level2Scene);
            }
            else
            {
                winLoseUIManagerScript.setLevelToLoad(level1Scene);
            }
        }

        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.PlayMusic(levelBGM);
        }

        isTransitioning = false;
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        InitializeReferences();
        SetupLevel();
    }

    void Update()
    {
        if (isTransitioning) return;

        // Only proceed if we're in a gameplay scene
        string currentScene = SceneManager.GetActiveScene().name;
        if (currentScene != level1Scene && currentScene != level2Scene)
        {
            return;
        }

        try
        {
            if (winColliderScript != null && winColliderScript.getIsPlayerWin())
            {
                if (currentScene == level1Scene)
                {
                    winLoseUIManagerScript?.setLevelToLoad(level2Scene);
                }
                else
                {
                    winLoseUIManagerScript?.setLevelToLoad(level1Scene);
                }
                winLoseUIManagerScript?.activePanel(true);
            }
            else if (playerControllerScript != null && playerControllerScript.getIsPlayerDie())
            {
                winLoseUIManagerScript?.setLevelToLoad(currentScene);
                winLoseUIManagerScript?.activePanel(false);
            }
        }
        catch (System.Exception e)
        {
            Debug.LogError("Error in Update: " + e.Message);
        }
    }
}

//using UnityEngine;
//using UnityEngine.SceneManagement;

//public class HeartrateGameController : MonoBehaviour
//{
//    private WinCollider winColliderScript;
//    public WinLoseUIManager winLoseUIManagerScript;
//    private PlayerController playerControllerScript;
//    private PauseMenu pauseMenuScript;
//    private PaddleManager paddleManagerScript;

//    [SerializeField]
//    private string level1Scene = "Heartrate_Level_1";
//    [SerializeField]
//    private string level2Scene = "Heartrate_Level_2";
//    public string levelBGM;

//    private bool initialized = false;
//    private bool hasWon = false;

//    void Start()
//    {
//        InitializeComponents();
//    }

//    void OnEnable()
//    {
//        SceneManager.sceneLoaded += OnSceneLoaded;
//    }

//    void OnDisable()
//    {
//        SceneManager.sceneLoaded -= OnSceneLoaded;
//        CleanupReferences();
//    }

//    private void CleanupReferences()
//    {
//        winColliderScript = null;
//        playerControllerScript = null;
//        pauseMenuScript = null;
//        paddleManagerScript = null;
//        initialized = false;
//        hasWon = false;
//    }

//    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
//    {
//        InitializeComponents();
//    }

//    void InitializeComponents()
//    {
//        try
//        {
//            FindUIManager();
//            FindGameComponents();
//            SetupLevel();
//            initialized = true;
//            hasWon = false;
//        }
//        catch (System.Exception e)
//        {
//            Debug.LogError($"Error during initialization: {e.Message}");
//            initialized = false;
//        }
//    }

//    void FindUIManager()
//    {
//        if (winLoseUIManagerScript == null)
//        {
//            var uiManager = GameObject.Find("WinLoseUIManager");
//            if (uiManager != null)
//            {
//                winLoseUIManagerScript = uiManager.GetComponent<WinLoseUIManager>();
//            }
//        }
//    }

//    void FindGameComponents()
//    {
//        var winColliderObj = GameObject.Find("WinCollider");
//        if (winColliderObj != null)
//        {
//            winColliderScript = winColliderObj.GetComponent<WinCollider>();
//        }

//        var kayakObj = GameObject.Find("Kayak");
//        if (kayakObj != null)
//        {
//            playerControllerScript = kayakObj.GetComponent<PlayerController>();
//        }

//        var leftControllerObj = GameObject.Find("Left Controller");
//        if (leftControllerObj != null)
//        {
//            pauseMenuScript = leftControllerObj.GetComponent<PauseMenu>();
//        }

//        var paddleObj = GameObject.Find("Paddle");
//        if (paddleObj != null)
//        {
//            paddleManagerScript = paddleObj.GetComponent<PaddleManager>();
//        }
//    }

//    void SetupLevel()
//    {
//        if (winLoseUIManagerScript != null)
//        {
//            winLoseUIManagerScript.ResetPanels();
//            string currentScene = SceneManager.GetActiveScene().name;
//            winLoseUIManagerScript.setLevelToLoad(currentScene == level1Scene ? level2Scene : level1Scene);
//        }

//        if (AudioManager.Instance != null)
//        {
//            AudioManager.Instance.PlayMusic(levelBGM);
//        }
//    }

//    void Update()
//    {
//        if (!initialized || hasWon) return;

//        try
//        {
//            string currentScene = SceneManager.GetActiveScene().name;

//            // Skip if not in a gameplay scene
//            if (currentScene != level1Scene && currentScene != level2Scene)
//                return;

//            if (winColliderScript != null && winColliderScript.getIsPlayerWin() && !hasWon)
//            {
//                hasWon = true;
//                string nextLevel = (currentScene == level1Scene) ? level2Scene : level1Scene;
//                if (winLoseUIManagerScript != null)
//                {
//                    winLoseUIManagerScript.setLevelToLoad(nextLevel);
//                    winLoseUIManagerScript.activePanel(true);
//                }
//            }
//            else if (playerControllerScript != null && playerControllerScript.getIsPlayerDie())
//            {
//                if (winLoseUIManagerScript != null)
//                {
//                    winLoseUIManagerScript.setLevelToLoad(currentScene);
//                    winLoseUIManagerScript.activePanel(false);
//                }
//            }
//        }
//        catch (System.Exception e)
//        {
//            Debug.LogError($"Error in Update: {e.Message}");
//        }
//    }
//}