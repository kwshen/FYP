using UnityEngine;
using UnityEngine.AI;

public class DeerController : MonoBehaviour
{
    public Transform[] wanderPoints; // Array of wander points

    private NavMeshAgent agent;
    private int currentPointIndex = 0; // Current index in the wander points array

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        if (agent == null)
        {
            Debug.LogError("NavMeshAgent is missing on " + gameObject.name);
            enabled = false;
        }

        if (wanderPoints == null || wanderPoints.Length == 0)
        {
            Debug.LogError("Wander points are not assigned on " + gameObject.name);
            enabled = false;
        }
        else
        {
            SetExactDestination(wanderPoints[currentPointIndex].position);
        }
    }

    void Update()
    {
        if (wanderPoints != null && wanderPoints.Length > 0 && agent.remainingDistance <= agent.stoppingDistance)
        {
            // Move to the next point
            currentPointIndex = (currentPointIndex + 1) % wanderPoints.Length;
            SetExactDestination(wanderPoints[currentPointIndex].position);
        }
    }

    private void SetExactDestination(Vector3 targetPosition)
    {
        Vector3 exactPosition = new Vector3(targetPosition.x, transform.position.y, targetPosition.z);
        agent.SetDestination(exactPosition);
    }
}
