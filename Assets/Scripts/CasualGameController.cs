using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class CasualGameController : MonoBehaviour
{
    public string levelBGM;
 
    // Start is called before the first frame update
    void Start()
    {
        AudioManager.Instance.PlayMusic(levelBGM);

        UnderwaterEffectController.Instance.ToggleUnderwaterEffect(false);

    }


}
