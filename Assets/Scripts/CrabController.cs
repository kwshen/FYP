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
//    public float jumpForce = 5f;
//    public float jumpAngle = 60f;
//    public float jumpHeight = 2f;
//    public float jumpCooldown = 2f;
//    private float lastJumpTime;

//    [Header("Wandering Parameters")]
//    public Transform wanderCenterPoint;
//    public int wanderRadius = 150;
//    public float wanderInterval = 5f;
//    private float wanderTimer = 0f;

//    private NavMeshAgent agent;
//    private ChaseArea chaseArea;
//    private Rigidbody rb;
//    private int waterAreaMask;
//    private Transform nearestTarget;
//    private MonsterCollision[] monsterCollision;

//    void Start()
//    {
//        wanderCenterPoint = transform;
//        agent = GetComponent<NavMeshAgent>();
//        rb = GetComponent<Rigidbody>();
//        monsterCollision = GetComponentsInChildren<MonsterCollision>();
//        chaseArea = GetComponentInChildren<ChaseArea>();

//        waterAreaMask = 1 << NavMesh.GetAreaFromName("Water");

//        // Set initial state based on monster type
//        currentState = isWanderingCrab ? CrabState.Wandering : CrabState.Standing;

//        // Disable rigidbody physics initially if using NavMesh
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
//            if (monsterCollision[i] != null && monsterCollision[i].getAppear())
//            {
//                TryJumpOntoRiver();
//                break;
//            }
//        }

//        //if (monsterCollision != null && monsterCollision.getAppear())
//        //{

//        //}

//        //Debug.Log(monsterCollision.getAppear());

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
//            TransitionToState(CrabState.Jumping);
//        }
//    }

//    void HandleJumping()
//    {
//        // Ensure we have a Rigidbody component
//        if (rb == null) return;

//        // Temporarily disable NavMesh agent
//        if (agent != null)
//        {
//            agent.enabled = false;
//        }

//        // Kinematic physics during jump
//        rb.isKinematic = false;

//        // Calculate jump direction and force
//        Vector3 jumpDirection = CalculateJumpTrajectory();
//        rb.AddForce(jumpDirection, ForceMode.Impulse);

//        // Mark last jump time
//        lastJumpTime = Time.time;

//        // Transition back to wandering after jump
//        StartCoroutine(ResetAfterJump());
//    }

//    Vector3 CalculateJumpTrajectory()
//    {
//        // Calculate jump vector based on angle and force
//        Vector3 jumpVector = transform.forward * Mathf.Cos(jumpAngle * Mathf.Deg2Rad) * jumpForce;
//        jumpVector += Vector3.up * Mathf.Sin(jumpAngle * Mathf.Deg2Rad) * jumpForce;

//        return jumpVector;
//    }

//    IEnumerator ResetAfterJump()
//    {
//        // Wait for jump to complete
//        yield return new WaitForSeconds(1f);

//        // Re-enable NavMesh agent
//        if (agent != null)
//        {
//            agent.enabled = true;
//            rb.isKinematic = true;
//        }

//        // Transition back to previous state or wandering
//        TransitionToState(CrabState.Wandering);
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
//        // State transition logic
//        currentState = newState;
//    }
//}



//===================================================================================================================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CrabController : MonoBehaviour
{
    public enum CrabState { Standing, Wandering, Chasing, Jumping, Attacking }

    [Header("Behavior Settings")]
    public bool isWanderingCrab = true;
    public CrabState currentState = CrabState.Standing;

    [Header("Wandering Parameters")]
    public Transform wanderCenterPoint;
    public int wanderRadius = 150;
    public float wanderInterval = 5f;
    private float wanderTimer = 0f;

    private GameObject river;

    private NavMeshAgent agent;
    private ChaseArea chaseArea;
    private Rigidbody rb;
    private int waterAreaMask;
    private Transform nearestTarget;
    private MonsterCollision[] monsterCollision;

    private bool isAttack = false;
    private bool isJump = false;



    void Start()
    {

        river = GameObject.FindGameObjectWithTag("Water");

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
            if (monsterCollision[i] != null && monsterCollision[i].getAppear() && currentState != CrabState.Jumping)
            {
                TransitionToState(CrabState.Jumping);
                break;
            }
            else
            {
                isJump = false;
            }

            //attackSuccess default is false, !attackSuccess == true
            if (monsterCollision[i].getAttack() == true && monsterCollision[i].getAttackSuccess() == false)
            {
                isAttack = true;
                break;
            }
            else
            {
                isAttack = false;
            }

        }



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

    void HandleJumping()
    {
        float heightNeedToJump = river.transform.position.y - gameObject.transform.position.y;
        
        foreach (var collision in monsterCollision)
        {
            if (collision != null && collision.getAppear())
            {
                isJump = true;
                agent.enabled = false;
                rb.isKinematic = false;
                rb.AddForce(new Vector3(0, heightNeedToJump + 1, 0), ForceMode.Impulse);
                Invoke("CheckIfOnWater", Mathf.Sqrt(2 * heightNeedToJump / Physics.gravity.magnitude) / 3);
                break;
            }
        }

        //TransitionToState(CrabState.Standing);
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

        //TransitionToState(CrabState.Standing);
    }

    void HandleStanding()
    {
        // Optional: Add standing behavior if needed
        //TransitionToState(CrabState.Wandering);
    }

    void TransitionToState(CrabState newState)
    {
        // State transition logic
        currentState = newState;
    }

    private void CheckIfOnWater()
    {
        // Check if the monster has reached the water level
        if (transform.position.y >= river.transform.position.y - 0.1 && transform.position.y <= river.transform.position.y + 10)
        {

            ResetCrabSettings();
            isJump = false;
        }
        else
        {
            // If not on water yet, check again after a short delay
            Invoke("CheckIfOnWater", 0.1f);
        }
    }

    private void ResetCrabSettings()
    {
        for (int i = 0; i < monsterCollision.Length; i++)
        {
            monsterCollision[i].setOnWater(true);
            monsterCollision[i].setAppear(false);
        }
        rb.isKinematic = true;
        agent.enabled = true;
    }

    public bool getIsAttack()
    {
        return isAttack;
    }
    public bool getIsJump()
    {
        return isJump;
    }

}


//=====================================================================================================================================
