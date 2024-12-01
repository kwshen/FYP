//using System.Collections;
//using System.Collections.Generic;
//using System.Xml.Serialization;
//using UnityEngine;
//using UnityEngine.AI;

//public class BearController : MonoBehaviour
//{
//    Transform wanderCenterPoint;
//    NavMeshAgent agent;
//    int wanderRadius = 150;
//    float wanderInterval = 20.0f;
//    public bool isSleeping = false;
//    float sleepTime = 10.0f;
//    float sleepTimer = 0.0f;
//    int waterAreaMask;

//    // Start is called before the first frame update
//    void Start()
//    {
//        waterAreaMask = 5 << NavMesh.GetAreaFromName("Water");
//        wanderCenterPoint = transform;
//        agent = GetComponent<NavMeshAgent>();
//        InvokeRepeating("wanderInArea", 0f, wanderInterval);
//    }

//    // Update is called once per frame
//    void Update()
//    {
//        if (isSleeping)
//        {
//            sleepTimer += Time.deltaTime; // Increase the sleep timer

//            if (sleepTimer >= sleepTime)
//            {
//                isSleeping = false;   // After sleep time, wake up
//                InvokeRepeating("wanderInArea", 0f, wanderInterval);
//                sleepTimer = 0f;       // Reset the sleep timer 
//            }
//        }
//        else
//        {
//            wanderInArea();      // Start wandering again
//        }
//    }

//    void wanderInArea()
//    {
//        //check if isSleeping
//        if (isSleeping)
//        {
//            CancelInvoke("wanderInArea");
//            return;
//        }

//        Vector3 randomPos = Position.GetRandomPosition(wanderCenterPoint, wanderRadius);  // Get a random position within the area

//        // check if valid
//        if (Position.isValidPosition(randomPos, waterAreaMask))
//        {
//            agent.SetDestination(randomPos);  // Move the agent to the random position

//            if (Random.Range(0f, 1f) < 0.1f)  // 20% chance to sleep after wandering
//            {
//                CancelInvoke("wanderInArea");
//                isSleeping = true;
//            }
//        }
//    }

//}
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BearController : MonoBehaviour
{
    public enum BearState { Idle, Wandering, Chasing, Sleeping }
    public BearState currentState = BearState.Idle;

    Transform wanderCenterPoint;
    NavMeshAgent agent;
    int wanderRadius = 150;
    float wanderInterval = 20.0f;
    float wanderTimer = 0.0f;
    public bool isSleeping = false;
    float sleepTime = 10.0f;
    float sleepTimer = 0.0f;
    ChaseArea chaseArea;
    Transform nearestTarget;
    int waterAreaMask;

    void Start()
    {
        wanderCenterPoint = transform;
        agent = GetComponent<NavMeshAgent>();
        chaseArea = GetComponentInChildren<ChaseArea>();
        waterAreaMask = 5 << NavMesh.GetAreaFromName("Water");
    }

    void Update()
    {
        switch (currentState)
        {
            case BearState.Idle:
                HandleIdle();
                break;

            case BearState.Wandering:
                HandleWandering();
                break;

            case BearState.Chasing:
                HandleChasing();
                break;

            case BearState.Sleeping:
                HandleSleeping();
                break;
        }
    }

    void HandleIdle()
    {
        // If the agent is not moving, stay in Idle state
        if (agent.velocity.magnitude < 0.1f)
        {
            // Check for any targets in range to start chasing
            CheckForChase();
        }
        else
        {
            // If the agent starts moving, transition to Wandering or Chasing
            TransitionToState(BearState.Wandering);
        }
    }

    void HandleWandering()
    {
        wanderTimer += Time.deltaTime;

        if (wanderTimer >= wanderInterval)
        {
            wanderTimer = 0.0f;

            Vector3 randomPos = Position.GetRandomPosition(wanderCenterPoint, wanderRadius);
            if (Position.isValidPosition(randomPos, waterAreaMask))
            {
                agent.SetDestination(randomPos);

                if (Random.Range(0f, 1f) < 0.2f)
                {
                    TransitionToState(BearState.Sleeping);
                }
            }
        }

        CheckForChase();
    }

    void HandleChasing()
    {
        List<Transform> targets = chaseArea.getTargetsInRange();
        UpdateNearestTarget(targets);

        if (nearestTarget != null && Position.isValidPosition(transform.position, waterAreaMask))
        {
            agent.SetDestination(nearestTarget.position);
        }
        else
        {
            TransitionToState(BearState.Wandering);
        }
    }

    void HandleSleeping()
    {
        sleepTimer += Time.deltaTime;
        if (sleepTimer >= sleepTime)
        {
            sleepTimer = 0.0f;
            TransitionToState(BearState.Wandering);
        }
    }

    void CheckForChase()
    {
        List<Transform> targets = chaseArea.getTargetsInRange();
        if (targets.Count > 0)
        {
            TransitionToState(BearState.Chasing);
        }
    }

    void TransitionToState(BearState newState)
    {

        switch (newState)
        {
            case BearState.Wandering:
                wanderTimer = 0.0f;
                break;
            case BearState.Sleeping:
                agent.ResetPath();
                break;
            case BearState.Chasing:
                agent.ResetPath();
                break;
            case BearState.Idle:
                break;
        }
        
        currentState = newState;
    }

    void UpdateNearestTarget(List<Transform> targets)
    {
        float shortestDistance = Mathf.Infinity;
        Transform closestTarget = null;

        foreach (Transform target in targets)
        {
            float distance = Vector3.Distance(transform.position, target.position);
            if (distance < shortestDistance)
            {
                shortestDistance = distance;
                closestTarget = target;
            }
        }

        nearestTarget = closestTarget;
    }


}

