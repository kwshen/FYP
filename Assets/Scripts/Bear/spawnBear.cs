using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnBear : MonoBehaviour
{
    public GameObject bear;
    int spawnRadius = 1;
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

    public GameObject[] getSpawnPoint()
    {
        return SpawnPointInScene;
    }

}


