//using System.Collections;
//using System.Collections.Generic;
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
//    private string level1Scene;
//    [SerializeField]
//    private string level2Scene;
//    public string levelBGM;

//    void Start()
//    {
//        InitializeReferences();
//        SetupLevel();
//    }

//    void InitializeReferences()
//    {
//        if (winLoseUIManagerScript == null)
//        {
//            winLoseUIManagerScript = GameObject.Find("WinLoseUIManager").GetComponent<WinLoseUIManager>();
//        }
//        winColliderScript = GameObject.Find("WinCollider").GetComponent<WinCollider>();
//        playerControllerScript = GameObject.Find("Kayak").GetComponent<PlayerController>();
//        pauseMenuScript = GameObject.Find("Left Controller").GetComponent<PauseMenu>();
//        paddleManagerScript = GameObject.Find("Paddle").GetComponent<PaddleManager>();
//    }

//    void SetupLevel()
//    {
//        if (winLoseUIManagerScript != null)
//        {
//            winLoseUIManagerScript.ResetPanels();

//            // Set the correct next level based on current scene
//            string currentScene = SceneManager.GetActiveScene().name;
//            if (currentScene == level1Scene)
//            {
//                winLoseUIManagerScript.setLevelToLoad(level2Scene);
//            }
//            else
//            {
//                winLoseUIManagerScript.setLevelToLoad(level1Scene);
//            }
//        }

//        AudioManager.Instance.PlayMusic(levelBGM);
//    }

//    void Update()
//    {
//        // Check for null references and try to recover
//        if (winLoseUIManagerScript == null)
//        {
//            winLoseUIManagerScript = GameObject.Find("WinLoseUIManager").GetComponent<WinLoseUIManager>();
//            return;
//        }

//        if (winColliderScript == null)
//        {
//            winColliderScript = GameObject.Find("WinCollider").GetComponent<WinCollider>();
//            return;
//        }

//        if (playerControllerScript == null)
//        {
//            playerControllerScript = GameObject.Find("Kayak").GetComponent<PlayerController>();
//            return;
//        }

//        // Handle win condition
//        if (winColliderScript.getIsPlayerWin())
//        {
//            string currentScene = SceneManager.GetActiveScene().name;
//            if (currentScene == level1Scene)
//            {
//                winLoseUIManagerScript.setLevelToLoad(level2Scene);
//            }
//            else
//            {
//                winLoseUIManagerScript.setLevelToLoad(level1Scene);
//            }
//            winLoseUIManagerScript.activePanel(true);
//        }
//        // Handle lose condition
//        else if (playerControllerScript.getIsPlayerDie())
//        {
//            string currentScene = SceneManager.GetActiveScene().name;
//            winLoseUIManagerScript.setLevelToLoad(currentScene);
//            winLoseUIManagerScript.activePanel(false);
//        }
//    }
//}

// HeartrateGameController.cs
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