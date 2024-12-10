using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMonster : MonoBehaviour
{
    public GameObject crabPrefab;
    public int spawnRadius = 100;
    public string tagName = "SpawnPoint";
    public int maxCrabsInArea = 2;

    [Header("Monster Spawn Configuration")]
    public int wanderingCrabCount = 1;  // Number of wandering Crabs per spawn point
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

            // Set as wandering Crab
            CrabController crabController = spawnedCrab.GetComponent<CrabController>();
            if (crabController != null)
            {
                crabController.isWanderingCrab = true;
            }

            crabCount[spawnIndex]++;
        }

        // Spawn standing Crabs
        for (int j = 0; j < standingCrabCount && crabCount[spawnIndex] < maxCrabsInArea; j++)
        {
            GameObject spawnedCrab = Instantiate(
                crabPrefab,
                Position.GetRandomPosition(spawnPoint.transform, spawnRadius),
                Quaternion.identity
            );

            // Set as standing monster
            CrabController crabController = spawnedCrab.GetComponent<CrabController>();
            if (crabController != null)
            {
                crabController.isWanderingCrab = false;
            }

            crabCount[spawnIndex]++;
        }
    }

    public GameObject[] GetSpawnPoints()
    {
        return spawnPointsInScene;
    }
}