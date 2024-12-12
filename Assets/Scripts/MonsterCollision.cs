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
            appear = true;
        }
        else if(!playerDie && gameObject.CompareTag(attackTag) && other.CompareTag(playerTag))
        {
            attack = true;
        }
        else
        {
            Debug.Log("collider not found");
        }
    }

    public bool getAppear()
    {
        return appear;
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
