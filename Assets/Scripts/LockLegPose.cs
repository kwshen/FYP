using UnityEngine;

public class LockLegPose : MonoBehaviour
{
    private Quaternion fixedRotation; // To store the current pose of the leg

    void Start()
    {
        // Save the current local rotation as the "fixed" pose
        fixedRotation = transform.localRotation;
    }

    void LateUpdate()
    {
        // After IK or animation updates, reset the rotation to the fixed pose
        transform.localRotation = fixedRotation;
    }
}