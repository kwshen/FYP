using UnityEngine;
using System;
using System.Security.Cryptography;
using System.Text;
using System.IO;

namespace ProjektSumperk
{
    public static class EncryptedPlayerPrefs
    {
        private const string encryptionKeyKey = "EncryptionKey";

        private static byte[] encryptionKey;

        static EncryptedPlayerPrefs()
        {
            InitializeEncryptionKey();
        }

        // Generate a valid encryption key of the required size
        private static void GenerateEncryptionKey()
        {
            int keySizeBytes = 32; // 256 bits key size
            encryptionKey = new byte[keySizeBytes];
            using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(encryptionKey);
            }
            // Save the encryption key to PlayerPrefs
            PlayerPrefs.SetString(encryptionKeyKey, Convert.ToBase64String(encryptionKey));
            PlayerPrefs.Save();
        }

        // Initialize the encryption key
        private static void InitializeEncryptionKey()
        {
            // Try to load the encryption key from PlayerPrefs
            string savedEncryptionKey = PlayerPrefs.GetString(encryptionKeyKey);
            if (!string.IsNullOrEmpty(savedEncryptionKey))
            {
                // If a key was saved, use it
                encryptionKey = Convert.FromBase64String(savedEncryptionKey);
            }
            else
            {
                // Generate the encryption key if it's not already set
                GenerateEncryptionKey();
            }
        }

        // Save an encrypted string to player preferences
        public static void SetEncryptedString(string key, string value)
        {
            InitializeEncryptionKey();
            try
            {
                string encryptedValue = Encrypt(value);
                PlayerPrefs.SetString(key, encryptedValue);
                PlayerPrefs.Save();
            }
            catch (Exception e)
            {
                Debug.LogError($"Error: Failed to save Encrypted String: {key} = {value}. Error: {e.Message}");
            }
        }

        // Get an encrypted string from player preferences
        public static string GetEncryptedString(string key)
        {
            InitializeEncryptionKey();
            try
            {
                if (PlayerPrefs.HasKey(key))
                {
                    string encryptedValue = PlayerPrefs.GetString(key);
                    string decryptedValue = Decrypt(encryptedValue);
                    return decryptedValue;
                }
                return null;
            }
            catch (Exception e)
            {
                Debug.LogError($"Error: Failed to load Encrypted String: {key}. Error: {e.Message}");
                return null;
            }
        }

        // Save an encrypted int to player preferences
        public static void SetEncryptedInt(string key, int value)
        {
            InitializeEncryptionKey();
            try
            {
                string encryptedValue = Encrypt(value.ToString());
                PlayerPrefs.SetString(key, encryptedValue);
                PlayerPrefs.Save();
            }
            catch (Exception e)
            {
                Debug.LogError($"Error: Failed to save Encrypted Int: {key} = {value}. Error: {e.Message}");
            }
        }

        // Get an encrypted int from player preferences
        public static int GetEncryptedInt(string key)
        {
            InitializeEncryptionKey();
            try
            {
                if (PlayerPrefs.HasKey(key))
                {
                    string encryptedValue = PlayerPrefs.GetString(key);
                    string decryptedValue = Decrypt(encryptedValue);
                    int intValue;
                    if (int.TryParse(decryptedValue, out intValue))
                    {
                        return intValue;
                    }
                }
                return 0; // Default value if key doesn't exist or parsing fails
            }
            catch (Exception e)
            {
                Debug.LogError($"Error: Failed to load Encrypted Int: {key}. Error: {e.Message}");
                return 0;
            }
        }

        // Save an encrypted float to player preferences
        public static void SetEncryptedFloat(string key, float value)
        {
            InitializeEncryptionKey();
            try
            {
                string encryptedValue = Encrypt(value.ToString());
                PlayerPrefs.SetString(key, encryptedValue);
                PlayerPrefs.Save();
            }
            catch (Exception e)
            {
                Debug.LogError($"Error: Failed to save Encrypted Float: {key} = {value}. Error: {e.Message}");
            }
        }

        // Get an encrypted float from player preferences
        public static float GetEncryptedFloat(string key)
        {
            InitializeEncryptionKey();
            try
            {
                if (PlayerPrefs.HasKey(key))
                {
                    string encryptedValue = PlayerPrefs.GetString(key);
                    string decryptedValue = Decrypt(encryptedValue);
                    float floatValue;
                    if (float.TryParse(decryptedValue, out floatValue))
                    {
                        return floatValue;
                    }
                }
                return 0.0f; // Default value if key doesn't exist or parsing fails
            }
            catch (Exception e)
            {
                Debug.LogError($"Error: Failed to load Encrypted Float: {key}. Error: {e.Message}");
                return 0.0f;
            }
        }

        // Save an encrypted double to player preferences
        public static void SetEncryptedDouble(string key, double value)
        {
            InitializeEncryptionKey();
            try
            {
                string encryptedValue = Encrypt(value.ToString());
                PlayerPrefs.SetString(key, encryptedValue);
                PlayerPrefs.Save();
            }
            catch (Exception e)
            {
                Debug.LogError($"Error: Failed to save Encrypted Double: {key} = {value}. Error: {e.Message}");
            }
        }

        // Get an encrypted double from player preferences
        public static double GetEncryptedDouble(string key)
        {
            InitializeEncryptionKey();
            try
            {
                if (PlayerPrefs.HasKey(key))
                {
                    string encryptedValue = PlayerPrefs.GetString(key);
                    string decryptedValue = Decrypt(encryptedValue);
                    double doubleValue;
                    if (double.TryParse(decryptedValue, out doubleValue))
                    {
                        return doubleValue;
                    }
                }
                return 0.0; // Default value if key doesn't exist or parsing fails
            }
            catch (Exception e)
            {
                Debug.LogError($"Error: Failed to load Encrypted Double: {key}. Error: {e.Message}");
                return 0.0;
            }
        }

        // Encrypt a string using AES encryption
        private static string Encrypt(string value)
        {
            byte[] data = Encoding.UTF8.GetBytes(value);
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = encryptionKey; // Use the encryption key as-is
                aesAlg.IV = new byte[aesAlg.BlockSize / 8];

                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        csEncrypt.Write(data, 0, data.Length);
                        csEncrypt.FlushFinalBlock();
                    }
                    return Convert.ToBase64String(msEncrypt.ToArray());
                }
            }
        }

        // Decrypt an encrypted string using AES decryption
        private static string Decrypt(string encryptedValue)
        {
            byte[] cipherText = Convert.FromBase64String(encryptedValue);

            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = encryptionKey; // Use the encryption key as-is
                aesAlg.IV = new byte[aesAlg.BlockSize / 8];

                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                using (MemoryStream msDecrypt = new MemoryStream(cipherText))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {
                            return srDecrypt.ReadToEnd();
                        }
                    }
                }
            }
        }
    }
}