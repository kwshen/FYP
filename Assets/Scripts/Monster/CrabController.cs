using ExciteOMeter;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CrabController : MonoBehaviour
{

    [Header("Behavior Settings")]
    public bool isWanderingCrab = true;

    [Header("Wandering Parameters")]
    public Transform wanderCenterPoint;
    public int wanderRadius = 150;
    public float wanderInterval = 5f;
    private float wanderTimer = 0f;

    private GameObject river;

    private NavMeshAgent agent;
    private Rigidbody rb;
    private int waterAreaMask;
    private MonsterCollision attackCollision;
    private MonsterCollision appearCollision;
    private HeartRateManager heartRateManager;
    private GameObject Player;

    //check for change state and the animation
    private bool isAttack = false;
    private bool isJump = false;
    private bool isChasing = false;
    private bool isWandering = true;
    private bool onWater = false;

    private string heartRateManagerName = "HeartrateManager";
    private string playerName = "Kayak";
    private string appearAreaName = "appearArea";
    private string attackAreaName = "attackArea";
    private string waterAreaMaskName = "Water";
    private Vector3 crabPreviousPosition;

    //jump
    private float heightNeedToJump;


    void Start()
    {

        river = GameObject.FindGameObjectWithTag(waterAreaMaskName);

        wanderCenterPoint = transform;
        agent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
        attackCollision = GameObject.Find(attackAreaName).GetComponent<MonsterCollision>();
        appearCollision = GameObject.Find(appearAreaName).GetComponent<MonsterCollision>();
        heartRateManager = GameObject.Find(heartRateManagerName).GetComponent<HeartRateManager>();
        Player = GameObject.Find(playerName);

        waterAreaMask = 1 << NavMesh.GetAreaFromName(waterAreaMaskName);
        agent.areaMask = NavMesh.AllAreas;

        // Disable rigidbody physics initially if using NavMesh
        if (rb != null)
        {
            rb.isKinematic = true;
        }

        crabPreviousPosition = transform.position;
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
            isAttack = true;
        }
        else
        {
            isAttack = false;
        }


        //chasing
        if (heartRateManager.getHeartrate() >= 85)
        //if (85 >= 85 && isAttack == false)
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
        if (isWanderingCrab == true && isChasing == false)
        {
            isWandering = true;
            Wander();
        }
        else
        {
            isWandering = false;
        }


        crabPreviousPosition = transform.position;
    }

    void Jump()
    {
        heightNeedToJump = river.transform.position.y - gameObject.transform.position.y;

        rb.AddForce(new Vector3(0, heightNeedToJump + 1, 0), ForceMode.Impulse);

    }

    void Wander()
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

                // Use the isValidPosition method to check for valid underwater positioning
                //if (Position.isValidPosition(randomPos, waterAreaMask))
                //{
                Debug.Log("Moving to new underwater position");
                agent.SetDestination(randomPos);
                //}
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

    public bool isMoving()
    {
        //return true when is moving
        return crabPreviousPosition != transform.position;
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