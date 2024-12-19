using UnityEngine;
using System.Collections;

public class SardineAutoWander : MonoBehaviour
{
    public float forwardSpeed = 5f; // Speed of forward movement
    public float turnSpeed = 2f;    // Speed of turning
    public float changeDirectionInterval = 2f; // Time interval to change direction
    public GameObject river; // Reference to the river GameObject
    

    private Rigidbody sardineRigid;
    private Animator sardineAnimator;
    private float timer = 0f;
    private Vector3 randomDirection;

    void Start()
    {
        sardineRigid = GetComponent<Rigidbody>();
        sardineAnimator = GetComponent<Animator>();
        river = GameObject.FindGameObjectWithTag("Water");

        // Initialize random direction
        randomDirection = GetRandomDirection();
        sardineAnimator.SetFloat("Forward", forwardSpeed);
    }

    void Update()
    {
        timer += Time.deltaTime;

        // Change direction at intervals
        if (timer >= changeDirectionInterval)
        {
            timer = 0f;
            randomDirection = GetRandomDirection();
        }

        // Apply movement and rotation
        MoveForward();
        TurnTowards(randomDirection);

        // Constrain height to river bounds
        ConstrainHeight();
    }

    private Vector3 GetRandomDirection()
    {
        // Generate a random direction vector
        float randomX = Random.Range(-1f, 1f);
        float randomY = Random.Range(-1f, 1f);
        float randomZ = Random.Range(-1f, 1f);
        return new Vector3(randomX, randomY, randomZ).normalized;
    }

    private void MoveForward()
    {
        // Move the sardine forward
        sardineRigid.AddForce(transform.forward * forwardSpeed);
    }

    private void TurnTowards(Vector3 direction)
    {
        // Calculate rotation towards the target direction
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);
    }

    private void ConstrainHeight()
    {
        if (river != null)
        {
            float riverHeight = river.transform.position.y;
            Vector3 constrainedPosition = transform.position;
            constrainedPosition.y = Mathf.Min(constrainedPosition.y, riverHeight);
            transform.position = constrainedPosition;
        }
    }
}
