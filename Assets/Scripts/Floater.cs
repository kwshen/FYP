//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class Floater : MonoBehaviour
//{
//    Water_Settings water_Settings;
//    public Rigidbody rigidbody;
//    public float depthBeforeSubmerged = 1f;
//    public float displacementAmount = 3f;
//    public float waterDrag = 0.99f;
//    public float waterAngularDrag = 0.5f;
//    float waveHeight;
//    public GameObject river;

//    private void Start()
//    {
//        if(river != null)
//        {
//            water_Settings = river.GetComponent<Water_Settings>();
//        }
//    }

//    private void FixedUpdate()
//    {
//        rigidbody.AddForceAtPosition(Physics.gravity, transform.position, ForceMode.Acceleration);

//        if (water_Settings != null)
//        {
//           waveHeight = water_Settings.getWaveHeight();
//        }
//        else
//        {
//            Debug.LogWarning("Water Setting not found");
//        }

//        if (transform.position.y < river.transform.position.y)
//        {
//            float displacementMultiplier = Mathf.Clamp01((waveHeight - transform.position.y)/ depthBeforeSubmerged) * displacementAmount;
//            rigidbody.AddForceAtPosition(new Vector3(0f, Mathf.Abs(Physics.gravity.y) * displacementMultiplier * displacementAmount, 0f), transform.position, ForceMode.Acceleration);
//        }
//    }
//}




//using System.Drawing;
//using UnityEngine;

//public class Floater : MonoBehaviour
//{
//    // Reference to the water plane material, which has the wave height information
//    public Material waterVolumeMaterial;
//    public float floatSpeed = 1.0f; // Speed at which the object floats up and down

//    private Vector3 initialPosition;
//    private float waveHeight;

//    void Start()
//    {
//        //initialPosition = transform.parent.position;
//    }

//    void Update()
//    {

//        Vector3 worldPoint = transform.TransformPoint(point.localPosition);
//        if (waterVolumeMaterial != null)
//        {
//            // Get the current wave height at the position of the floater
//            waveHeight = GetWaveHeight();

//            // Apply the wave height to the floater's y position
//            float newY = initialPosition.y + Mathf.Sin(Time.time * floatSpeed) * waveHeight;
//            transform.position = new Vector3(initialPosition.x, newY, initialPosition.z);
//        }
//    }

//    // Get the current wave height from the water material (should match the wave height logic from Water_Settings)
//    float GetWaveHeight()
//    {
//        // This assumes that the wave height is being calculated from the water material's vector position as in Water_Settings
//        return waterVolumeMaterial.GetFloat("_Displacement_Amount");
//    }
//}

using UnityEngine;

public class Floater : MonoBehaviour
{
    // Reference to the water plane material, which has the wave height information
    public Material waterVolumeMaterial;
    public float floatSpeed = 5.0f; // Speed at which the object floats up and down
    private float waveHeight;

    private Rigidbody kayakRigidbody;

    void Start()
    {
        // Find the Rigidbody on the parent kayak
        kayakRigidbody = transform.parent.GetComponent<Rigidbody>();
        if (kayakRigidbody == null)
        {
            Debug.LogError("No Rigidbody found on the parent kayak!");
        }

        if (waterVolumeMaterial == null)
        {
            Debug.LogError("No water volume material assigned!");
        }
    }

    void FixedUpdate()
    {
        // Get the world position of this floater
        Vector3 floaterPosition = transform.position;

        if (waterVolumeMaterial != null && kayakRigidbody != null)
        {
            // Get the wave height at the floater's x and z position
            waveHeight = GetWaveHeight(floaterPosition);

            // If the floater is below the wave height, apply buoyancy
            if (floaterPosition.y < waveHeight)
            {
                float displacement = Mathf.Clamp01(waveHeight - floaterPosition.y); // Adjust displacement based on how submerged the floater is
                Vector3 buoyancyForce = new Vector3(0f, displacement * floatSpeed, 0f);

                // Apply the buoyancy force to the parent kayak's Rigidbody at this floater's position
                kayakRigidbody.AddForceAtPosition(buoyancyForce, floaterPosition, ForceMode.Acceleration);
            }
        }
    }

    // Get the current wave height from the water material (should match the wave height logic from Water_Settings)
    float GetWaveHeight(Vector3 position)
    {
        // Replace this with the actual logic from your Water_Settings script
        return waterVolumeMaterial.GetFloat("_Displacement_Amount") / 3.0f + position.y;
    }
}
