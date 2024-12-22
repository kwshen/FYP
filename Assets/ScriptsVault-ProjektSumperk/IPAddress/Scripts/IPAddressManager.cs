using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

namespace ProjektSumperk
{
    public class IPAddressManager : MonoBehaviour
    {
        // Singleton instance
        private static IPAddressManager instance;

        // List to store available IP addresses
        private List<string> availableIPAddresses = new List<string>();

        // Event for IP address change
        public delegate void IPAddressChanged(string newIPAddress);
        public event IPAddressChanged OnIPAddressChanged;

        // TMP_Text to display IP addresses
        public TMP_Text ipAddressText;

        // Specify the network interface name from you device you want to retrieve the static IP address for
        public string staticNetworkInterfaceName = "en0"; // Change to the name of your network interface

        // Current IP address
        private string currentIPAddress = "";

        // Global IP address
        private string globalIPAddress = "";

        // Get the singleton instance
        public static IPAddressManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = FindObjectOfType<IPAddressManager>();
                    if (instance == null)
                    {
                        GameObject obj = new GameObject("IPAddressManager");
                        instance = obj.AddComponent<IPAddressManager>();
                    }
                }
                return instance;
            }
        }

        private void Awake()
        {
            // Ensure there is only one instance of IPAddressManager
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void Start()
        {
            // Initialize and retrieve the current IP address
            currentIPAddress = GetLocalIPAddress();
            availableIPAddresses.Add(currentIPAddress);
            OnIPAddressChanged?.Invoke(currentIPAddress);

            // Display the current IP address on TMP_Text
            DisplayIPAddress(currentIPAddress);

            // Retrieve and display the static IP address
            string staticIPAddress = GetStaticIPAddress();
            DisplayStaticIPAddress(staticIPAddress);

            // Retrieve and display the global IP address
            StartCoroutine(GetGlobalIPAddress());
            StartCoroutine(GetGlobalIPv4Address());
        }

        // Get the current IP address
        public string GetCurrentIPAddress()
        {
            return currentIPAddress;
        }

        // Get a list of available IP addresses
        public List<string> GetAvailableIPAddresses()
        {
            return availableIPAddresses;
        }

        // Add a new IP address to the available list
        public void AddIPAddress(string ipAddress)
        {
            if (!availableIPAddresses.Contains(ipAddress))
            {
                availableIPAddresses.Add(ipAddress);
            }
        }

        // Remove an IP address from the available list
        public void RemoveIPAddress(string ipAddress)
        {
            if (availableIPAddresses.Contains(ipAddress))
            {
                availableIPAddresses.Remove(ipAddress);
            }
        }

        // Get the local IP address of the device
        private string GetLocalIPAddress()
        {
            string localIP = "";
            string hostName = Dns.GetHostName();
            IPHostEntry ipEntry = Dns.GetHostEntry(hostName);

            foreach (IPAddress ipAddress in ipEntry.AddressList)
            {
                if (ipAddress.AddressFamily == AddressFamily.InterNetwork)
                {
                    localIP = ipAddress.ToString();
                    break;
                }
            }

            return localIP;
        }

        // Get the static IP address of the device
        private string GetStaticIPAddress()
        {
            string staticIP = "";

            NetworkInterface[] networkInterfaces = NetworkInterface.GetAllNetworkInterfaces();

            foreach (NetworkInterface networkInterface in networkInterfaces)
            {
                if (networkInterface.Name == staticNetworkInterfaceName)
                {
                    foreach (UnicastIPAddressInformation ipAddressInfo in networkInterface.GetIPProperties().UnicastAddresses)
                    {
                        if (ipAddressInfo.Address.AddressFamily == AddressFamily.InterNetwork)
                        {
                            // This is the static IPv4 address
                            staticIP = ipAddressInfo.Address.ToString();
                            break;
                        }
                    }
                }
            }

            return staticIP;
        }

        // Display the IP address on TMP_Text
        private void DisplayIPAddress(string ipAddress)
        {
            if (ipAddressText != null)
            {
                ipAddressText.text = "Current IP Address: " + ipAddress;
            }
        }

        // Display the static IP address on TMP_Text
        private void DisplayStaticIPAddress(string staticIPAddress)
        {
            if (ipAddressText != null)
            {
                ipAddressText.text += "\nStatic IP Address: " + staticIPAddress;
            }
        }

        // Coroutine to retrieve global IP address
        private IEnumerator GetGlobalIPAddress()
        {
            UnityWebRequest www = UnityWebRequest.Get("https://api64.ipify.org?format=json");
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success)
            {
                string response = www.downloadHandler.text;
                GlobalIPResponse ipResponse = JsonUtility.FromJson<GlobalIPResponse>(response);
                globalIPAddress = ipResponse.ip;
                DisplayGlobalIPAddress(globalIPAddress);
            }
            else
            {
                Debug.LogError("Failed to fetch global IP address.");
            }
        }

        // Display the global IP address on TMP_Text
        private void DisplayGlobalIPAddress(string globalIP)
        {
            if (ipAddressText != null)
            {
                ipAddressText.text += "\nGlobal IP Address: " + globalIP;
            }
        }

        // Coroutine to retrieve global IPv4 address
        private IEnumerator GetGlobalIPv4Address()
        {
            UnityWebRequest www = UnityWebRequest.Get("https://ipv4.icanhazip.com");
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success)
            {
                string globalIP = www.downloadHandler.text.Trim();
                DisplayGlobalIPAddress(globalIP);
            }
            else
            {
                Debug.LogError("Failed to fetch global IPv4 address.");
            }
        }
    }

    [System.Serializable]
    public class GlobalIPResponse
    {
        public string ip;
    }
}