using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WinCollider : MonoBehaviour
{
    private string playerTag = "Player";
    private bool isPlayerWin = false;

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            isPlayerWin = true;
        }
    }

    public bool getIsPlayerWin()
    {
        return isPlayerWin;
    }
}
