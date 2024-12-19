using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSardine : MonoBehaviour
{
    public GameObject sardinePrefab;
    string tagName = "SpawnCrabPoint";
    public int spawnRadius = 3;

    private GameObject[] spawnPointsInScene;
    private int[] sardineCount;

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


        // Spawn monsters for each spawn point
        for (int i = 0; i < spawnPointsInScene.Length; i++)
        {
            GameObject spawnedCrab = Instantiate(
                sardinePrefab,
                Position.GetRandomPosition(spawnPointsInScene[i].transform, spawnRadius),
                Quaternion.identity
            );
        }
    }
}
