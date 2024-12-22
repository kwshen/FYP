using UnityEngine;
using UnityEngine.UI;

public class ButtonSoundBinder : MonoBehaviour
{
    public Button button; // Assign the button in the Inspector
    public string sfxClipName; // Name of the SFX clip to play on click

    private void Start()
    {
        if (button != null)
        {
            button.onClick.RemoveAllListeners(); // Clear old listeners to prevent duplication
            button.onClick.AddListener(() =>
            {
                if (AudioManager.Instance != null)
                {
                    AudioManager.Instance.PlaySFX(sfxClipName);
                }
                else
                {
                    Debug.LogWarning("AudioManager instance is missing.");
                }
            });
        }
        else
        {
            Debug.LogWarning("Button reference is missing.");
        }
    }
}
