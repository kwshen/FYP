using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public WinCollider winColliderScript;
    public WinLoseUIManager winLoseUIManagerScript;
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
        winLoseUIManagerScript.winPanel.SetActive(false);
        winLoseUIManagerScript.losePanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(winColliderScript != null && winColliderScript.getIsPlayerWin() == true)     //player win, nextlvl button
        {
            winLoseUIManagerScript.setLevelToLoad(level2Scene);
            winLoseUIManagerScript.activePanel(true);
        }       
        else if(playerManagerScript.getIsPlayerDie() == true)                           //player die, restart button
        {
            winLoseUIManagerScript.setLevelToLoad(level1Scene);
            winLoseUIManagerScript.activePanel(false);
        }
    }
}
