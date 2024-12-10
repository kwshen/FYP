using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KayakManager : MonoBehaviour
{

    public float resetSpeed = 2f; // Speed of the rotation reset

    void Update()
    {
        // Get the current rotation of the kayak
        Quaternion currentRotation = transform.rotation;

        // Create a target rotation with X = -90, keeping the current Y and Z rotations
        Quaternion targetRotation = Quaternion.Euler(-90, currentRotation.eulerAngles.y,currentRotation.eulerAngles.z);

        // Smoothly interpolate between the current rotation and the target rotation
        transform.rotation = Quaternion.Slerp(currentRotation, targetRotation, resetSpeed * Time.deltaTime);
    }
}
