using UnityEngine;

public class AppearAreaCollision : MonoBehaviour
{
    string playerTag = "Player";
    bool appear = false;
    bool onWater = false;

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("appear" + true);
        if (appear == false && other.CompareTag(playerTag) && onWater == false)
        {
            Debug.Log("onwater" + onWater);
            appear = true;
            Debug.Log("appear" + true);
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
