using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrabAnimation : MonoBehaviour
{
    protected Animator crabAnimator;
    protected CrabController crabController;

    // Start is called before the first frame update
    protected void Start()
    {
        crabAnimator = gameObject.GetComponent<Animator>();
        crabController = gameObject.GetComponent<CrabController>();
    }

    // Update is called once per frame
    protected void Update()
    {

        if (crabController.getIsSpecialMoving() == true)
        {

            crabAnimator.SetTrigger("SpecialMove");
        }

        else if (crabController.getIsAttack() == true)
        {
            //transform.Rotate(0f, 180f, 0f);
            transform.localRotation = Quaternion.Euler(0f, 180f, 0f);
            crabAnimator.SetTrigger("Attack");
        }

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



