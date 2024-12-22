using UnityEngine;
using System.Collections;
using System.Net;
using System;
using TMPro;

namespace ProjektSumperk
{
    public class InternetConnectionChecker : MonoBehaviour
    {
        public enum CheckMode
        {
            OnClick,
            Timed,
            Continuous
        }

        public CheckMode checkMode = CheckMode.Continuous;
        public float checkInterval = 5.0f; // Custom interval for checking the connection (in seconds).

        private bool isConnected = false;

        // Event to notify when the connection status changes.
        public event Action<bool> OnConnectionStatusChange;

        public TMP_Text connectionStatusText; // Reference to the TMP_Text component.

        private void Start()
        {
            if (checkMode == CheckMode.Continuous)
            {
                StartCoroutine(CheckConnectionContinuously());
            }
        }

        // Method to check the internet connection.
        private bool CheckInternetConnection()
        {
            try
            {
                using (var client = new WebClient())
                using (client.OpenRead("http://www.google.com"))
                {
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        // Coroutine to check the connection continuously.
        private IEnumerator CheckConnectionContinuously()
        {
            while (true)
            {
                bool newStatus = CheckInternetConnection();

                if (newStatus != isConnected)
                {
                    isConnected = newStatus;
                    OnConnectionStatusChange?.Invoke(isConnected);
                }

                // Update the TMP_Text component.
                if (connectionStatusText != null)
                {
                    connectionStatusText.text = isConnected ? "Connected" : "Disconnected";
                }

                yield return new WaitForSeconds(checkInterval);
            }
        }

        // Method to check the connection when a button is clicked.
        public void CheckConnectionOnClick()
        {
            bool newStatus = CheckInternetConnection();

            if (newStatus != isConnected)
            {
                isConnected = newStatus;
                OnConnectionStatusChange?.Invoke(isConnected);
            }

            // Update the TMP_Text component.
            if (connectionStatusText != null)
            {
                connectionStatusText.text = isConnected ? "Connected" : "Disconnected";
            }
        }

        // Update is called once per frame.
        private void Update()
        {
            if (checkMode == CheckMode.Timed)
            {
                if (Time.time % checkInterval < Time.deltaTime)
                {
                    bool newStatus = CheckInternetConnection();

                    if (newStatus != isConnected)
                    {
                        isConnected = newStatus;
                        OnConnectionStatusChange?.Invoke(isConnected);
                    }

                    // Update the TMP_Text component.
                    if (connectionStatusText != null)
                    {
                        connectionStatusText.text = isConnected ? "Connected" : "Disconnected";
                    }
                }
            }
        }
    }
}