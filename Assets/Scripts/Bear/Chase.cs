using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Chase : MonoBehaviour
{
    public NavMeshAgent agent;
    List<Transform> targets;
    Transform nearestTarget;
    int waterAreaMask;
    ChaseArea chaseArea;

    // Start is called before the first frame update
    void Start()
    {
        waterAreaMask = 5 << NavMesh.GetAreaFromName("Water");
        chaseArea = GetComponentInChildren<ChaseArea>();
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        targets = chaseArea.getTargetsInRange();

        UpdateNearestTarget(targets);

        // Chase the nearest target if one exists
        if (nearestTarget != null && Position.isValidPosition(transform.position, waterAreaMask) && chaseArea.HasTargets())
        {
            agent.SetDestination(nearestTarget.position);
        }
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

        nearestTarget = closestTarget;  // Update the nearest target
    }
}
