using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class PaddleManager : MonoBehaviour
{
    //save start rotation
    private Quaternion initialRotation;

    //use for check hand is still grabbing or not
    private XRBaseInteractor leftHandInteractor = null;
    private XRBaseInteractor rightHandInteractor = null;
    private XRGrabInteractable grabInteractable;

    //use for reset the position after hand release
    public Transform originalPoint;

    public GameObject kayak;

    void Start()
    {
        initialRotation = transform.rotation;
        grabInteractable = GetComponent<XRGrabInteractable>();
        grabInteractable.selectExited.AddListener(OnRelease);

        Collider kayakCollider = kayak.GetComponent<Collider>();
        Collider paddleCollider = GetComponent<Collider>();

        if (paddleCollider != null && kayakCollider != null)
        {
            Physics.IgnoreCollision(paddleCollider, kayakCollider);
        }
        else
        {
            Debug.Log("collider not found");
        }
    }

    void OnDestroy()
    {
        grabInteractable.selectExited.RemoveListener(OnRelease);
    }



    private void OnRelease(SelectExitEventArgs args)
    {
       
        // Reset the paddle's parent if neither hand is holding it
        if (leftHandInteractor == null && rightHandInteractor == null)
        {
            transform.position = originalPoint.transform.position;
            transform.rotation = initialRotation;
        }
    }
}

