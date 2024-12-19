//using TMPro;
//using UnityEngine;
//using UnityEngine.XR.Interaction.Toolkit;

//public class Paddling : MonoBehaviour
//{
//    [Header("Kayak References")]
//    public Rigidbody kayakRigidbody; // Reference to the kayak's Rigidbody
//    public Transform waterSurface;   // Reference to the water's surface (for depth checks)

//    [Header("Paddling Parameters")]
//    public float paddleForce = 10f;  // Strength of the paddle force
//    public float waterDepthThreshold = 0.2f; // Minimum depth for paddle to apply force
//    public float rotationForce = 2f; // Force applied for kayak rotation
//    public float maxPaddleAngle = 45f; // Maximum angle of paddle from vertical

//    [Header("Paddle Position")]
//    public Transform paddleLeftReference;  // Reference point for left-side paddle
//    public Transform paddleRightReference; // Reference point for right-side paddle

//    private Vector3 lastLeftPaddlePosition;
//    private Vector3 lastRightPaddlePosition;
//    private Vector3 leftPaddleVelocity;
//    private Vector3 rightPaddleVelocity;

//    //use for check hand is still grabbing or not
//    public XRDirectInteractor leftHandInteractor;
//    public XRDirectInteractor rightHandInteractor;

//    bool leftPaddleUnderwater;
//    bool rightPaddleUnderwater;
//    private bool isPaddling = false;

//    void Start()
//    {
//        // Ensure references are set
//        if (paddleLeftReference == null || paddleRightReference == null)
//        {
//            Debug.LogError("Paddle reference transforms are not set!");
//        }

//        // Initialize last positions
//        lastLeftPaddlePosition = paddleLeftReference.position;
//        lastRightPaddlePosition = paddleRightReference.position;
//    }

//    void Update()
//    {
//        // Calculate paddle velocities
//        leftPaddleVelocity = (paddleLeftReference.position - lastLeftPaddlePosition) / Time.deltaTime;
//        rightPaddleVelocity = (paddleRightReference.position - lastRightPaddlePosition) / Time.deltaTime;

//        // Check if paddles are underwater
//        leftPaddleUnderwater = paddleLeftReference.position.y < waterSurface.position.y - waterDepthThreshold;
//        rightPaddleUnderwater = paddleRightReference.position.y < waterSurface.position.y - waterDepthThreshold;

//        if (leftHandInteractor.selectTarget != null && rightHandInteractor.selectTarget != null)
//        {
//            // Apply paddling forces
//            if (leftPaddleUnderwater)
//            {

//                    AudioManager.Instance.PlaySFX("PaddleWater");

//                ApplyPaddlingForce(leftPaddleVelocity, true);
//            }

//            if (rightPaddleUnderwater)
//            {
//                ApplyPaddlingForce(rightPaddleVelocity, false);
//            }
//        }
//        else
//        {
//            leftPaddleUnderwater = false;
//            rightPaddleUnderwater = false;
//        }

//        // Update last positions
//        lastLeftPaddlePosition = paddleLeftReference.position;
//        lastRightPaddlePosition = paddleRightReference.position;


//    }

//    void ApplyPaddlingForce(Vector3 paddleVelocity, bool isLeftPaddle)
//    {
//        if (kayakRigidbody == null) return;

//        // Calculate force based on paddle velocity
//        Vector3 force = -paddleVelocity * paddleForce;
//        // Ignore vertical forces prevent fly up
//        force.y = 0; 

//        // Cacululate paddle angle
//        float paddleAngle = Vector3.Angle(Vector3.down, paddleVelocity.normalized);

//        // apply force if paddle is within a reasonable angle
//        if (paddleAngle <= maxPaddleAngle && force.magnitude > 0.1f)
//        {
//            isPaddling = true;

//            // Transform force to kayak's local space, accounting for initial -90 rotation
//            Vector3 localForce = kayakRigidbody.transform.InverseTransformDirection(force);

//            // Apply main propulsion force in world space
//            kayakRigidbody.AddForce(force, ForceMode.Acceleration);

//            // Calculate rotation based on paddle side, accounting for initial rotation
//            Vector3 rotationAxis = isLeftPaddle ? kayakRigidbody.transform.up : -kayakRigidbody.transform.up;
//            Vector3 rotationForceVector = Vector3.Cross(force.normalized, rotationAxis) * rotationForce;

//            // Apply rotational force
//            kayakRigidbody.AddTorque(rotationForceVector, ForceMode.Acceleration);
//        }
//    }

//    public bool getLeftPaddleUnderWater()
//    {
//        return leftPaddleUnderwater;
//    }
//     public bool getRightPaddleUnderWater()
//    {
//        return rightPaddleUnderwater;
//    }

//    public bool getIsPaddling()
//    {
//        return isPaddling;
//    }
//}

