using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BearController : MonoBehaviour
{
    public enum BearState { Wandering, Chasing, Sleeping, Standing }
    public BearState currentState = BearState.Wandering;

    Transform wanderCenterPoint;
    NavMeshAgent agent;
    int wanderRadius = 150;
    public bool isSleeping = false;
    float sleepTime = 10.0f;
    float sleepTimer = 0.0f;
    ChaseArea chaseArea;
    Transform nearestTarget;
    int waterAreaMask;
    bool isAtDestination;
    public float standDuration = 3f; // Time to stand idle before wandering
    private float standTimer = 0f;  // Timer for standing

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
            case BearState.Wandering:
                HandleWandering();
                break;

            case BearState.Chasing:
                HandleChasing();
                break;

            case BearState.Sleeping:
                HandleSleeping();
                break;
            case BearState.Standing:
                break;
        }
    }


    public float wanderInterval = 5f; // Time between wandering attempts
    private float wanderTimer = 0f;

    void HandleWandering()
    {
        // Increment wander timer
        wanderTimer += Time.deltaTime;

        // Check if the agent has reached the destination or is very close to it
        isAtDestination = !agent.pathPending &&
                               agent.remainingDistance <= agent.stoppingDistance;


        // Only attempt new wandering if enough time has passed
        if (isAtDestination && wanderTimer >= wanderInterval)
        {
            // Reset the timer
            wanderTimer = 0f;

            Vector3 randomPos = Position.GetRandomPosition(wanderCenterPoint, wanderRadius);
            if (Position.isValidPosition(randomPos, waterAreaMask))
            {
                agent.SetDestination(randomPos);

                // Small chance of transitioning to sleeping state
                if (Random.Range(0f, 1f) < 0.01f)
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
                break;
            case BearState.Sleeping:
                agent.ResetPath();
                break;
            case BearState.Chasing:
                agent.ResetPath();
                break;
            case BearState.Standing:
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

