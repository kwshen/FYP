using UnityEngine;
using TMPro;

namespace ProjektSumperk
{
    public class ScoreManager : MonoBehaviour
    {
        public TMP_Text scoreText;
        private int score = 0;

        private void Start()
        {
            // Initialize the score text.
            UpdateScoreText();

            // Add a listener to the "CoinCollected" event.
            EventManager.Instance.AddListener("CoinCollected", OnCoinCollected);
        }

        private void OnCoinCollected()
        {
            // Handle the event when a coin is collected.
            score += 10; // Increase the score by 10 (or any desired value).
            UpdateScoreText();
        }

        private void UpdateScoreText()
        {
            // Update the score text on the UI.
            scoreText.text = "Score: " + score.ToString();
        }

        private void OnDestroy()
        {
            // Remove the listener when the ScoreManager is destroyed to prevent memory leaks.
            EventManager.Instance.RemoveListener("CoinCollected", OnCoinCollected);
        }

        private void OnApplicationQuit()
        {
            EventManager.Instance.ClearAllEventListeners();
        }
    }
}