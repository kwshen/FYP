using System.Collections.Generic;
using UnityEngine;

public class ChaseArea : MonoBehaviour
{
    private List<Transform> targetsInRange = new List<Transform>();
    string deerTag = "Deer";
    string playerTag = "Player";

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(deerTag) || other.CompareTag(playerTag))
        {
            targetsInRange.Add(other.transform);
        }
        
    }


    void OnTriggerExit(Collider other)
    {
        targetsInRange.Remove(other.transform);
    }

    public List<Transform> getTargetsInRange()
    {
        return targetsInRange;
    }

    public bool HasTargets()
    {
        return targetsInRange.Count > 0; // Check if there are any targets in range
    }
}
