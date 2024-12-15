using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

namespace ExciteOMeter
{
    public class HeartRateManager : MonoBehaviour
    {
        [Header("Incoming data type")]
        public DataType dataType = DataType.NONE;


        public Color connectedColor = new Color(0, 1, 0);
        public Color disconnectedColor = new Color(1, 0, 0);

        private bool currentlyConnected = false;

        private float heartrate;
        private string labelText;

        private void Start()
        {
            labelText = dataType.ToString() + " : ";

            // Setup connection indication
            currentlyConnected = false;

        }

        void OnEnable()
        {
            EoM_Events.OnStreamConnected += StreamConnection;
            EoM_Events.OnStreamDisconnected += StreamDisconnection;
            EoM_Events.OnDataReceived += DataReceived;
        }

        void OnDisable()
        {
            EoM_Events.OnStreamConnected -= StreamConnection;
            EoM_Events.OnStreamDisconnected -= StreamDisconnection;
            EoM_Events.OnDataReceived -= DataReceived;
        }



        private void StreamConnection(DataType type)
        {
            // If type of data from new LSL connection is equal to this flag's type
            if (type == dataType)
            {
                currentlyConnected = true;
            }
        }

        private void StreamDisconnection(DataType type)
        {
            // If type of data from new LSL connection is equal to this flag's type
            if (type == dataType)
            {
                currentlyConnected = false;
            }

            ExciteOMeterOnlineUI.instance.ShowDisconnectedSignal();
        }

        private void DataReceived(DataType type, float timestamp, float value)
        {
            if (type == dataType)
            {
                heartrate = value;
            }
        }

        public bool getCurrentlyConnected()
        {
            return currentlyConnected;
        }

        public float getHeartrate()
        {
            return heartrate;
        }

        public string getLabelText()
        {
           return labelText;
        }
    }
}
