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
}