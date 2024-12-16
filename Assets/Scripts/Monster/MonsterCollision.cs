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
    {
        if (appear == false && gameObject.CompareTag(appearTag) && other.CompareTag(playerTag) && onWater == false)
        {
            appear = true;
        }

        if(attackSuccess == false && gameObject.CompareTag(attackTag) && other.CompareTag(playerTag))
        {
            attack = true;
        }
        //if attack area no collide with player
        else if((gameObject.CompareTag(attackTag) && !other.CompareTag(playerTag)) || attackSuccess == true)
        {
            attack = false;
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

    public bool getAttack()
    {
        return attack;
    }

    public void setAttackSuccess(bool attackSuccess)
    {
        this.attackSuccess = attackSuccess;
    }

    public bool getAttackSuccess()
    {
        return attackSuccess;
    }
}
