//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.AI;
//using UnityEngine.InputSystem.Android;

//public class BearAnimation : MonoBehaviour
//{

//    Animator bearAnimator;
//    BearController bearController;
//    NavMeshAgent agent;

//    // Start is called before the first frame update
//    void Start()
//    {
//        agent = GetComponent<NavMeshAgent>();
//        bearAnimator = GetComponent<Animator>();
//        bearController = GetComponent<BearController>();
//        bearAnimator.SetBool("isIdle", true);
//    }

//    // Update is called once per frame
//    void Update()
//    {
//        if(agent.velocity.magnitude > 0.01f)
//        {
//            bearAnimator.SetBool("isIdle", false);
//            bearAnimator.SetBool("isWalking", true) ;
//            bearAnimator.SetBool("isSleeping", false) ;
//        }
//        if (bearController.isSleeping && agent.velocity.magnitude == 0)
//        {
//            //Debug.Log(bearController.isSleeping);
//            bearAnimator.SetBool("isSleeping", true) ;
//            bearAnimator.SetBool("isIdle", false);
//            bearAnimator.SetBool("isWalking", false);
//        }

//    }
//}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem.Android;

public class BearAnimation : MonoBehaviour
{

    Animator bearAnimator;
    BearController bearController;
    NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        bearAnimator = GetComponent<Animator>();
        bearController = GetComponent<BearController>();
        bearAnimator.SetInteger("state", (int)bearController.currentState);
    }

    // Update is called once per frame
    void Update()
    {
        //if (agent.velocity.magnitude > 0.01f)
        //{
        //    bearAnimator.SetInteger("state", (int)bearController.currentState);
        //}
        //if (bearController.isSleeping && agent.velocity.magnitude == 0)
        //{
        //    bearAnimator.SetInteger("state", (int)bearController.currentState);
        //}
        bearAnimator.SetInteger("state", (int)bearController.currentState); 
    }
}