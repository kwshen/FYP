using UnityEngine;
using TMPro;

namespace ProjektSumperk
{
    public class CountdownTimer : MonoBehaviour
    {
        // Countdown timer properties
        public float initialTime = 60f;
        private float currentTime = 0f;
        public bool isCountdownRunning = false;

        // TMP_Text to display countdown and events
        public TMP_Text countdownText;

        private string eventLog = "";

        // Events for countdown callbacks
        public delegate void CountdownStart();
        public event CountdownStart OnCountdownStart;

        public delegate void CountdownPaused();
        public event CountdownPaused OnCountdownPaused;

        public delegate void CountdownResumed();
        public event CountdownResumed OnCountdownResumed;

        public delegate void CountdownComplete();
        public event CountdownComplete OnCountdownComplete;

        private void Start()
        {
            // Initialize the countdown timer
            currentTime = initialTime;
            UpdateCountdownDisplay();
            UpdateEventLog("Countdown Timer Initialized");

            // Register custom callback methods for all events
            OnCountdownStart += HandleCountdownStart;
            OnCountdownPaused += HandleCountdownPaused;
            OnCountdownResumed += HandleCountdownResumed;
            OnCountdownComplete += HandleCountdownComplete;
        }

        private void Update()
        {
            if (isCountdownRunning)
            {
                currentTime -= Time.deltaTime;

                if (currentTime <= 0)
                {
                    currentTime = 0;
                    isCountdownRunning = false;
                    OnCountdownComplete?.Invoke();
                }

                UpdateCountdownDisplay();
            }
        }

        public void StartCountdown()
        {
            if (!isCountdownRunning)
            {
                isCountdownRunning = true;
                OnCountdownStart?.Invoke();
            }
        }

        public void PauseCountdown()
        {
            if (isCountdownRunning)
            {
                isCountdownRunning = false;
                OnCountdownPaused?.Invoke();
            }
        }

        public void ResumeCountdown()
        {
            if (!isCountdownRunning)
            {
                isCountdownRunning = true;
                OnCountdownResumed?.Invoke();
            }
        }

        public void SetInitialTime(float time)
        {
            initialTime = time;
            if (!isCountdownRunning)
            {
                currentTime = initialTime;
                UpdateCountdownDisplay();
            }
        }

        private void UpdateCountdownDisplay()
        {
            if (countdownText != null)
            {
                countdownText.text = FormatTime(currentTime) + "\n\n" + eventLog;
            }
        }

        private void UpdateEventLog(string log)
        {
            eventLog += log + "\n";
            UpdateCountdownDisplay();
        }

        private string FormatTime(float timeInSeconds)
        {
            int minutes = Mathf.FloorToInt(timeInSeconds / 60);
            int seconds = Mathf.FloorToInt(timeInSeconds % 60);
            int milliseconds = Mathf.FloorToInt((timeInSeconds * 1000) % 1000);
            return string.Format("{0:00}:{1:00}:{2:000}", minutes, seconds, milliseconds);
        }

        // Custom callback methods for all events
        private void HandleCountdownStart()
        {
            // Your custom logic for countdown start here
            UpdateEventLog("Countdown Started");
        }

        private void HandleCountdownPaused()
        {
            // Your custom logic for countdown pause here
            UpdateEventLog("Countdown Paused");
        }

        private void HandleCountdownResumed()
        {
            // Your custom logic for countdown resume here
            UpdateEventLog("Countdown Resumed");
        }

        private void HandleCountdownComplete()
        {
            // Your custom logic for countdown completion here
            UpdateEventLog("Countdown Complete");
        }
    }
}