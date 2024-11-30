using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.AI;

public class BearController : MonoBehaviour
{
    Transform wanderCenterPoint;
    NavMeshAgent agent;
    int wanderRadius = 150;
    float wanderInterval = 20.0f;
    public bool isSleeping = false;
    float sleepTime = 10.0f;
    float sleepTimer = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        wanderCenterPoint = transform;
        agent = GetComponent<NavMeshAgent>();
        InvokeRepeating("wanderInArea", 0f, wanderInterval);
    }

    // Update is called once per frame
    void Update()
    {
        if (isSleeping)
        {
            sleepTimer += Time.deltaTime; // Increase the sleep timer

            if (sleepTimer >= sleepTime)
            {
                isSleeping = false;   // After sleep time, wake up
                Debug.Log(isSleeping);

                sleepTimer = 0f;       // Reset the sleep timer 
            }
        }
        else
        {
            wanderInArea();      // Start wandering again
        }
    }

    void wanderInArea()
    {
        Vector3 randomPos = Position.GetRandomPosition(wanderCenterPoint, wanderRadius);  // Get a random position within the area
        if (agent.isOnNavMesh)  // Ensure the position is valid on the NavMesh
        {
            agent.SetDestination(randomPos);  // Move the agent to the random position
        }

        if (Random.Range(0f, 1f) < 0.1f)  // 20% chance to sleep after wandering
        {
            isSleeping = true;
            Debug.Log("Is sleeping");
        }
    }

    //public Vector3 GetRandomPosition(Transform centerPoint, int radius)
    //{
    //    // Get a random angle (0 to 2π)
    //    float angle = Random.Range(0f, 2f * Mathf.PI);

    //    // Get a random radius (between 0 and the full radius of the circle)
    //    float randomRadius = Random.Range(0f, radius);

    //    // Convert polar coordinates to Cartesian coordinates
    //    float x = centerPoint.position.x + randomRadius * Mathf.Cos(angle);
    //    float z = centerPoint.position.z + randomRadius * Mathf.Sin(angle);
    //    // Return the random point in world space
    //    return new Vector3(x, centerPoint.position.y, z);
    //}
}
