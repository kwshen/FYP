using UnityEngine;

public class UnderwaterChecker : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Water"))
        {
            UnderwaterEffectController.Instance.ToggleUnderwaterEffect(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        UnderwaterEffectController.Instance.ToggleUnderwaterEffect(false);
    }
}
