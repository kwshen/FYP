using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCrab : MonoBehaviour
{
    public GameObject crabPrefab;
    public int spawnRadius = 100;
    string tagName = "SpawnCrabPoint";
    public int crabsPerPoint = 5;

    [Header("Scale Configuration")]
    public bool randomizeScale = true;
    public float minScale = 0.8f;
    public float maxScale = 1.2f;

    void Start()
    {
        GameObject[] spawnPoints = GameObject.FindGameObjectsWithTag(tagName);

        if (spawnPoints == null || spawnPoints.Length == 0)
        {
            Debug.Log("No spawn points found");
            return;
        }

        foreach (GameObject point in spawnPoints)
        {
            for (int i = 0; i < crabsPerPoint; i++)
            {
                GameObject crab = Instantiate(
                    crabPrefab,
                    Position.GetRandomPosition(point.transform, spawnRadius),
                    Quaternion.identity
                );

                if (randomizeScale)
                {
                    float randomScale = Random.Range(minScale, maxScale);
                    crab.transform.localScale = Vector3.one * randomScale;
                }
            }
        }
    }
}