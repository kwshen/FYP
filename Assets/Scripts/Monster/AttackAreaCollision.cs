using UnityEngine;

public class AttackAreaCollision : MonoBehaviour
{

    string playerTag = "Player";

    bool attack = false;
    bool attackSuccess = false;
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        

        if (attackSuccess == false && other.CompareTag(playerTag))
        {
            attack = true;
        }
        //if attack area no collide with player
        else if (!other.CompareTag(playerTag) || attackSuccess == true)
        {
            attack = false;
        }
    }

    public bool getAttack()
    {
        return attack;
    }

    public void setAttackSuccess(bool attackSuccess)
    {
        this.attackSuccess = attackSuccess;
    }

    public bool getAttackSuccess()
    {
        return attackSuccess;
    }
}
