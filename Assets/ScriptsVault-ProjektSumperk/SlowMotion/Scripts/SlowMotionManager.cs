using UnityEngine;
using TMPro;

namespace ProjektSumperk
{
    public class SlowMotionManager : MonoBehaviour
    {
        [Header("Slow Motion Settings")]
        public float slowMotionFactor = 0.5f; // Adjust the slow-motion factor (0.0f to 1.0f, 0.0f = no slow motion, 1.0f = full slow motion)
        public float transitionDuration = 0.5f; // Duration of the transition to slow motion (in seconds)

        [Header("Key Bindings")]
        public KeyCode slowMotionKey = KeyCode.Space; // Key to trigger slow motion (customize as needed)

        [Header("UI Text")]
        public TMP_Text statusText; // Reference to the TMP_Text component to display status

        private float originalFixedDeltaTime;
        private bool isSlowingDown = false;
        private float currentTransitionTime = 0f;

        private void Start()
        {
            originalFixedDeltaTime = Time.fixedDeltaTime;
            UpdateStatusText();
        }

        private void Update()
        {
            // Check if the slow-motion key is pressed
            if (Input.GetKeyDown(slowMotionKey))
            {
                if (!isSlowingDown)
                {
                    // Start slowing down time
                    isSlowingDown = true;
                    currentTransitionTime = 0f;
                }
                else
                {
                    // Resume normal time
                    isSlowingDown = false;
                    currentTransitionTime = transitionDuration;
                }
            }

            // Apply slow motion
            if (isSlowingDown)
            {
                currentTransitionTime += Time.deltaTime;
                float t = Mathf.Clamp01(currentTransitionTime / transitionDuration);

                // Smoothly transition to slow motion
                Time.timeScale = Mathf.Lerp(1.0f, slowMotionFactor, t);
                Time.fixedDeltaTime = originalFixedDeltaTime * Time.timeScale;
            }
            else
            {
                // Resume normal time
                Time.timeScale = 1.0f;
                Time.fixedDeltaTime = originalFixedDeltaTime;
            }

            UpdateStatusText();
        }

        // Update the TMP_Text component with the current status
        private void UpdateStatusText()
        {
            if (statusText != null)
            {
                if (isSlowingDown)
                {
                    statusText.text = "Slow Motion Active (Press " + slowMotionKey.ToString() + " to Resume)";
                }
                else
                {
                    statusText.text = "Normal Time (Press " + slowMotionKey.ToString() + " for Slow Motion)";
                }
            }
        }
    }
}