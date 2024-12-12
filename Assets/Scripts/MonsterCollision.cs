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
    bool playerDie = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!appear && gameObject.CompareTag(appearTag) && other.CompareTag(playerTag))
        {
            Debug.Log("APPEAR!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
            appear = true;
        }
        else if(!playerDie && gameObject.CompareTag(attackTag) && other.CompareTag(playerTag))
        {
            attack = true;
        }
        else
        {
            //Debug.Log("appear" + appear);
            //Debug.Log("attack" + attack);
            //Debug.Log("gameObject tag" + gameObject.tag);
            //Debug.Log("other tag" + other.tag);
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

    public bool getAttack()
    {
        return attack;
    }

    public void setPlayerDie(bool isPlayerDie)
    {
        this.playerDie = isPlayerDie;
    }
}
