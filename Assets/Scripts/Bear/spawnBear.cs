//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class spawnBear : MonoBehaviour
//{
//    public GameObject bear;
//    int spawnRadius = 1;
//    string tagName = "SpawnPoint";
//    GameObject[] SpawnPointInScene;
//    int[] bearCount;
//    int maxBearInArea = 2;


//    // Start is called before the first frame update
//    void Start()
//    {

//        // get the total actual spawn point
//        SpawnPointInScene = GameObject.FindGameObjectsWithTag(tagName);

//        // null checking
//        if (SpawnPointInScene == null)
//        {
//            Debug.Log("Spawn point in scene not found");
//            return;
//        }

//        bearCount = new int[SpawnPointInScene.Length];

//        for (int i = 0; i < SpawnPointInScene.Length; i++)
//        {
//            for (int j = 0; bearCount[i] < maxBearInArea; j++)
//            {
//                Instantiate(bear, Position.GetRandomPosition(SpawnPointInScene[i].transform, spawnRadius), Quaternion.identity);
//                bearCount[i]++;
//            }
//        }

//    }

//    public GameObject[] getSpawnPoint()
//    {
//        return SpawnPointInScene;
//    }

//}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnBear : MonoBehaviour
{
    public GameObject bear;
    public float minScale = 0.8f; // Minimum bear scale
    public float maxScale = 1.5f; // Maximum bear scale
    int spawnRadius = 1;
    string tagName = "SpawnPoint";
    GameObject[] SpawnPointInScene;
    int[] bearCount;
    int maxBearInArea = 2;

    // Start is called before the first frame update
    void Start()
    {
        // Get the total actual spawn points
        SpawnPointInScene = GameObject.FindGameObjectsWithTag(tagName);

        // Null checking
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
                // Instantiate the bear
                GameObject spawnedBear = Instantiate(
                    bear,
                    Position.GetRandomPosition(SpawnPointInScene[i].transform, spawnRadius),
                    Quaternion.identity
                );

                // Apply random scaling
                float randomScale = Random.Range(minScale, maxScale);
                spawnedBear.transform.localScale = new Vector3(randomScale, randomScale, randomScale);

                bearCount[i]++;
            }
        }
    }

    public GameObject[] getSpawnPoint()
    {
        return SpawnPointInScene;
    }
}
