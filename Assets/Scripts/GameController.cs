using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public WinCollider winColliderScript;
    public GameObject winPanel;
    public GameObject losePanel;
    public PlayerManager playerManagerScript;

    // Start is called before the first frame update
    void Start()
    {
        winPanel.SetActive(false);
        losePanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(winColliderScript != null && winColliderScript.getIsPlayerWin() == true)
        {
            losePanel.SetActive(false);
            winPanel.SetActive(true);
        }       
        else if(playerManagerScript.getIsPlayerDie() == true)
        {
            winPanel.SetActive(false);
            losePanel.SetActive(true);
        }
    }
}
