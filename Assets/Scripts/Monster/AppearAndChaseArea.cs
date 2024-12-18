using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppearAndChase : MonoBehaviour
{
    private bool appearAndChase = false;
    string playerTag = "Player";

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            appearAndChase = true;
        }
    }

    public bool getAppearAndChase()
    {
        return appearAndChase;
    }
}
