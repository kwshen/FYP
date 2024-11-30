using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem.Android;

public class Chase : MonoBehaviour
{
    public NavMeshAgent agent;
    public GameObject Player;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Player != null)
        {
            agent.SetDestination(Player.transform.position);
        }
    }
}
