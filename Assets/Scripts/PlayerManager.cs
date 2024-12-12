using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private bool isPlayerDie = false;

    public void setIsPlayerDie(bool isPlayerDie)
    {
        this.isPlayerDie = isPlayerDie;
    }

    public bool getIsPlayerDie()
    {
        return isPlayerDie;
    }
}
