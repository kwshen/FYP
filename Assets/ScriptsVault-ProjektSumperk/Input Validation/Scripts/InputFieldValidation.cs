using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace ProjektSumperk
{
    public class InputFieldValidation : MonoBehaviour
    {
        public TMP_InputField nameInputField;
        public TMP_Text nameCounterText;
        public TMP_Text nameErrorText;
        public int nameMaxCharacters = 50;

        public TMP_InputField ageInputField;
        public TMP_Text ageCounterText;
        public TMP_Text ageErrorText;
        public int ageMaxCharacters = 2;

        public TMP_InputField emailInputField;
        public TMP_Text emailCounterText;
        public TMP_Text emailErrorText;
        public int emailMaxCharacters = 100;


        public TMP_InputField dateInputField;
        public TMP_Text dateCounterText;
        public TMP_Text dateErrorText;
        public int dateMaxCharacters = 10;

        public TMP_InputField phoneInputField;
        public TMP_Text phoneCounterText;
        public TMP_Text phoneErrorText;
        public int phoneMaxCharacters = 15;

        private void Update()
        {
            UpdateCharacterCount(nameInputField, nameCounterText, nameMaxCharacters);
            UpdateCharacterCount(ageInputField, ageCounterText, ageMaxCharacters);
            UpdateCharacterCount(emailInputField, emailCounterText, emailMaxCharacters);
            UpdateCharacterCount(dateInputField, dateCounterText, dateMaxCharacters);
            UpdateCharacterCount(phoneInputField, phoneCounterText, phoneMaxCharacters);
        }

        public void ValidateName()
        {
            string name = nameInputField.text;
            nameInputField.characterLimit = nameMaxCharacters;
            // Validate alphabet characters and spaces
            if (!System.Text.RegularExpressions.Regex.IsMatch(name, @"^[a-zA-Z ]+$"))
            {
                nameErrorText.text = "Invalid name format. Please enter alphabet characters and spaces only.";
            }
            else
            {
                nameErrorText.text = string.Empty; // Clear error text if valid
            }
        }

        public void ValidateAge()
        {
            string age = ageInputField.text;
            ageInputField.characterLimit = ageMaxCharacters;
            // Validate 2-digit numbers only
            // Validate 1 or 2 digit numbers
            if (!System.Text.RegularExpressions.Regex.IsMatch(age, @"^\d{1,2}$"))
            {
                ageErrorText.text = "Invalid age format. Please enter 1 or 2 digit numbers only.";
            }
            else
            {
                ageErrorText.text = string.Empty; // Clear error text if valid
            }
        }

        public void ValidateEmail()
        {
            string email = emailInputField.text;
            emailInputField.characterLimit = emailMaxCharacters;
            // Validate email format
            if (!System.Text.RegularExpressions.Regex.IsMatch(email, @"^\S+@\S+\.\S+$"))
            {
                emailErrorText.text = "Invalid email format. Please enter a valid email address.";
            }
            else
            {
                emailErrorText.text = string.Empty; // Clear error text if valid
            }
        }

        public void ValidateDate()
        {
            string date = dateInputField.text;
            dateInputField.characterLimit = dateMaxCharacters;
            // Validate date format (e.g., "MM/DD/YYYY")
            if (!System.Text.RegularExpressions.Regex.IsMatch(date, @"^(0[1-9]|1[0-2])/(0[1-9]|[12][0-9]|3[01])/\d{4}$"))
            {
                dateErrorText.text = "Invalid date format. Please enter a valid date (MM/DD/YYYY).";
            }
            else
            {
                dateErrorText.text = string.Empty; // Clear error text if valid
            }
        }

        public void ValidatePhoneNumber()
        {
            string phoneNumber = phoneInputField.text;
            phoneInputField.characterLimit = phoneMaxCharacters;
            // Validate phone number format (e.g., (123) 456-7890 or 123-456-7890)
            if (!System.Text.RegularExpressions.Regex.IsMatch(phoneNumber, @"^\(?\d{3}\)?[-.\s]?\d{3}[-.\s]?\d{4}$"))
            {
                phoneErrorText.text = "Invalid phone number format. Please enter a valid phone number.";
            }
            else
            {
                phoneErrorText.text = string.Empty; // Clear error text if valid
            }
        }

        private void UpdateCharacterCount(TMP_InputField inputField, TMP_Text counterText, int maxCharacters)
        {
            if (inputField != null && counterText != null)
            {
                int characterCount = inputField.text.Length;
                counterText.text = $"{characterCount}/{maxCharacters}";
            }
        }
    }
}