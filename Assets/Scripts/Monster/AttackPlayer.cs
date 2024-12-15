using UnityEngine;

public class AttackPlayer : MonoBehaviour
{
    private string playerGetHitAreaTag = "PlayerGetHitArea";
    MonsterCollision[] monsterCollision;

    private void Start()
    {
        monsterCollision = gameObject.transform.root.GetComponentsInChildren<MonsterCollision>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerGetHitAreaTag))
        {
            for(int i = 0; i < monsterCollision.Length; i++)
            {
                monsterCollision[i].setAttackSuccess(true);
            }
        }
    }

}
