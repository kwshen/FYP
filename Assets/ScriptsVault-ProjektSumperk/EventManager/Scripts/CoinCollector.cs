using UnityEngine;

namespace ProjektSumperk
{
    public class CoinCollector : MonoBehaviour
    {
        private void OnMouseDown()
        {
            // Trigger the "CoinCollected" event when the player clicks on the coin.
            EventManager.Instance.TriggerEvent("CoinCollected");

            // Destroy the coin object when it's collected.
            Destroy(gameObject);
        }
    }
}