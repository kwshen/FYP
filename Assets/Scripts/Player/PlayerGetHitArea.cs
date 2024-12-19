using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGetHitArea : MonoBehaviour
{
    private string monsterWeaponPartTag = "MonsterWeaponPart";
    PlayerController playerControllerScript;

    // Start is called before the first frame update
    void Start()
    {
        playerControllerScript = GetComponentInParent<PlayerController>();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(monsterWeaponPartTag))
        {
            playerControllerScript.setIsPlayerDie(true);
        }
    }
}
