using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterCrabAnimation : MonoBehaviour
{
    Animator crabAnimator;
    CrabController crabController;
    private string childName = "000";

    // Start is called before the first frame update
    void Start()
    {
        crabAnimator = GetComponent<Animator>();
        crabController = GetComponent<CrabController>();
        crabAnimator.SetInteger("state", (int)crabController.currentState);
        transform.Find(childName).rotation = Quaternion.Euler(transform.eulerAngles.x, 0, transform.eulerAngles.z);
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
        else
        {
            crabAnimator.SetInteger("state", (int)crabController.currentState);
        }

    }


}



