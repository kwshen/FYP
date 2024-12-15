using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGetHitArea : MonoBehaviour
{
    private string monsterWeaponPartTag = "MonsterWeaponPart";
    PlayerManager playerManager;

    // Start is called before the first frame update
    void Start()
    {
        playerManager = GetComponentInParent<PlayerManager>();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(monsterWeaponPartTag))
        {
            playerManager.setIsPlayerDie(true);
        }
    }
}
