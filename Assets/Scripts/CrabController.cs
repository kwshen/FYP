using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrabController : MonoBehaviour
{
    public enum CrabState { Standing, Wandering, Chasing }

    [Header("Behavior Settings")]
    public bool isWanderingCrab = true; // Toggle to determine monster type
    public CrabState currentState = CrabState.Standing;

    [Header("Wandering Parameters")]
    public Transform wanderCenterPoint;
    public int wanderRadius = 150;
    public float wanderInterval = 5f; // Time between wandering attempts
    private float wanderTimer = 0f;

    private UnityEngine.AI.NavMeshAgent agent;
    private ChaseArea chaseArea;
    private int waterAreaMask;
    private Transform nearestTarget;

    void Start()
    {
        wanderCenterPoint = transform;
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        chaseArea = GetComponentInChildren<ChaseArea>();
        waterAreaMask = 5 << UnityEngine.AI.NavMesh.GetAreaFromName("Water");

        // Set initial state based on monster type
        currentState = isWanderingCrab ? CrabState.Wandering : CrabState.Standing;
    }

    void Update()
    {
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
        }

    }

    void HandleStanding()
    {

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
                Debug.Log("going new underwater pos");
                agent.SetDestination(randomPos);

                // Small chance of transitioning to another state (if applicable)
                if (Random.Range(0f, 1f) < 0.01f)
                {
                    //TransitionToState(CrabState.Sleeping);
                }
            }
        }
    }

    //void HandleChasing()
    //{

    //    if (Position.isValidPosition(transform.position, waterAreaMask))
    //    {
    //        agent.SetDestination(nearestTarget.position);
    //    }
    //    else
    //    {
    //        TransitionToState(BearState.Wandering);
    //    }
    //}

    void TransitionToState(CrabState newState)
    {

        switch (newState)
        {
            case CrabState.Wandering:
                break;
            case CrabState.Standing:
                break;
                //case BearState.Sleeping:
                //    agent.ResetPath();
                //    break;
                //case BearState.Chasing:
                //    agent.ResetPath();
                //    break;
        }

        currentState = newState;
    }


}





