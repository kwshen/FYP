using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnBear : MonoBehaviour
{
    public GameObject bear;
    int spawnRadius = 100;
    string tagName = "SpawnPoint";
    GameObject[] SpawnPointInScene;
    int[] bearCount;
    int maxBearInArea = 2;


    // Start is called before the first frame update
    void Start()
    {

        // get the total actual spawn point
        SpawnPointInScene = GameObject.FindGameObjectsWithTag(tagName);

        // null checking
        if (SpawnPointInScene == null)
        {
            Debug.Log("Spawn point in scene not found");
            return;
        }

        bearCount = new int[SpawnPointInScene.Length];

        for (int i = 0; i < SpawnPointInScene.Length; i++)
        {
            for (int j = 0; bearCount[i] < maxBearInArea; j++)
            {
                Instantiate(bear, Position.GetRandomPosition(SpawnPointInScene[i].transform, spawnRadius), Quaternion.identity);
                bearCount[i]++;
            }
        }

    }

    //public Vector3 GetRandomPosition(Transform centerPoint, int radius)
    //{
    //    // Get a random angle (0 to 2π)
    //    float angle = Random.Range(0f, 2f * Mathf.PI);

    //    // Get a random radius (between 0 and the full radius of the circle)
    //    float randomRadius = Random.Range(0f, radius);

    //    // Convert polar coordinates to Cartesian coordinates
    //    float x = centerPoint.position.x + randomRadius * Mathf.Cos(angle);
    //    float z = centerPoint.position.z + randomRadius * Mathf.Sin(angle);
    //    // Return the random point in world space
    //    return new Vector3(x, centerPoint.position.y, z);
    //}

    public GameObject[] getSpawnPoint()
    {
        return SpawnPointInScene;
    }

}


