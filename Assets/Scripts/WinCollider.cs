//using System.Collections;
//using System.Collections.Generic;
//using Unity.VisualScripting;
//using UnityEngine;

//public class WinCollider : MonoBehaviour
//{
//    private string playerTag = "Player";
//    private bool isPlayerWin = false;

//    private void Start()
//    {
//        isPlayerWin = false;
//        Debug.Log("WinCollider Start - Reset win state");
//    }

//    private void OnTriggerEnter(Collider other)
//    {
//        Debug.Log("Collision detected with: " + other.gameObject.name);

//        // Check if this collider is attached to the player object (either directly or as a child)
//        Transform currentTransform = other.transform;
//        while (currentTransform != null)
//        {
//            if (currentTransform.CompareTag(playerTag))
//            {
//                isPlayerWin = true;
//                Debug.Log("Player win condition met through object: " + other.gameObject.name);
//                return;
//            }
//            currentTransform = currentTransform.parent;
//        }
//    }

//    private void OnTriggerExit(Collider other)
//    {
//        // Check if the exiting object is part of the player
//        Transform currentTransform = other.transform;
//        while (currentTransform != null)
//        {
//            if (currentTransform.CompareTag(playerTag))
//            {
//                isPlayerWin = false;
//                Debug.Log("Player exit detected through object: " + other.gameObject.name);
//                return;
//            }
//            currentTransform = currentTransform.parent;
//        }
//    }

//    public bool getIsPlayerWin()
//    {
//        return isPlayerWin;
//    }
//}

// WinCollider.cs
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WinCollider : MonoBehaviour
{
    private string playerTag = "Player";
    private bool isPlayerWin = false;

    private void Start()
    {
        isPlayerWin = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        try
        {
            Transform currentTransform = other.transform;
            while (currentTransform != null)
            {
                if (currentTransform.CompareTag(playerTag))
                {
                    isPlayerWin = true;
                    Debug.Log("Player win condition met");
                    return;
                }
                currentTransform = currentTransform.parent;
            }
        }
        catch (System.Exception e)
        {
            Debug.LogError("Error in OnTriggerEnter: " + e.Message);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        try
        {
            Transform currentTransform = other.transform;
            while (currentTransform != null)
            {
                if (currentTransform.CompareTag(playerTag))
                {
                    isPlayerWin = false;
                    return;
                }
                currentTransform = currentTransform.parent;
            }
        }
        catch (System.Exception e)
        {
            Debug.LogError("Error in OnTriggerExit: " + e.Message);
        }
    }

    public bool getIsPlayerWin()
    {
        return isPlayerWin;
    }
}