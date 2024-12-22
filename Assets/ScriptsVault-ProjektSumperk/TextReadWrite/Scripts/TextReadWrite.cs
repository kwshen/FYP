using UnityEngine;
using UnityEngine.UI;
using System.IO;
using TMPro;

namespace ProjektSumperk
{
    public class TextReadWrite : MonoBehaviour
    {
        public TMP_InputField inputField; // Reference to the input field for entering text
        public TMP_Text textDisplay; // Reference to the TMP text component for displaying text
        public Button saveButton; // Reference to the Save button
        public Button loadButton; // Reference to the Load button

        private string textFilePath; // Specify your custom path here

        private string textData = "";

        private void Start()
        {
            // Specify your custom path here, for example:
            textFilePath = Path.Combine(Application.dataPath, "ScriptsVault-ProjektSumperk/TextReadWrite/TextFile/textData.txt");

            // Add onClick listeners to the Save and Load buttons
            saveButton.onClick.AddListener(SaveTextData);
            loadButton.onClick.AddListener(LoadTextData);

            // Load and display initial text data
            //LoadTextData();
        }

        public void SaveTextData()
        {
            try
            {
                // Check if the input field is empty
                if (string.IsNullOrEmpty(inputField.text))
                {
                    textDisplay.text = "Error: Input field is empty.";
                    Debug.LogWarning("Empty input field detected. Text data not saved.");
                    return; // Exit the method without saving data
                }

                // Get text from the input field
                textData = inputField.text;

                // Write text data to the specified file
                File.WriteAllText(textFilePath, textData);

                // Display a success message
                textDisplay.text = "Text data saved successfully!";
                Debug.Log("Text data saved successfully.");
            }
            catch (System.Exception e)
            {
                // Handle any other exceptions and display the error message
                textDisplay.text = $"Error: {e.Message}";
                Debug.LogError("Error while saving text data: " + e.Message);
            }
        }

        public void LoadTextData()
        {
            try
            {
                if (File.Exists(textFilePath))
                {
                    // Read text data from the specified file
                    textData = File.ReadAllText(textFilePath);

                    // Clear any previous error messages
                    textDisplay.text = "";

                    // Display the loaded text data
                    textDisplay.text = textData;
                }
                else
                {
                    // Handle the case when the text file is not found
                    textDisplay.text = "No text data found.";
                    Debug.LogWarning("Text file not found.");
                }
            }
            catch (System.Exception e)
            {
                // Handle any other exceptions and display the error message
                textDisplay.text = $"Error: {e.Message}";
                Debug.LogError("Error while loading text data: " + e.Message);
            }
        }
    }
}