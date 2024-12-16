using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterCrabAnimation : MonoBehaviour
{
    Animator crabAnimator;
    CrabController crabController;

    // Start is called before the first frame update
    void Start()
    {
        crabAnimator = gameObject.GetComponent<Animator>();
        crabController = gameObject.GetComponent<CrabController>();
    }

    // Update is called once per frame
    void Update()
    {

        if (crabController.getIsJump() == true)
        {

            crabAnimator.SetTrigger("Appear");
        }

        else if (crabController.getIsAttack() == true)
        {

            crabAnimator.SetTrigger("Attack");
        }
        //else
        //{
        //    crabAnimator.SetInteger("state", (int)crabController.currentState);
        //}

        //run animation
        if (crabController.getIsChasing() == true)
        {
            crabAnimator.SetInteger("state", 2);
        }
        //walk animation
        else if (crabController.getIsWandering() == true)
        {
            crabAnimator.SetInteger("state", 1);
        }
        //idle animaiton
        else
        {
            crabAnimator.SetInteger("state", 0);
        }

    }


}



