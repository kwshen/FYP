using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMonster : MonoBehaviour
{
    public GameObject crabPrefab;
    public int spawnRadius = 100;
    string tagName = "SpawnCrabPoint";
    public int maxCrabsInArea = 8;

    [Header("Monster Spawn Configuration")]
    public int wanderingCrabCount = 1;  // Number of wandedring Crabs per spawn point
    public int standingCrabCount = 1;   // Number of standing Crabs per spawn point

    private GameObject[] spawnPointsInScene;
    private int[] crabCount;

    void Start()
    {
        // Get the total actual spawn points
        spawnPointsInScene = GameObject.FindGameObjectsWithTag(tagName);

        // Null checking
        if (spawnPointsInScene == null || spawnPointsInScene.Length == 0)
        {
            Debug.Log("Spawn points in scene not found");
            return;
        }

        crabCount = new int[spawnPointsInScene.Length];

        // Spawn monsters for each spawn point
        for (int i = 0; i < spawnPointsInScene.Length; i++)
        {
            SpawnCrabsAtPoint(spawnPointsInScene[i], i);
        }
    }

    void SpawnCrabsAtPoint(GameObject spawnPoint, int spawnIndex)
    {
        // Spawn wandering Crabs
        for (int j = 0; j < wanderingCrabCount && crabCount[spawnIndex] < maxCrabsInArea; j++)
        {
            GameObject spawnedCrab = Instantiate(
                crabPrefab,
                Position.GetRandomPosition(spawnPoint.transform, spawnRadius),
                Quaternion.identity
            );

            crabCount[spawnIndex]++;
        }
    }

    public GameObject[] GetSpawnPoints()
    {
        return spawnPointsInScene;
    }
}


//========================================================================================================================================

//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class SpawnMonster : MonoBehaviour
//{
//    public GameObject crabPrefab;
//    public int spawnRadius = 100;
//    public int playerSpawnRadius = 50; // Radius around the player within which monsters will spawn
//    public int maxCrabsInArea = 8;

//    [Header("Monster Spawn Configuration")]
//    public int wanderingCrabCount = 1;  // Number of wandering Crabs per spawn point
//    public int standingCrabCount = 1;   // Number of standing Crabs per spawn point

//    private GameObject[] spawnPointsInScene;
//    private int[] crabCount;
//    private GameObject player;
//    private List<GameObject> spawnedCrabs = new List<GameObject>();

//    void Start()
//    {
//        // Get the player GameObject
//        player = GameObject.FindGameObjectWithTag("Player");

//        // Get the total actual spawn points
//        spawnPointsInScene = GameObject.FindGameObjectsWithTag("SpawnCrabPoint");

//        // Null checking
//        if (spawnPointsInScene == null || spawnPointsInScene.Length == 0)
//        {
//            Debug.Log("Spawn points in scene not found");
//            return;
//        }

//        crabCount = new int[spawnPointsInScene.Length];

//        // Spawn monsters for each spawn point
//        for (int i = 0; i < spawnPointsInScene.Length; i++)
//        {
//            float distanceToPlayer = Vector3.Distance(spawnPointsInScene[i].transform.position, player.transform.position);

//            // Check if the spawn point is within the player spawn radius
//            if (distanceToPlayer <= playerSpawnRadius)
//            {
//                SpawnCrabsAtPoint(spawnPointsInScene[i], i);
//            }
//        }
//    }

//    void Update()
//    {
//        // Check for each spawned crab if the player has moved outside the spawn area
//        for (int i = 0; i < spawnPointsInScene.Length; i++)
//        {
//            float distanceToPlayer = Vector3.Distance(spawnPointsInScene[i].transform.position, player.transform.position);

//            // If player moves out of the spawn radius, destroy spawned crabs
//            if (distanceToPlayer > playerSpawnRadius)
//            {
//                DestroyCrabsAtPoint(i);
//            }
//        }
//    }

//    void SpawnCrabsAtPoint(GameObject spawnPoint, int spawnIndex)
//    {
//        // Spawn wandering Crabs
//        for (int j = 0; j < wanderingCrabCount && crabCount[spawnIndex] < maxCrabsInArea; j++)
//        {
//            GameObject spawnedCrab = Instantiate(
//                crabPrefab,
//                Position.GetRandomPosition(spawnPoint.transform, spawnRadius),
//                Quaternion.identity
//            );

//            // Set as wandering Crab
//            CrabController crabController = spawnedCrab.GetComponent<CrabController>();
//            if (crabController != null)
//            {
//                crabController.isWanderingCrab = true;
//            }

//            crabCount[spawnIndex]++;
//            spawnedCrabs.Add(spawnedCrab);
//        }

//        // Spawn standing Crabs
//        for (int j = 0; j < standingCrabCount && crabCount[spawnIndex] < maxCrabsInArea; j++)
//        {
//            GameObject spawnedCrab = Instantiate(
//                crabPrefab,
//                Position.GetRandomPosition(spawnPoint.transform, spawnRadius),
//                Quaternion.identity
//            );

//            // Set as standing Crab
//            CrabController crabController = spawnedCrab.GetComponent<CrabController>();
//            if (crabController != null)
//            {
//                crabController.isWanderingCrab = false;
//            }

