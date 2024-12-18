using ExciteOMeter;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CrabController : MonoBehaviour
{

    [Header("Wandering Parameters")]
    protected Transform wanderCenterPoint;
    public int wanderRadius = 150;
    public float wanderInterval = 5f;
    private float wanderTimer = 0f;

    private GameObject river;

    protected NavMeshAgent agent;
    private Rigidbody rb;
    private AttackAreaCollision attackCollision;
    private AppearAreaCollision appearCollision;
    private AppearAndChase appearAndChaseScript;
    private HeartRateManager heartRateManager;
    private GameObject Player;

    //check for change state and the animation
    private bool isAttack = false;
    private bool isJump = false;
    private bool isChasing = false;
    private bool isWandering = true;
    private bool onWater = false;

    private string heartRateManagerName = "HeartrateManager";
    private string playerName = "Player";
    private string waterAreaMaskName = "Water";

    //jump
    private float heightNeedToJump;


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

    void Update()
    {

        //appear
        if (appearCollision.getAppear() == true)
        {
            isJump = true;
            disableAgent();
            Jump();
            ResetJumpStatus();
        }
        else
        {
            isJump = false;
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
                isJump = true;
                disableAgent();
                Jump();
                ResetJumpStatus();
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

    void Jump()
    {
        heightNeedToJump = river.transform.position.y - gameObject.transform.position.y;

        rb.AddForce(new Vector3(0, heightNeedToJump + 1, 0), ForceMode.Impulse);

    }

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

    void Chase()
    {
        if (agent != null && agent.enabled == true && agent.isOnNavMesh == true)
        {
            agent.SetDestination(Player.transform.position);
            Debug.Log(Player.transform.position);
        }

    }

    private void ResetJumpStatus()
    {
        if (isOnWater())
        {
            appearCollision.setOnWater(true);
            appearCollision.setAppear(false);
            enableAgent();
            isJump = false;
            onWater = true;
        }
    }

    private bool isOnWater()
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

    private void enableAgent()
    {
        if (agent != null)
        {
            rb.isKinematic = true;
            agent.enabled = true;
        }
    }

    private void disableAgent()
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

    public bool getIsJump()
    {
        return isJump;
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