using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppearAreaCollision : MonoBehaviour
{
    string playerTag = "Player";
    bool appear = false;
    bool onWater = false;

    private void OnTriggerEnter(Collider other)
    {
        if (appear == false && other.CompareTag(playerTag) && onWater == false)
        {
            appear = true;
        }
    }

    public bool getAppear()
    {
        return appear;
    }

    public void setAppear(bool isAppear)
    {
        appear = isAppear;
    }

    public bool getOnWater()
    {
        return onWater;
    }

    public void setOnWater(bool onWater)
    {
        this.onWater = onWater;
    }

}
