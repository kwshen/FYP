using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public static class Position
{
    public static Vector3 GetRandomPosition(Transform centerPoint, int radius)
    {
        //Get a random angle(0 to 2π)
        float angle = Random.Range(0f, 2f * Mathf.PI);

        //Get a random radius(between 0 and the full radius of the circle)
        float randomRadius = Random.Range(0f, radius);

        //Convert polar coordinates to Cartesian coordinates
        float x = centerPoint.position.x + randomRadius * Mathf.Cos(angle);
        float z = centerPoint.position.z + randomRadius * Mathf.Sin(angle);
        //Return the random point in world space
        return new Vector3(x, centerPoint.position.y, z);
    }

    public static bool isValidPosition(Vector3 randomPosition, int avoidAreaMask)
    {
        // Check if the position is valid on the NavMesh and avoids undesired areas
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomPosition, out hit, 1.0f, NavMesh.AllAreas))
        {
            if ((hit.mask & avoidAreaMask) == 0) // Check that it doesn't belong to the avoid area
            {
                randomPosition = hit.position; // Adjust position to the nearest valid NavMesh point
                return true;
            }
            return false;
        }
        return false;
    }
}