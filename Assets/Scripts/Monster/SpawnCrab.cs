using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCrab : MonoBehaviour
{
    public GameObject crabPrefab;
    public int spawnRadius = 100;
    string tagName = "SpawnCrabPoint";
    public int maxCrabsInArea = 8;

    [Header("Monster Spawn Configuration")]
    public int wanderingCrabCount = 1;  // Number of wandering Crabs per spawn point
    public int standingCrabCount = 1;   // Number of standing Crabs per spawn point

    [Header("Scale Configuration")]
    public bool randomizeScale = true;
    public float minScale = 0.8f;
    public float maxScale = 1.2f;

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

            // Apply random scale if enabled
            if (randomizeScale)
            {
                float randomScale = Random.Range(minScale, maxScale);
                spawnedCrab.transform.localScale = Vector3.one * randomScale;
            }

            crabCount[spawnIndex]++;
        }
    }

    public GameObject[] GetSpawnPoints()
    {
        return spawnPointsInScene;
    }
}