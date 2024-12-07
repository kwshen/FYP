using UnityEngine;

public class Paddling : MonoBehaviour
{
    public Rigidbody kayakRigidbody; // Reference to the kayak's Rigidbody
    public Transform waterSurface;   // Optional reference to water for effects
    public float paddleForce = 10f;  // Strength of the paddle force
    public float waterDepthThreshold = 0.2f; // Minimum depth to register a stroke
    public float waterResistance = 0.7f;

    private Vector3 lastPosition; // Last frame's position for velocity calculation

    void Start()
    {
        lastPosition = transform.position;
    }

    void Update()
    {
        // Calculate paddle velocity
        Vector3 paddleVelocity = (transform.position - lastPosition) / Time.deltaTime;

        // Check if the paddle is underwater
        if (transform.position.y < waterSurface.position.y - waterDepthThreshold)
        {
            ApplyPaddlingForce(paddleVelocity);
        }

        lastPosition = transform.position;
    }

    void ApplyPaddlingForce(Vector3 paddleVelocity)
    {
        if (kayakRigidbody != null)
        {
            // Calculate direction of the force based on paddle movement
            Vector3 forceDirection = -transform.forward; // Opposite direction of paddle stroke
            //Vector3 force = forceDirection * paddleVelocity.magnitude * paddleForce;
            Vector3 force = -paddleVelocity * paddleForce;
            force.y = 0;
            // Apply force to the kayak
            kayakRigidbody.AddForce(force * waterResistance, ForceMode.Acceleration);
        }
    }

}