using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private bool isPlayerDie = false;

    public void setIsPlayerDie(bool isPlayerDie)
    {
        this.isPlayerDie = isPlayerDie;
    }

    public bool getIsPlayerDie()
    {
        return isPlayerDie;
    }
}
