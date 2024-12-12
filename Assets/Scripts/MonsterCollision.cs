using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterCollision : MonoBehaviour
{
    string appearTag = "AppearArea";
    string attackTag = "AttackArea";
    string playerTag = "Player";
    bool appear = false;
    bool attack = false;
    bool onWater = false;
    bool attackSuccess = false;

    private void OnTriggerEnter(Collider other)
    {Debug.Log("1" + attackSuccess);
        if (!appear && gameObject.CompareTag(appearTag) && other.CompareTag(playerTag) && !onWater)
        {
            appear = true;
        }
        else if(!attackSuccess && gameObject.CompareTag(attackTag) && other.CompareTag(playerTag))
        {
            attack = true;
        }
        
        if (onWater)
        {
            appear = false;
        }

        if(attackSuccess)
        {
            
            attack = false;
            
        }Debug.Log("2" + attackSuccess);
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

    public bool getAttack()
    {
        return attack;
    }

    public void setAttackSuccess(bool attackSuccess)
    {
        this.attackSuccess = attackSuccess;
    }
}
