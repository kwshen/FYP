using UnityEngine;
using TMPro;

namespace ProjektSumperk
{
    public class EncryptedPlayerPrefsExample : MonoBehaviour
    {
        public TMP_Text logText;
        public TMP_Text errorText;

        // Example 1: Saving an encrypted string
        public void SaveEncryptedString()
        {
            string playerName = "Alice";
            EncryptedPlayerPrefs.SetEncryptedString("PlayerName", playerName);
            logText.text += "Saved Player Name: " + playerName + "\n";
        }

        // Example 1: Loading an encrypted string
        public void LoadEncryptedString()
        {
            string loadedPlayerName = EncryptedPlayerPrefs.GetEncryptedString("PlayerName");
            if (loadedPlayerName != null)
            {
                logText.text += "Loaded Player Name: " + loadedPlayerName + "\n";
            }
            else
            {
                errorText.text += "Failed to load Player Name.\n";
            }
        }

        // Example 2: Saving an encrypted int
        public void SaveEncryptedInt()
        {
            int playerScore = 1000;
            EncryptedPlayerPrefs.SetEncryptedInt("PlayerScore", playerScore);
            logText.text += "Saved Player Score: " + playerScore + "\n";
        }

        // Example 2: Loading an encrypted int
        public void LoadEncryptedInt()
        {
            int loadedPlayerScore = EncryptedPlayerPrefs.GetEncryptedInt("PlayerScore");
            if (loadedPlayerScore != 0)
            {
                logText.text += "Loaded Player Score: " + loadedPlayerScore + "\n";
            }
            else
            {
                errorText.text += "Failed to load Player Score.\n";
            }
        }

        // Example 3: Saving an encrypted float
        public void SaveEncryptedFloat()
        {
            float playerHealth = 75.5f;
            EncryptedPlayerPrefs.SetEncryptedFloat("PlayerHealth", playerHealth);
            logText.text += "Saved Player Health: " + playerHealth + "\n";
        }

        // Example 3: Loading an encrypted float
        public void LoadEncryptedFloat()
        {
            float loadedPlayerHealth = EncryptedPlayerPrefs.GetEncryptedFloat("PlayerHealth");
            if (loadedPlayerHealth != 0.0f)
            {
                logText.text += "Loaded Player Health: " + loadedPlayerHealth + "\n";
            }
            else
            {
                errorText.text += "Failed to load Player Health.\n";
            }
        }

        // Example 4: Saving an encrypted double
        public void SaveEncryptedDouble()
        {
            double playerCurrency = 123456.789;
            EncryptedPlayerPrefs.SetEncryptedDouble("PlayerCurrency", playerCurrency);
            logText.text += "Saved Player Currency: " + playerCurrency + "\n";
        }

        // Example 4: Loading an encrypted double
        public void LoadEncryptedDouble()
        {
            double loadedPlayerCurrency = EncryptedPlayerPrefs.GetEncryptedDouble("PlayerCurrency");
            if (loadedPlayerCurrency != 0.0)
            {
                logText.text += "Loaded Player Currency: " + loadedPlayerCurrency + "\n";
            }
            else
            {
                errorText.text += "Failed to load Player Currency.\n";
            }
        }
    }
}