using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CasualGameController : MonoBehaviour
{
    public string levelBGM;

    // Start is called before the first frame update
    void Start()
    {
        AudioManager.Instance.PlayMusic(levelBGM);
    }

}
