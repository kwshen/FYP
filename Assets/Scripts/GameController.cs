using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public WinCollider winColliderScript;
    public WinLoseUIManager winLoseUIManager;
    public GameObject winPanel;
    public GameObject losePanel;
    public PlayerManager playerManagerScript;

    [SerializeField]
    private string level1Scene;
    [SerializeField]
    private string level2Scene;

    // Start is called before the first frame update
    void Start()
    {
        winLoseUIManager.winPanel.SetActive(false);
        winLoseUIManager.losePanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(winColliderScript != null && winColliderScript.getIsPlayerWin() == true)     //player win, nextlvl button
        {
            winLoseUIManager.setLevelToLoad(level2Scene);
            winLoseUIManager.losePanel.SetActive(false);
            winLoseUIManager.winPanel.SetActive(true);
        }       
        else if(playerManagerScript.getIsPlayerDie() == true)                           //player die, restart button
        {
            winLoseUIManager.setLevelToLoad(level1Scene);
            winLoseUIManager.winPanel.SetActive(false);
            winLoseUIManager.losePanel.SetActive(true);
        }
    }
}
