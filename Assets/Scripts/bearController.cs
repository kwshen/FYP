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
    int waterAreaMask;

    // Start is called before the first frame update
    void Start()
    {
        waterAreaMask = 5 << NavMesh.GetAreaFromName("Water");
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
                InvokeRepeating("wanderInArea", 0f, wanderInterval);
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
        //check if isSleeping
        if (isSleeping)
        {
            CancelInvoke("wanderInArea");

            return;
        }

        Vector3 randomPos = Position.GetRandomPosition(wanderCenterPoint, wanderRadius);  // Get a random position within the area

        // check if valid
        if (Position.isValidPosition(randomPos, waterAreaMask))
        {

            agent.SetDestination(randomPos);  // Move the agent to the random position


            if (Random.Range(0f, 1f) < 0.1f)  // 20% chance to sleep after wandering
            {
                CancelInvoke("wanderInArea");

                isSleeping = true;

            }
        }


    }


}
