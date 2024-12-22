using UnityEngine;
using UnityEngine.UI;
using System.IO;
using TMPro;

namespace ProjektSumperk
{
    public class JSONReadWrite : MonoBehaviour
    {
        public TMP_InputField usernameInput;
        public TMP_InputField ageInput;
        public TMP_InputField emailInput;
        public TMP_Text dataText;
        public Button saveButton; // Reference to the Save button
        public Button loadButton; // Reference to the Load button

        private string jsonFilePath; // Specify your custom path here

        [System.Serializable]
        public class UserData
        {
            public string username;
            public int age;
            public string email;
            public string currentDate;
        }

        private UserData userData = new UserData();

        private void Start()
        {
            // Specify your custom path here, for example:
            jsonFilePath = Path.Combine(Application.dataPath, "ScriptsVault-ProjektSumperk/JSONReadWrite/JSON/userData.json");

            // Add onClick listeners to the Save and Load buttons
            saveButton.onClick.AddListener(SaveJSONData);
            loadButton.onClick.AddListener(LoadJSONData);

            // LoadData();
            //DisplayData();
        }

        public void SaveData()
        {
            try
            {
                // Check if any of the input fields are empty
                if (string.IsNullOrEmpty(usernameInput.text) || string.IsNullOrEmpty(ageInput.text) || string.IsNullOrEmpty(emailInput.text))
                {
                    dataText.text = "Error: All fields must be filled.";
                    Debug.LogWarning("Empty fields detected. Data not saved.");
                    return; // Exit the method without saving data
                }

                // Populate the UserData object with input field values
                userData.username = usernameInput.text;
                userData.age = int.Parse(ageInput.text);
                userData.email = emailInput.text;
                userData.currentDate = System.DateTime.Now.ToString();

                // Convert UserData to JSON and save to file
                string jsonData = JsonUtility.ToJson(userData);
                File.WriteAllText(jsonFilePath, jsonData);

                // Display a success message
                dataText.text = "Data saved successfully!";
                Debug.Log("Data saved successfully.");
            }
            catch (System.Exception e)
            {
                // Handle any other exceptions and display the error message
                dataText.text = $"Error: {e.Message}";
                Debug.LogError("Error while saving data: " + e.Message);
            }
        }


        public void LoadData()
        {
            try
            {
                if (File.Exists(jsonFilePath))
                {
                    string jsonData = File.ReadAllText(jsonFilePath);
                    userData = JsonUtility.FromJson<UserData>(jsonData);

                    // Clear any previous error messages
                    dataText.text = "";

                    // Display the loaded data
                    DisplayData();
                }
                else
                {
                    // Handle the case when the JSON file is not found
                    dataText.text = "Error: JSON file not found.";
                    Debug.LogWarning("JSON file not found. Creating a new one.");
                }
            }
            catch (System.Exception e)
            {
                // Handle any other exceptions and display the error message
                dataText.text = $"Error: {e.Message}";
                Debug.LogError("Error while loading data: " + e.Message);
            }
        }


        public void DisplayData()
        {
            dataText.text = $"Username: {userData.username}\nAge: {userData.age}\nEmail: {userData.email}\nCurrent Date: {userData.currentDate}";
        }

        // Add SaveJSONData method
        public void SaveJSONData()
        {
            SaveData();
        }

        // Add LoadJSONData method
        public void LoadJSONData()
        {
            LoadData();
            DisplayData();
        }
    }
}
