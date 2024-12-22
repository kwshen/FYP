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