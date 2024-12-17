using UnityEngine;

public class AttackPlayer : MonoBehaviour
{
    private string playerGetHitAreaTag = "PlayerGetHitArea";
    AttackAreaCollision attackAreaCollision;

    private void Start()
    {
        attackAreaCollision = gameObject.transform.root.GetComponentInChildren<AttackAreaCollision>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerGetHitAreaTag))
        {
            
                //monsterCollision[i].setAttackSuccess(true);
                attackAreaCollision.setAttackSuccess(true);
            
        }
    }

}
