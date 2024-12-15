using UnityEngine;
using UnityEngine.AI;

public class BearAnimation : MonoBehaviour
{

    Animator bearAnimator;
    BearController bearController;

    // Start is called before the first frame update
    void Start()
    {
        bearAnimator = GetComponent<Animator>();
        bearController = GetComponent<BearController>();
        bearAnimator.SetInteger("state", (int)bearController.currentState);
    }

    // Update is called once per frame
    void Update()
    {
        bearAnimator.SetInteger("state", (int)bearController.currentState); 
    }
}