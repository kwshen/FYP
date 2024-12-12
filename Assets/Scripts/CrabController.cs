//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class CrabController : MonoBehaviour
//{
//    public enum CrabState { Standing, Wandering, Chasing }

//    [Header("Behavior Settings")]
//    public bool isWanderingCrab = true; // Toggle to determine monster type
//    public CrabState currentState = CrabState.Standing;

//    [Header("Wandering Parameters")]
//    Transform wanderCenterPoint;
//    public int wanderRadius = 150;
//    public float wanderInterval = 5f; // Time between wandering attempts
//    private float wanderTimer = 0f;

//    private UnityEngine.AI.NavMeshAgent agent;
//    private ChaseArea chaseArea;
//    private int waterAreaMask;
//    private Transform nearestTarget;

//    void Start()
//    {
//        wanderCenterPoint = transform;
//        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
//        chaseArea = GetComponentInChildren<ChaseArea>();
//        waterAreaMask = 5 << UnityEngine.AI.NavMesh.GetAreaFromName("Water");

//        // Set initial state based on monster type
//        currentState = isWanderingCrab ? CrabState.Wandering : CrabState.Standing;
//    }

//    void Update()
//    {
//        switch (currentState)
//        {
//            case CrabState.Wandering:
//                if (isWanderingCrab)
//                {
//                    HandleWandering();
//                }
//                break;
//            case CrabState.Standing:
//                if (!isWanderingCrab)
//                {
//                    HandleStanding();
//                }
//                break;
//        }

//    }

//    void HandleStanding()
//    {

//    }

//    void HandleWandering()
//    {
//        wanderTimer += Time.deltaTime;

//        bool isAtDestination = !agent.pathPending &&
//                               agent.remainingDistance <= agent.stoppingDistance;

//        if (isAtDestination && wanderTimer >= wanderInterval)
//        {
//            wanderTimer = 0f;
//            Vector3 randomPos = Position.GetRandomPosition(wanderCenterPoint, wanderRadius);

//            // Use the isValidPosition method to check for valid underwater positioning
//            if (Position.isValidPosition(randomPos, waterAreaMask))
//            {
//                Debug.Log("going new underwater pos");
//                agent.SetDestination(randomPos);

//                // Small chance of transitioning to another state (if applicable)
//                if (Random.Range(0f, 1f) < 0.01f)
//                {
//                    //TransitionToState(CrabState.Sleeping);
//                }
//            }
//        }
//    }

//    //void HandleChasing()
//    //{

//    //    if (Position.isValidPosition(transform.position, waterAreaMask))
//    //    {
//    //        agent.SetDestination(nearestTarget.position);
//    //    }
//    //    else
//    //    {
//    //        TransitionToState(BearState.Wandering);
//    //    }
//    //}

//    void TransitionToState(CrabState newState)
//    {

//        switch (newState)
//        {
//            case CrabState.Wandering:
//                break;
//            case CrabState.Standing:
//                break;
//                //case BearState.Sleeping:
//                //    agent.ResetPath();
//                //    break;
//                //case BearState.Chasing:
//                //    agent.ResetPath();
//                //    break;
//        }

//        currentState = newState;
//    }


//}




