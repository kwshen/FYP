using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterCrabAnimation : MonoBehaviour
{
    Animator crabAnimator;
    CrabController crabController;
    MonsterCollision[] monsterCollision;
    AttackPlayer[] attackPlayer;

    // Start is called before the first frame update
    void Start()
    {
        crabAnimator = GetComponent<Animator>();
        crabController = GetComponent<CrabController>();
        monsterCollision = GetComponentsInChildren<MonsterCollision>();
        attackPlayer = GetComponentsInChildren<AttackPlayer>();
        crabAnimator.SetInteger("state", (int)crabController.currentState);
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < monsterCollision.Length; i++)
        {
            if (monsterCollision[i].getAppear())
            {
                crabAnimator.SetTrigger("Appear");
                break;
            };
        }
        
        for (int i = 0; i < monsterCollision.Length; i++)
        {
            if (monsterCollision[i].getAttack())
            {
                crabAnimator.SetTrigger("Attack");
                break;
            };
        }

        crabAnimator.SetInteger("state", (int)crabController.currentState);
    }
}
