using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class KayakJoystickMovement : MonoBehaviour
{
    [Header("Input Actions")]
    public InputActionProperty moveAction;  // Reference to Move action

    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float rotationSpeed = 100f;
    public float maxSpeed = 10f;

    [Header("Turning Settings")]
    public float turnForce = 2f;
    public float bankAngle = 5f;
    public float bankSpeed = 1f;
    public float waterResistance = 0.95f;

    [Header("References")]
    public Rigidbody kayakRigidbody;
    private PlayerController playerController;

    [Header("Input Smoothing")]
    public float movementSmoothTime = 0.1f;
    private Vector2 currentVelocity;
    private Vector2 smoothedInput;
    private float currentBankAngle = 0f;

    private void OnEnable()
    {
        moveAction.action.Enable();
    }

    private void OnDisable()
    {
        moveAction.action.Disable();
    }

    private void Start()
    {
        if (kayakRigidbody == null)
            kayakRigidbody = GetComponent<Rigidbody>();

        playerController = GetComponent<PlayerController>();

        kayakRigidbody.angularDrag = 2f;
        kayakRigidbody.drag = 1f;
    }

    private void FixedUpdate()
    {
        if (playerController.getIsPlayerDie())
            return;

        // Get input from the action
        Vector2 joystickInput = moveAction.action.ReadValue<Vector2>();

        // Smooth the input
        smoothedInput = Vector2.SmoothDamp(smoothedInput, joystickInput, ref currentVelocity, movementSmoothTime);

        // Calculate movement direction based on kayak's forward direction
        Vector3 moveDirection = transform.forward * smoothedInput.y;

        // Apply forward/backward movement force
        if (smoothedInput.y != 0 && kayakRigidbody.velocity.magnitude < maxSpeed)
        {
            kayakRigidbody.AddForce(moveDirection * moveSpeed, ForceMode.Acceleration);
        }

        // Apply turning force
        ApplyTurningForce(smoothedInput.x);

        // Apply banking effect during turns
        ApplyBankingEffect(smoothedInput.x);
    }

    private void ApplyTurningForce(float horizontalInput)
    {
        if (Mathf.Abs(horizontalInput) > 0.1f)
        {
            float speedMultiplier = Mathf.Clamp(kayakRigidbody.velocity.magnitude / maxSpeed, 0.2f, 1f);
            float actualTurnForce = turnForce * speedMultiplier;

            Vector3 turnTorque = Vector3.up * (horizontalInput * actualTurnForce);
            kayakRigidbody.AddTorque(turnTorque, ForceMode.Acceleration);
        }

        kayakRigidbody.angularVelocity *= waterResistance;
    }

    private void ApplyBankingEffect(float horizontalInput)
    {
        float targetBankAngle = -horizontalInput * bankAngle;
        currentBankAngle = Mathf.Lerp(currentBankAngle, targetBankAngle, Time.fixedDeltaTime * bankSpeed);

        Quaternion bankRotation = Quaternion.Euler(transform.rotation.eulerAngles.x,
                                                 transform.rotation.eulerAngles.y,
                                                 currentBankAngle);

        kayakRigidbody.MoveRotation(bankRotation);
    }
}