using TMPro;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Paddling : MonoBehaviour
{
    [Header("Kayak References")]
    public Rigidbody kayakRigidbody;
    public Transform waterSurface;

    [Header("Paddling Parameters")]
    public float paddleForce = 10f;
    public float waterDepthThreshold = 0.2f;
    public float rotationForce = 2f;
    public float maxPaddleAngle = 45f;

    [Header("Paddle Position")]
    public Transform paddleLeftReference;
    public Transform paddleRightReference;

    private Vector3 lastLeftPaddlePosition;
    private Vector3 lastRightPaddlePosition;
    private Vector3 leftPaddleVelocity;
    private Vector3 rightPaddleVelocity;

    public XRDirectInteractor leftHandInteractor;
    public XRDirectInteractor rightHandInteractor;

    private bool leftPaddleUnderwater;
    private bool rightPaddleUnderwater;
    private bool wasLeftPaddleUnderwater;  // New tracking variable
    private bool wasRightPaddleUnderwater; // New tracking variable
    private bool isPaddling = false;

    void Start()
    {
        if (paddleLeftReference == null || paddleRightReference == null)
        {
            Debug.LogError("Paddle reference transforms are not set!");
        }

        lastLeftPaddlePosition = paddleLeftReference.position;
        lastRightPaddlePosition = paddleRightReference.position;

        // Initialize underwater states
        wasLeftPaddleUnderwater = false;
        wasRightPaddleUnderwater = false;
    }

    void Update()
    {
        leftPaddleVelocity = (paddleLeftReference.position - lastLeftPaddlePosition) / Time.deltaTime;
        rightPaddleVelocity = (paddleRightReference.position - lastRightPaddlePosition) / Time.deltaTime;

        // Store previous state
        wasLeftPaddleUnderwater = leftPaddleUnderwater;
        wasRightPaddleUnderwater = rightPaddleUnderwater;

        // Update current state
        leftPaddleUnderwater = paddleLeftReference.position.y < waterSurface.position.y - waterDepthThreshold;
        rightPaddleUnderwater = paddleRightReference.position.y < waterSurface.position.y - waterDepthThreshold;

        if (leftHandInteractor.selectTarget != null && rightHandInteractor.selectTarget != null)
        {
            // Play sound only when paddle enters water (transition from above to below)
            if (!wasLeftPaddleUnderwater && leftPaddleUnderwater)
            {
                AudioManager.Instance.PlaySFX("PaddleWater");
            }
            else if (!wasRightPaddleUnderwater && rightPaddleUnderwater)
            {
                AudioManager.Instance.PlaySFX("PaddleWater");
            }

            // Apply paddling forces
            if (leftPaddleUnderwater)
            {
                ApplyPaddlingForce(leftPaddleVelocity, true);
            }

            if (rightPaddleUnderwater)
            {
                ApplyPaddlingForce(rightPaddleVelocity, false);
            }
        }
        else
        {
            leftPaddleUnderwater = false;
            rightPaddleUnderwater = false;
        }

        lastLeftPaddlePosition = paddleLeftReference.position;
        lastRightPaddlePosition = paddleRightReference.position;
    }

    void ApplyPaddlingForce(Vector3 paddleVelocity, bool isLeftPaddle)
    {
        if (kayakRigidbody == null) return;

        // Calculate force based on paddle velocity
        Vector3 force = -paddleVelocity * paddleForce;
        // Ignore vertical forces prevent fly up
        force.y = 0;

        // Cacululate paddle angle
        float paddleAngle = Vector3.Angle(Vector3.down, paddleVelocity.normalized);

        // apply force if paddle is within a reasonable angle
        if (paddleAngle <= maxPaddleAngle && force.magnitude > 0.1f)
        {
            isPaddling = true;

            // Transform force to kayak's local space, accounting for initial -90 rotation
            Vector3 localForce = kayakRigidbody.transform.InverseTransformDirection(force);

            // Apply main propulsion force in world space
            kayakRigidbody.AddForce(force, ForceMode.Acceleration);

            // Calculate rotation based on paddle side, accounting for initial rotation
            Vector3 rotationAxis = isLeftPaddle ? kayakRigidbody.transform.up : -kayakRigidbody.transform.up;
            Vector3 rotationForceVector = Vector3.Cross(force.normalized, rotationAxis) * rotationForce;

            // Apply rotational force
            kayakRigidbody.AddTorque(rotationForceVector, ForceMode.Acceleration);
        }
    }

    public bool getLeftPaddleUnderWater()
    {
        return leftPaddleUnderwater;
    }
    public bool getRightPaddleUnderWater()
    {
        return rightPaddleUnderwater;
    }

    public bool getIsPaddling()
    {
        return isPaddling;
    }
}