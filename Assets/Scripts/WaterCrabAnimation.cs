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
        crabAnimator = GetComponent<Animator>();
        crabController = GetComponent<CrabController>();
        crabAnimator.SetInteger("state", (int)crabController.currentState);
    }

    // Update is called once per frame
    void Update()
    {
       crabAnimator.SetInteger("state", (int)crabController.currentState);
    }
}
