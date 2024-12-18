using UnityEngine;

public class Floater : MonoBehaviour
{
    // Reference to the water plane material, which has the wave height information
    public Material waterVolumeMaterial;
    public float floatSpeed = 5.0f; // Speed at which the object floats up and down
    private float waveHeight;

    public Rigidbody kayakRigidbody;

    void Start()
    {

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
