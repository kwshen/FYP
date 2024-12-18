using ExciteOMeter;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class CrabController : MonoBehaviour
{

    [Header("Wandering Parameters")]
    protected Transform wanderCenterPoint;
    public int wanderRadius = 150;
    public float wanderInterval = 5f;
    private float wanderTimer = 0f;

    protected GameObject river;

    protected NavMeshAgent agent;
    protected Rigidbody rb;
    private AttackAreaCollision attackCollision;
    protected AppearAreaCollision appearCollision;
    private AppearAndChase appearAndChaseScript;
    private HeartRateManager heartRateManager;
    private GameObject Player;

    //check for change state and the animation
    private bool isAttack = false;
    protected bool isSpecialMoving = false;
    private bool isChasing = false;
    private bool isWandering = true;
    protected bool onWater = false;

    private string heartRateManagerName = "HeartrateManager";
    private string playerName = "Player";
    private string waterAreaMaskName = "Water";

    //jump
    protected float heightNeedToJump;


    protected void Start()
    {

        river = GameObject.FindGameObjectWithTag(waterAreaMaskName);

        wanderCenterPoint = gameObject.transform;
        agent = gameObject.GetComponent<NavMeshAgent>();
        rb = gameObject.GetComponent<Rigidbody>();
        attackCollision = gameObject.GetComponentInChildren<AttackAreaCollision>();
        appearCollision = gameObject.GetComponentInChildren<AppearAreaCollision>();
        appearAndChaseScript = gameObject.GetComponentInChildren<AppearAndChase>();
        heartRateManager = GameObject.FindGameObjectWithTag(heartRateManagerName).GetComponent<HeartRateManager>();
        Player = GameObject.FindGameObjectWithTag(playerName);

        agent.areaMask = NavMesh.AllAreas;

        // Disable rigidbody physics initially if using NavMesh
        if (rb != null)
        {
            rb.isKinematic = true;
        }

    }

    protected void Update()
    {

        //appear
        if (appearCollision.getAppear() == true)
        {
            isSpecialMoving = true;
            disableAgent();
            specialMove();
            ResetStatus();
        }
        else
        {
            isSpecialMoving = false;
        }

        //attack
        if (attackCollision.getAttack() == true)
        {
            agent.enabled = false;
            isAttack = true;
        }
        else
        {
            agent.enabled = true;
            isAttack = false;
        }


        //chasing
        //if (heartRateManager.getHeartrate() >= 85)
        if (85 >= 85 && isAttack == false && appearAndChaseScript.getAppearAndChase() == true)
        {
            isChasing = true;
            if (onWater == false)
            {
                isSpecialMoving = true;
                disableAgent();
                specialMove();
                ResetStatus();
            }

            Chase();
        }
        else
        {
            isChasing = false;
        }

        //if is wander crab and is not chasing
        if (/*isWanderingCrab == true && */ isChasing == false && isAttack == false)
        {
            isWandering = true;
            Wander();
        }
        else
        {
            isWandering = false;
        }

    }

    protected abstract void specialMove();
    protected void Wander()
    {
        wanderTimer += Time.deltaTime;
        bool isAtDestination;

        if (agent != null && agent.enabled == true)
        {
            isAtDestination = !agent.pathPending && agent.remainingDistance <= agent.stoppingDistance;
            if (isAtDestination && wanderTimer >= wanderInterval)
            {
                wanderTimer = 0f;
                Vector3 randomPos = Position.GetRandomPosition(wanderCenterPoint, wanderRadius);

                
                agent.SetDestination(randomPos);
                
            }
        }
    }

    private void Chase()
    {
        if (agent != null && agent.enabled == true && agent.isOnNavMesh == true)
        {
            agent.SetDestination(Player.transform.position);
            Debug.Log(Player.transform.position);
        }

    }

    protected abstract void ResetStatus();

    protected bool isOnWater()
    {
        // Check if the monster has reached the water level
        if (transform.position.y >= river.transform.position.y - 0.1 && transform.position.y <= river.transform.position.y + 10)
        {
            return true;
        }
        else
        {
            // If not on water yet, check again after a short delay
            Invoke("isOnWater", 0.1f);
        }
        return false;
    }

    protected void enableAgent()
    {
        if (agent != null)
        {
            rb.isKinematic = true;
            agent.enabled = true;
        }
    }

    protected void disableAgent()
    {
        if (agent != null)
        {
            rb.isKinematic = false;
            agent.enabled = false;
        }
    }

    public bool getIsAttack()
    {
        return isAttack;
    }

    public bool getIsSpecialMoving()
    {
        return isSpecialMoving;
    }

    public bool getIsChasing()
    {
        return isChasing;
    }

    public bool getIsWandering()
    {
        return isWandering;
    }


}