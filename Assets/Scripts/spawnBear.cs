using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnBear : MonoBehaviour
{
    public GameObject bear;
    float radius = 141.0f;
    List<GameObject> spawnPointList = new List<GameObject>();
    string tagName = "SpawnPoint";
    GameObject[] SpawnPointInScene;
    

    // Start is called before the first frame update
    void Start()
    {
        

        // get the total actual spawn point
        SpawnPointInScene = GameObject.FindGameObjectsWithTag(tagName);
        
        // null checking
        if(SpawnPointInScene == null)
        {
            Debug.Log("Spawn point in scene not found");
            return;
        }

        int[] bearCount = new int[SpawnPointInScene.Length];

        for (int i = 0; i < SpawnPointInScene.Length; i++)
        {
            if (bearCount[i] < 3)
            {
                Instantiate(bear, GetRandomPosition(SpawnPointInScene[i]), Quaternion.identity);
                bearCount[i]++;
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Vector3 GetRandomPosition(GameObject spawnPointCenter)
    {
        // Get a random angle (0 to 2π)
        float angle = Random.Range(0f, 2f * Mathf.PI);

        // Get a random radius (between 0 and the full radius of the circle)
        float randomRadius = Random.Range(0f, radius);

        // Convert polar coordinates to Cartesian coordinates
        float x = spawnPointCenter.transform.position.x + randomRadius * Mathf.Cos(angle);
        float z = spawnPointCenter.transform.position.z + randomRadius * Mathf.Sin(angle);

        // Return the random point in world space
        return new Vector3(x, spawnPointCenter.transform.position.y, z);
    }
}