//==============================================================================================================================================


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CrabController : MonoBehaviour
{
    public enum CrabState { Standing, Wandering, Jumping, Chasing }

    [Header("Behavior Settings")]
    public bool isWanderingCrab = true;
    public CrabState currentState = CrabState.Standing;

    [Header("Jumping Parameters")]
    public float jumpForce = 5f;
    public float jumpAngle = 60f;
    public float jumpHeight = 2f;
    public float jumpCooldown = 2f;
    private float lastJumpTime;

    [Header("Wandering Parameters")]
    public Transform wanderCenterPoint;
    public int wanderRadius = 150;
    public float wanderInterval = 5f;
    private float wanderTimer = 0f;

    private NavMeshAgent agent;
    private ChaseArea chaseArea;
    private Rigidbody rb;
    private int waterAreaMask;
    private Transform nearestTarget;
    private MonsterCollision[] monsterCollision;

    void Start()
    {
        wanderCenterPoint = transform;
        agent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
        monsterCollision = GetComponentsInChildren<MonsterCollision>();
        chaseArea = GetComponentInChildren<ChaseArea>();

        waterAreaMask = 1 << NavMesh.GetAreaFromName("Water");

        // Set initial state based on monster type
        currentState = isWanderingCrab ? CrabState.Wandering : CrabState.Standing;

        // Disable rigidbody physics initially if using NavMesh
        if (rb != null)
        {
            rb.isKinematic = true;
        }
    }

    void Update()
    {
        // Check for appearance trigger
        for (int i = 0; i < monsterCollision.Length; i++)
        {
            if (monsterCollision[i] != null && monsterCollision[i].getAppear())
            {
                TryJumpOntoRiver();
                break;
            }
        }

        //if (monsterCollision != null && monsterCollision.getAppear())
        //{

        //}

        //Debug.Log(monsterCollision.getAppear());

        switch (currentState)
        {
            case CrabState.Wandering:
                if (isWanderingCrab)
                {
                    HandleWandering();
                }
                break;
            case CrabState.Standing:
                if (!isWanderingCrab)
                {
                    HandleStanding();
                }
                break;
            case CrabState.Jumping:
                HandleJumping();
                break;
        }
    }

    void TryJumpOntoRiver()
    {
        // Check if enough time has passed since last jump and not currently jumping
        if (currentState != CrabState.Jumping &&
            Time.time - lastJumpTime > jumpCooldown)
        {
            TransitionToState(CrabState.Jumping);
        }
    }

    void HandleJumping()
    {
        // Ensure we have a Rigidbody component
        if (rb == null) return;

        // Temporarily disable NavMesh agent
        if (agent != null)
        {
            agent.enabled = false;
        }

        // Kinematic physics during jump
        rb.isKinematic = false;

        // Calculate jump direction and force
        Vector3 jumpDirection = CalculateJumpTrajectory();
        rb.AddForce(jumpDirection, ForceMode.Impulse);

        // Mark last jump time
        lastJumpTime = Time.time;

        // Transition back to wandering after jump
        StartCoroutine(ResetAfterJump());
    }

    Vector3 CalculateJumpTrajectory()
    {
        // Calculate jump vector based on angle and force
        Vector3 jumpVector = transform.forward * Mathf.Cos(jumpAngle * Mathf.Deg2Rad) * jumpForce;
        jumpVector += Vector3.up * Mathf.Sin(jumpAngle * Mathf.Deg2Rad) * jumpForce;

        return jumpVector;
    }

    IEnumerator ResetAfterJump()
    {
        // Wait for jump to complete
        yield return new WaitForSeconds(1f);

        // Re-enable NavMesh agent
        if (agent != null)
        {
            agent.enabled = true;
            rb.isKinematic = true;
        }

        // Transition back to previous state or wandering
        TransitionToState(CrabState.Wandering);
    }

    void HandleWandering()
    {
        wanderTimer += Time.deltaTime;
        bool isAtDestination = !agent.pathPending &&
                               agent.remainingDistance <= agent.stoppingDistance;
        if (isAtDestination && wanderTimer >= wanderInterval)
        {
            wanderTimer = 0f;
            Vector3 randomPos = Position.GetRandomPosition(wanderCenterPoint, wanderRadius);

            // Use the isValidPosition method to check for valid underwater positioning
            if (Position.isValidPosition(randomPos, waterAreaMask))
            {
                Debug.Log("Moving to new underwater position");
                agent.SetDestination(randomPos);
            }
        }
    }

    void HandleStanding()
    {
        // Optional: Add standing behavior if needed
    }

    void TransitionToState(CrabState newState)
    {
        // State transition logic
        currentState = newState;
    }
}



//===================================================================================================================================================

//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.AI;

//public class CrabController : MonoBehaviour
//{
//    public enum CrabState { Standing, Wandering, Jumping, Chasing }

//    [Header("Behavior Settings")]
//    public bool isWanderingCrab = true;
//    public CrabState currentState = CrabState.Standing;

//    [Header("Jumping Parameters")]
//    public float jumpForce = 10f;
//    public float jumpUpwardForce = 5f;
//    public float jumpCooldown = 2f;
//    public float jumpThreshold = 0.5f; // Minimum jump distance
//    private float lastJumpTime;

//    [Header("Wandering Parameters")]
//    public Transform wanderCenterPoint;
//    public int wanderRadius = 150;
//    public float wanderInterval = 5f;
//    private float wanderTimer = 0f;

//    private NavMeshAgent agent;
//    private Rigidbody rb;
//    private int waterAreaMask;
//    private MonsterCollision[] monsterCollision;

//    // Jump target
//    private Vector3 jumpTarget;
//    private bool isJumping = false;

//    void Start()
//    {
//        wanderCenterPoint = transform;
//        agent = GetComponent<NavMeshAgent>();
//        rb = GetComponent<Rigidbody>();
//        monsterCollision = GetComponentsInChildren<MonsterCollision>();

//        waterAreaMask = 1 << NavMesh.GetAreaFromName("Water");

