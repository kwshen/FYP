using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Text.RegularExpressions;

namespace ProjektSumperk
{
    public class AdvancePasswordSystem : MonoBehaviour
    {
        public TMP_InputField passwordInputField; // Reference to the TMP_InputField where the password is entered
        public Button toggleButton; // Reference to the button that toggles password visibility
        public TMP_Text statusText; // Reference to the TMP_Text for displaying status
        public Image strengthIndicator; // Reference to the UI circle image for password strength
        public Sprite visible;
        public Sprite notVisible;

        private bool isPasswordVisible = false; // Flag to track whether the password is currently visible

        private void Start()
        {
            // Ensure that the references are set in the Inspector
            if (passwordInputField == null || toggleButton == null || statusText == null || strengthIndicator == null)
            {
                Debug.LogError("Please assign the TMP_InputField, toggle button, status text, and strength indicator in the Inspector.");
                return;
            }

            // Add a click event listener to the toggle button
            toggleButton.onClick.AddListener(TogglePasswordVisibility);

            // Add an event listener to update the password strength in real-time
            passwordInputField.onValueChanged.AddListener(UpdatePasswordStrength);
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

        private void UpdatePasswordStrength(string newPassword)
        {
            int strength = CalculatePasswordStrength(newPassword);

            // Update the circle image color based on strength
            SetCircleImageColor(strength);

            // Update the status text based on strength
            UpdateStatusText(strength);
        }

        private int CalculatePasswordStrength(string password)
        {
            // Initialize strength to 0
            int strength = 0;

            // Check password length
            if (password.Length >= 8)
            {
                strength++;
            }

            // Check for a mix of uppercase and lowercase characters
            if (ContainsUppercaseCharacter(password) && ContainsLowercaseCharacter(password))
            {
                strength++;
            }

            // Check for at least one digit
            if (ContainsDigit(password))
            {
                strength++;
            }

            // Check for at least one special character
            if (ContainsSpecialCharacter(password))
            {
                strength++;
            }

            // Check for password policy validation
            if (MeetsPasswordPolicy(password))
            {
                strength++;
            }

            return strength;
        }

        private bool ContainsUppercaseCharacter(string text)
        {
            foreach (char c in text)
            {
                if (char.IsUpper(c))
                {
                    return true;
                }
            }
            return false;
        }

        private bool ContainsLowercaseCharacter(string text)
        {
            foreach (char c in text)
            {
                if (char.IsLower(c))
                {
                    return true;
                }
            }
            return false;
        }

        private bool ContainsDigit(string text)
        {
            foreach (char c in text)
            {
                if (char.IsDigit(c))
                {
                    return true;
                }
            }
            return false;
        }

        private bool ContainsSpecialCharacter(string text)
        {
            string specialCharacters = "!@#$%^&*";
            foreach (char c in text)
            {
                if (specialCharacters.Contains(c))
                {
                    return true;
                }
            }
            return false;
        }

        private void SetCircleImageColor(int strength)
        {
            Color color = Color.white;

            // Determine the color based on password strength
            // Customize the color-coding as needed
            switch (strength)
            {
                case 0:
                    color = Color.red; // Very Weak
                    break;
                case 1:
                    color = new Color(1f, 0.5f, 0f); // Weak (Orange)
                    break;
                case 2:
                    color = new Color(1f, 0.75f, 0f); // Moderate (Light Orange)
                    break;
                case 3:
                    color = Color.yellow; // Strong
                    break;
                case 4:
                    color = Color.green; // Very Strong
                    break;
                default:
                    color = Color.white; // Unknown
                    break;
            }

            strengthIndicator.color = color;
        }

        private void UpdateStatusText(int strength)
        {
            // Update the status text based on strength
            switch (strength)
            {
                case 0:
                    statusText.text = "Very Weak";
                    break;
                case 1:
                    statusText.text = "Weak";
                    break;
                case 2:
                    statusText.text = "Moderate";
                    break;
                case 3:
                    statusText.text = "Strong";
                    break;
                case 4:
                    statusText.text = "Very Strong";
                    break;
                default:
                    statusText.text = "Unknown";
                    break;
            }
        }

        private bool MeetsPasswordPolicy(string password)
        {
            // Implement your password policy rules here
            // For example, require at least one special character and one uppercase letter
            bool hasSpecialCharacter = ContainsSpecialCharacter(password);
            bool hasUppercaseCharacter = ContainsUppercaseCharacter(password);

            // Add more policy rules as needed
            bool policyRule1 = password.Length >= 8;
            bool policyRule2 = !Regex.IsMatch(password, @"(.)\1{2,}"); // No more than 2 repeating characters in a row

            // Return true if the password meets your policy, otherwise return false
            return hasSpecialCharacter && hasUppercaseCharacter && policyRule1 && policyRule2;
        }
    }
}