//            crabCount[spawnIndex]++;
//            spawnedCrabs.Add(spawnedCrab);
//        }
//    }

//    void DestroyCrabsAtPoint(int spawnIndex)
//    {
//        for (int i = spawnedCrabs.Count - 1; i >= 0; i--)
//        {
//            GameObject crab = spawnedCrabs[i];
//            float distanceToSpawnPoint = Vector3.Distance(crab.transform.position, spawnPointsInScene[spawnIndex].transform.position);

//            if (distanceToSpawnPoint <= spawnRadius)
//            {
//                Destroy(crab);
//                spawnedCrabs.RemoveAt(i);
//                crabCount[spawnIndex]--;
//                Debug.Log("Destroyed crab at spawn point " + spawnIndex);
//            }
//        }
//    }

//    public GameObject[] GetSpawnPoints()
//    {
//        return spawnPointsInScene;
//    }
//}







//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class SpawnMonster : MonoBehaviour
//{
//    public GameObject crabPrefab;
//    public int spawnRadius = 100;
//    public string tagName = "SpawnCrabPoint";
//    public int maxCrabsInArea = 8;

//    private GameObject[] spawnPointsInScene;
//    private int[] crabCount;

//    void Start()
//    {
//        // Get the total actual spawn points
//        spawnPointsInScene = GameObject.FindGameObjectsWithTag(tagName);

//        // Null checking
//        if (spawnPointsInScene == null || spawnPointsInScene.Length == 0)
//        {
//            Debug.Log("Spawn points in scene not found");
//            return;
//        }

//        crabCount = new int[spawnPointsInScene.Length];

//        // Spawn monsters for each spawn point
//        for (int i = 0; i < spawnPointsInScene.Length; i++)
//        {
//            SpawnCrabsAtPoint(spawnPointsInScene[i], i);
//        }
//    }

//    void SpawnCrabsAtPoint(GameObject spawnPoint, int spawnIndex)
//    {
//        for (int j = 0; j < maxCrabsInArea; j++)
//        {
//            if (crabCount[spawnIndex] >= maxCrabsInArea)
//                break;

//            GameObject spawnedCrab = Instantiate(
//                crabPrefab,
//                Position.GetRandomPosition(spawnPoint.transform, spawnRadius),
//                Quaternion.identity
//            );

//            crabCount[spawnIndex]++;
//        }
//    }

//    public GameObject[] GetSpawnPoints()
//    {
//        return spawnPointsInScene;
//    }
//}



//=======================================================================================================================================


//using ExciteOMeter;
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class SpawnMonster : MonoBehaviour
//{
//    public GameObject crabPrefab;
//    public int spawnRadius = 100;
//    public string tagName = "SpawnCrabPoint";
//    public int maxCrabsInArea = 8;

//    // References to be passed to each spawned crab
//    private MonsterCollision attackCollision;
//    private MonsterCollision appearCollision;
//    private HeartRateManager heartRateManager;
//    private GameObject player;

//    private GameObject[] spawnPointsInScene;
//    private int[] crabCount;

//    void Start()
//    {
//        // Find references once
//        attackCollision = GameObject.Find("attackArea")?.GetComponent<MonsterCollision>();
//        appearCollision = GameObject.Find("appearArea")?.GetComponent<MonsterCollision>();
//        heartRateManager = GameObject.Find("HeartrateManager")?.GetComponent<HeartRateManager>();
//        player = GameObject.Find("Kayak");

//        // Validate references
//        if (attackCollision == null || appearCollision == null ||
//            heartRateManager == null || player == null)
//        {
//            Debug.LogError("One or more required references are missing!");
//            return;
//        }

//        // Get the total actual spawn points
//        spawnPointsInScene = GameObject.FindGameObjectsWithTag(tagName);

//        // Null checking
//        if (spawnPointsInScene == null || spawnPointsInScene.Length == 0)
//        {
//            Debug.LogError("Spawn points in scene not found");
//            return;
//        }

//        crabCount = new int[spawnPointsInScene.Length];

//        // Spawn monsters for each spawn point
//        for (int i = 0; i < spawnPointsInScene.Length; i++)
//        {
//            SpawnCrabsAtPoint(spawnPointsInScene[i], i);
//        }
//    }

//    void SpawnCrabsAtPoint(GameObject spawnPoint, int spawnIndex)
//    {
//        for (int j = 0; j < maxCrabsInArea; j++)
//        {
//            if (crabCount[spawnIndex] >= maxCrabsInArea)
//                break;

//            // Instantiate the crab
//            GameObject spawnedCrab = Instantiate(
//                crabPrefab,
//                Position.GetRandomPosition(spawnPoint.transform, spawnRadius),
//                Quaternion.identity
//            );

//            // Get the CrabController component
//            CrabController crabController = spawnedCrab.GetComponent<CrabController>();

//            // If the method exists, pass the references
//            if (crabController != null)
//            {
//                crabController.SetReferences(
//                    attackCollision,
//                    appearCollision,
//                    heartRateManager,
//                    player
//                );
//            }

//            crabCount[spawnIndex]++;
//        }
//    }

//    public GameObject[] GetSpawnPoints()
//    {
//        return spawnPointsInScene;
//    }
//}