//        // Set initial state based on monster type
//        currentState = isWanderingCrab ? CrabState.Wandering : CrabState.Standing;

//        // Disable rigidbody physics initially
//        if (rb != null)
//        {
//            rb.isKinematic = true;
//        }
//    }

//    void Update()
//    {


//        // Check for appearance trigger
//        for (int i = 0; i < monsterCollision.Length; i++)
//        {
//            if (monsterCollision[i] == null)
//            {
//                Debug.Log("collision not found");
//            }

//            if (monsterCollision[i] != null && monsterCollision[i].getAppear())
//            {
//                TryJumpOntoRiver();
//                Debug.Log("try to jump in for loop");
//                break;
//            }
//        }

//        switch (currentState)
//        {
//            case CrabState.Wandering:
//                if (isWanderingCrab)
//                {
//                    HandleWandering();
//                }
//                break;
//            case CrabState.Standing:
//                if (!isWanderingCrab)
//                {
//                    HandleStanding();
//                }
//                break;
//            case CrabState.Jumping:
//                HandleJumping();
//                break;
//        }
//    }

//    void TryJumpOntoRiver()
//    {
//        // Check if enough time has passed since last jump and not currently jumping
//        if (currentState != CrabState.Jumping &&
//            Time.time - lastJumpTime > jumpCooldown)
//        {
//            // Find a valid water area point
//            NavMeshHit hit;
//            if (NavMesh.SamplePosition(transform.position, out hit, 100f, waterAreaMask))
//            {
//                // Only jump if target is sufficiently far
//                float jumpDistance = Vector3.Distance(transform.position, hit.position);
//                if (jumpDistance > jumpThreshold)
//                {
//                    jumpTarget = hit.position;
//                    TransitionToState(CrabState.Jumping);
//                }
//            }
//        }
//    }

//    void HandleJumping()
//    {
//        if (rb == null) return;

//        // Disable NavMesh agent
//        if (agent != null)
//        {
//            agent.enabled = false;
//        }

//        // Calculate jump direction
//        Vector3 jumpDirection = (jumpTarget - transform.position).normalized;

//        // Kinematic physics during jump
//        rb.isKinematic = false;

//        // Apply forward force
//        Vector3 horizontalForce = jumpDirection * jumpForce;

//        // Apply upward force
//        Vector3 verticalForce = Vector3.up * jumpUpwardForce;

//        // Combine forces
//        rb.AddForce(horizontalForce + verticalForce, ForceMode.Impulse);

//        // Mark last jump time
//        lastJumpTime = Time.time;
//        isJumping = true;

//        // Start coroutine to handle jump completion
//        StartCoroutine(CompleteJump());
//    }

//    IEnumerator CompleteJump()
//    {
//        // Wait for jump to complete
//        yield return new WaitForSeconds(2f);

//        // Ensure we land on the target area
//        NavMeshHit hit;
//        if (NavMesh.SamplePosition(jumpTarget, out hit, 10f, waterAreaMask))
//        {
//            // Teleport to the water area
//            transform.position = hit.position;

//            // Re-enable NavMesh agent
//            if (agent != null)
//            {
//                rb.isKinematic = true;
//                agent.enabled = true;
//                agent.Warp(hit.position);
//            }

//            // Transition back to wandering
//            TransitionToState(CrabState.Wandering);
//        }
//        else
//        {
//            // Fallback if can't find water area
//            transform.position = jumpTarget;

//            if (agent != null)
//            {
//                rb.isKinematic = true;
//                agent.enabled = true;
//                agent.Warp(jumpTarget);
//            }

//            TransitionToState(CrabState.Wandering);
//        }

//        isJumping = false;
//    }

//    void HandleWandering()
//    {
//        wanderTimer += Time.deltaTime;
//        bool isAtDestination = !agent.pathPending &&
//                               agent.remainingDistance <= agent.stoppingDistance;
//        if (isAtDestination && wanderTimer >= wanderInterval)
//        {
//            wanderTimer = 0f;
//            Vector3 randomPos = Position.GetRandomPosition(wanderCenterPoint, wanderRadius);

//            // Use the isValidPosition method to check for valid underwater positioning
//            if (Position.isValidPosition(randomPos, waterAreaMask))
//            {
//                Debug.Log("Moving to new underwater position");
//                agent.SetDestination(randomPos);
//            }
//        }
//    }

//    void HandleStanding()
//    {
//        // Optional: Add standing behavior if needed
//    }

//    void TransitionToState(CrabState newState)
//    {
//        currentState = newState;
//    }
//}