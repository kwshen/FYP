using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace ProjektSumperk
{
    public class PasswordToggle : MonoBehaviour
    {
        public TMP_InputField passwordInputField; // Reference to the TMP_InputField where the password is entered
        public Button toggleButton; // Reference to the button that toggles password visibility
        public TMP_Text statusText; // Reference to the TMP_Text for displaying status
        public Sprite visible;
        public Sprite notVisible;

        private bool isPasswordVisible = false; // Flag to track whether the password is currently visible

        private void Start()
        {
            // Ensure that the references are set in the Inspector
            if (passwordInputField == null || toggleButton == null || statusText == null)
            {
                Debug.LogError("Please assign the TMP_InputField, toggle button, and status text in the Inspector.");
                return;
            }

            // Add a click event listener to the toggle button
            toggleButton.onClick.AddListener(TogglePasswordVisibility);
        }

        public void TogglePasswordVisibility()
        {
            // Toggle the visibility of the password
            isPasswordVisible = !isPasswordVisible;

            // Update the input field's content type and secure text display
            if (isPasswordVisible)
            {
                passwordInputField.contentType = TMP_InputField.ContentType.Standard;
                passwordInputField.inputType = TMP_InputField.InputType.Standard;
                statusText.text = "Password is visible";
                toggleButton.GetComponent<Image>().sprite = visible;
            }
            else
            {
                passwordInputField.contentType = TMP_InputField.ContentType.Password;
                passwordInputField.inputType = TMP_InputField.InputType.Password;
                statusText.text = "Password is hidden";
                toggleButton.GetComponent<Image>().sprite = notVisible;
            }

            // Refresh the input field to apply the changes
            passwordInputField.ForceLabelUpdate();
        }
    }
}