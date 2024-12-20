//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class SpawnSardine : MonoBehaviour
//{
//    public GameObject sardinePrefab;
//    string tagName = "SpawnCrabPoint";
//    public int spawnRadius = 3;

//    private GameObject[] spawnPointsInScene;
//    private int[] sardineCount;

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


//        // Spawn monsters for each spawn point
//        for (int i = 0; i < spawnPointsInScene.Length; i++)
//        {
//            GameObject spawnedCrab = Instantiate(
//                sardinePrefab,
//                Position.GetRandomPosition(spawnPointsInScene[i].transform, spawnRadius),
//                Quaternion.identity
//            );
//        }
//    }
//}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSardine : MonoBehaviour
{
    public GameObject sardinePrefab;
    string tagName = "SpawnCrabPoint";
    public int spawnRadius = 3;
    public int sardinesPerPoint = 10;

    void Start()
    {
        GameObject[] spawnPoints = GameObject.FindGameObjectsWithTag(tagName);

        if (spawnPoints == null || spawnPoints.Length == 0)
        {
            Debug.Log("Spawn points in scene not found");
            return;
        }

        foreach (GameObject point in spawnPoints)
        {
            for (int i = 0; i < sardinesPerPoint; i++)
            {
                Instantiate(
                    sardinePrefab,
                    Position.GetRandomPosition(point.transform, spawnRadius),
                    Quaternion.identity
                );
            }
        }
    }
}