using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class PaddleManager : MonoBehaviour
{

    //use for check hand is still grabbing or not
    private XRBaseInteractor leftHandInteractor = null;
    private XRBaseInteractor rightHandInteractor = null;
    private XRGrabInteractable grabInteractable;

    //use for reset the position after hand release
    public Transform originalPoint;

    public Collider kayakCollider;
    public Collider waterCollider;

    void Start()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();
        grabInteractable.selectExited.AddListener(OnRelease);

        Collider paddleCollider = GetComponent<Collider>();

        if (paddleCollider != null && kayakCollider != null && waterCollider != null)
        {
            Physics.IgnoreCollision(paddleCollider, kayakCollider);
            Physics.IgnoreCollision(paddleCollider, waterCollider);
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
            transform.localRotation = Quaternion.Euler(0f, 0f, -90f);
           
        }
    }
}
