//using UnityEngine;

//public class Paddling : MonoBehaviour
//{
//    public Rigidbody kayakRigidbody; // Reference to the kayak's Rigidbody
//    public Transform waterSurface;   // Optional reference to water for effects
//    public float paddleForce = 10f;  // Strength of the paddle force
//    public float waterDepthThreshold = 0.2f; // Minimum depth to register a stroke
//    public float waterResistance = 0.7f;

//    private Vector3 lastPosition; // Last frame's position for velocity calculation

//    void Start()
//    {
//        lastPosition = transform.position;
//    }

//    void Update()
//    {
//        // Calculate paddle velocity
//        Vector3 paddleVelocity = (transform.position - lastPosition) / Time.deltaTime;

//        // Check if the paddle is underwater
//        if (transform.position.y < waterSurface.position.y - waterDepthThreshold)
//        {
//            ApplyPaddlingForce(paddleVelocity);
//        }

//        lastPosition = transform.position;
//    }

//    void ApplyPaddlingForce(Vector3 paddleVelocity)
//    {
//        if (kayakRigidbody != null)
//        {
//            // Calculate direction of the force based on paddle movement
//            Vector3 forceDirection = -transform.forward; // Opposite direction of paddle stroke
//            //Vector3 force = forceDirection * paddleVelocity.magnitude * paddleForce;
//            Vector3 force = -paddleVelocity * paddleForce;
//            force.y = 0;
//            // Apply force to the kayak
//            kayakRigidbody.AddForce(force * waterResistance, ForceMode.Acceleration);
//        }
//    }

//}

////using UnityEngine;

////public class Paddling : MonoBehaviour
////{
////    public Rigidbody kayakRigidbody; // Reference to the kayak's Rigidbody
////    public Transform waterSurface;   // Reference to water surface position
////    public float paddleForce = 10f;  // Strength of the paddle force
////    public float waterDepthThreshold = 0.2f; // Minimum depth to register a stroke
////    public float waterResistance = 0.7f;     // Resistance applied to paddling

////    private Vector3 lastPosition; // Last frame's position for velocity calculation
////    private bool isUnderwater;    // Whether the paddle is currently underwater

////    void Start()
////    {
////        lastPosition = transform.position;
////    }

////    void Update()
////    {
////        // Calculate paddle velocity
////        Vector3 paddleVelocity = (transform.position - lastPosition) / Time.deltaTime;

////        // Check if the paddle is underwater
////        isUnderwater = transform.position.y < waterSurface.position.y - waterDepthThreshold;

////        if (isUnderwater)
////        {
////            ApplyPaddlingForce(paddleVelocity);
////        }

////        lastPosition = transform.position;
////    }

////    void ApplyPaddlingForce(Vector3 paddleVelocity)
////    {
////        if (kayakRigidbody != null && paddleVelocity.magnitude > 0.1f) // Avoid applying force for minor movements
////        {
////            // Calculate direction of the force based on paddle movement
////            Vector3 forceDirection = -paddleVelocity.normalized; // Opposite direction of paddle movement
////            forceDirection.y = 0; // Ignore vertical force to avoid unrealistic movement

////            // Adjust the force based on paddle speed and resistance
////            float effectiveForce = paddleVelocity.magnitude * paddleForce * waterResistance;
////            Vector3 force = forceDirection * effectiveForce;

////            // Apply force to the kayak
////            kayakRigidbody.AddForce(force, ForceMode.Acceleration);

////            // Optional: Adjust kayak rotation based on paddling direction
////            AdjustKayakRotation(forceDirection, effectiveForce);
////        }
////    }

////    void AdjustKayakRotation(Vector3 forceDirection, float forceMagnitude)
////    {
////        // Rotate the kayak slightly based on the direction of the applied force
////        float rotationAmount = forceMagnitude * 0.1f; // Adjust rotation sensitivity
////        Quaternion targetRotation = Quaternion.LookRotation(forceDirection, Vector3.up);
////        targetRotation.x = 0; targetRotation.z = 0;
////        kayakRigidbody.rotation = Quaternion.Slerp(kayakRigidbody.rotation, targetRotation, rotationAmount * Time.deltaTime);
////    }
////}


using UnityEngine;

public class Paddling : MonoBehaviour
{
    public Rigidbody kayakRigidbody; // Reference to the kayak's Rigidbody
    public Transform waterSurface;   // Reference to the water's surface (for depth checks)
    public float paddleForce = 10f;  // Strength of the paddle force
    public float waterDepthThreshold = 0.2f; // Minimum depth for paddle to apply force
    public float stabilityForce = 5f; // Force to stabilize the kayak's rotation
    public float rotationSpeed = 2f;  // Speed for rotating the kayak based on paddling force

    private Vector3 lastPosition; // Last position of the paddle for velocity calculation
    private Vector3 forceDirection; // Direction of the paddling force

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
            // Calculate force based on paddle velocity
            Vector3 force = -paddleVelocity * paddleForce;
            force.y = -1; // Ignore vertical forces to prevent unnecessary flips

            // Apply force to the kayak
            kayakRigidbody.AddForce(force, ForceMode.Acceleration);

            // Calculate rotational force to turn the kayak
            if (force.magnitude > 0.1f)
            {
                forceDirection = force.normalized;
                forceDirection.x = 0;forceDirection.z = 0;
                Quaternion targetRotation = Quaternion.LookRotation(forceDirection, Vector3.up);
                kayakRigidbody.rotation = Quaternion.Slerp(kayakRigidbody.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            }
        }
    }


}
