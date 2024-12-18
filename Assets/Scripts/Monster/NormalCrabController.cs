using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalCrabController : CrabController
{
    // Override the base Update method to only handle wandering
    void Update()
    {

        Wander();

    }

    // Override Start method to ensure base initialization
    void Start()
    {
        // Initialize only wandering-specific components
        wanderCenterPoint = gameObject.transform;
        agent = gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>();

        // Set up basic wandering parameters
        agent.areaMask = UnityEngine.AI.NavMesh.AllAreas;
    }

    protected override void specialMove()
    {
        
    }

    protected override void ResetStatus()
    {

    }
}