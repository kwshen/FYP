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

//using UnityEngine;

//public class WinCollider : MonoBehaviour
//{
//    private string playerTag = "Player";
//    private bool isPlayerWin = false;

//    void Start()
//    {
//        ResetState();
//    }

//    void OnEnable()
//    {
//        ResetState();
//    }

//    private void ResetState()
//    {
//        isPlayerWin = false;
//    }

//    private void OnTriggerEnter(Collider other)
//    {
//        try
//        {
//            Transform currentTransform = other.transform;
//            while (currentTransform != null)
//            {
//                if (currentTransform.CompareTag(playerTag))
//                {
//                    isPlayerWin = true;
//                    Debug.Log($"Win triggered by: {currentTransform.name}");
//                    return;
//                }
//                currentTransform = currentTransform.parent;
//            }
//        }
//        catch (System.Exception e)
//        {
//            Debug.LogError($"Error in trigger enter: {e.Message}");
//        }
//    }

//    private void OnTriggerExit(Collider other)
//    {
//        try
//        {
//            Transform currentTransform = other.transform;
//            while (currentTransform != null)
//            {
//                if (currentTransform.CompareTag(playerTag))
//                {
//                    isPlayerWin = false;
//                    return;
//                }
//                currentTransform = currentTransform.parent;
//            }
//        }
//        catch (System.Exception e)
//        {
//            Debug.LogError($"Error in trigger exit: {e.Message}");
//        }
//    }

//    public bool getIsPlayerWin()
//    {
//        return isPlayerWin;
//    }
//}