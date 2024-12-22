using UnityEngine;
using TMPro;

namespace ProjektSumperk
{
    public class SwipeDemo : MonoBehaviour
    {
        public TMP_Text swipeLogText; // Reference to the TMP_Text component in the UI

        private void Start()
        {
            // Subscribe to the OnSwipeDetected event
            SwipeManager.OnSwipeDetected += HandleSwipe;
        }

        private void OnDestroy()
        {
            // Unsubscribe from the event when the object is destroyed
            SwipeManager.OnSwipeDetected -= HandleSwipe;
        }

        private void HandleSwipe(Swipe swipeDirection, Vector2 swipeVelocity)
        {
            // Handle different swipe directions
            switch (swipeDirection)
            {
                case Swipe.Up:
                    LogSwipe("Swipe Up detected!");
                    // Perform actions for swipe up
                    break;
                case Swipe.Down:
                    LogSwipe("Swipe Down detected!");
                    // Perform actions for swipe down
                    break;
                case Swipe.Left:
                    LogSwipe("Swipe Left detected!");
                    // Perform actions for swipe left
                    break;
                case Swipe.Right:
                    LogSwipe("Swipe Right detected!");
                    // Perform actions for swipe right
                    break;
                case Swipe.UpLeft:
                    LogSwipe("Swipe Up-Left detected!");
                    // Perform actions for swipe up-left
                    break;
                case Swipe.UpRight:
                    LogSwipe("Swipe Up-Right detected!");
                    // Perform actions for swipe up-right
                    break;
                case Swipe.DownLeft:
                    LogSwipe("Swipe Down-Left detected!");
                    // Perform actions for swipe down-left
                    break;
                case Swipe.DownRight:
                    LogSwipe("Swipe Down-Right detected!");
                    // Perform actions for swipe down-right
                    break;
                case Swipe.Tap:
                    LogSwipe("Tap detected!");
                    // Perform actions for tap
                    break;
                case Swipe.None:
                    LogSwipe("No swipe detected.");
                    // Handle when no swipe is detected
                    break;
            }
        }

        private void LogSwipe(string log)
        {
            // Display the swipe detection log on the TMP_Text component
            swipeLogText.text = log;

            // Also log to the console
            Debug.Log(log);
        }
    }
}