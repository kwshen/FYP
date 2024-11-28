using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floater : MonoBehaviour
{
    Water_Settings water_Settings;
    public Rigidbody rigidbody;
    public float depthBeforeSubmerged = 1f;
    public float displacementAmount = 3f;
    public float waterDrag = 0.99f;
    public float waterAngularDrag = 0.5f;
    float waveHeight;

    private void FixedUpdate()
    {
        rigidbody.AddForceAtPosition(Physics.gravity, transform.position, ForceMode.Acceleration);

        if (water_Settings != null)
        {
           waveHeight = water_Settings.getWaveHeight();
        }
        else
        {
            Debug.LogWarning("Water Setting not found");
        }
 
        if (transform.position.y < 0)
        {
            float displacementMultiplier = Mathf.Clamp01((waveHeight - transform.position.y)/ depthBeforeSubmerged) * displacementAmount;
            rigidbody.AddForceAtPosition(new Vector3(0f, Mathf.Abs(Physics.gravity.y) * displacementMultiplier * displacementAmount, 0f), transform.position, ForceMode.Acceleration);
        }
    }
}
