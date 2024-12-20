using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartrateGameController : MonoBehaviour
{
    public WinCollider winColliderScript;
    public WinLoseUIManager winLoseUIManagerScript;
    public PlayerController playerControllerScript;

    public PauseMenu pauseMenuScript;
    public PaddleManager paddleManagerScript;

    [SerializeField]
    private string level1Scene;
    [SerializeField]
    private string level2Scene;

    public string levelBGM;

    // Start is called before the first frame update
    void Start()
    {
        winLoseUIManagerScript.winPanel.SetActive(false);
        winLoseUIManagerScript.losePanel.SetActive(false);
        AudioManager.Instance.PlayMusic(levelBGM);
    }

    // Update is called once per frame
    void Update()
    {
        if(winColliderScript.getIsPlayerWin() == true || playerControllerScript.getIsPlayerDie() == true)
        {
            pauseMenuScript.enabled = false;
            paddleManagerScript.setEnableOrNot(false);
        }

        if(winColliderScript != null && winColliderScript.getIsPlayerWin() == true)     //player win, nextlvl button
        {
            winLoseUIManagerScript.setLevelToLoad(level2Scene);
            winLoseUIManagerScript.activePanel(true);
        }       
        else if(playerControllerScript.getIsPlayerDie() == true)                           //player die, restart button
        {
            winLoseUIManagerScript.setLevelToLoad(level1Scene);
            winLoseUIManagerScript.activePanel(false);
        }
    }
}
