using UnityEngine;
using UnityEngine.UI;
using TMPro;
#if PLATFORM_ANDROID
using UnityEngine.Android;
#endif
using UnityEngine.SceneManagement;

namespace ProjektSumperk
{
    public class Permissions : MonoBehaviour
    {
        // UI elements for different permissions
        public Image cam;
        public Image phone;
        public Image location;
        public Image mic;
        public Image storage;
        public Image contacts;
        public Image All;
        public Sprite done;
        public GameObject AllowPopup;
        public TMP_Text msg;
        public TMP_Text permissionMsg;

        // Currently selected permission type
        private string permissionType;

        // Requests storage permission
        public void PermissionStorage()
        {
            AndroidRuntimePermissions.Permission result = AndroidRuntimePermissions.RequestPermission("android.permission.WRITE_EXTERNAL_STORAGE");
            if (result == AndroidRuntimePermissions.Permission.Granted)
            {
                storage.sprite = done;
                Debug.Log("Storage Permission state: " + result);
                permissionMsg.text = "Storage Permission state: " + result;
            }
            else
            {
                Debug.Log("Storage Permission state: " + result);
            }
        }

        // Requests microphone permission
        public void PermissionMicrophone()
        {
            AndroidRuntimePermissions.Permission result = AndroidRuntimePermissions.RequestPermission("android.permission.RECORD_AUDIO");
            if (result == AndroidRuntimePermissions.Permission.Granted)
            {
                mic.sprite = done;
                Debug.Log("Microphone Permission state: " + result);
                permissionMsg.text = "Microphone Permission state: " + result;
            }
            else
            {
                Debug.Log("Microphone Permission state: " + result);
            }
        }

        // Requests location permission
        public void PermissionLocation()
        {
            AndroidRuntimePermissions.Permission result = AndroidRuntimePermissions.RequestPermission("android.permission.ACCESS_FINE_LOCATION");
            if (result == AndroidRuntimePermissions.Permission.Granted)
            {
                location.sprite = done;
                Debug.Log("Location Permission state: " + result);
                permissionMsg.text = "Location Permission state: " + result;
            }
            else
            {
                Debug.Log("Location Permission state: " + result);
            }
        }

        // Requests phone state permission
        public void PermissionPhone()
        {
            AndroidRuntimePermissions.Permission result = AndroidRuntimePermissions.RequestPermission("android.permission.READ_PHONE_STATE");
            if (result == AndroidRuntimePermissions.Permission.Granted)
            {
                phone.sprite = done;
                Debug.Log("Phone State Permission state: " + result);
                permissionMsg.text = "Phone State Permission state: " + result;
            }
            else
            {
                Debug.Log("Phone State Permission state: " + result);
            }
        }

        // Requests camera permission
        public void PermissionCamera()
        {
            AndroidRuntimePermissions.Permission result = AndroidRuntimePermissions.RequestPermission("android.permission.CAMERA");
            if (result == AndroidRuntimePermissions.Permission.Granted)
            {
                cam.sprite = done;
                Debug.Log("Camera Permission state: " + result);
                permissionMsg.text = "Camera Permission state: " + result;
            }
            else
            {
                Debug.Log("Camera Permission state: " + result);
            }
        }

        // Requests contacts permission
        public void PermissionContacts()
        {
            AndroidRuntimePermissions.Permission result = AndroidRuntimePermissions.RequestPermission("android.permission.READ_CONTACTS");
            if (result == AndroidRuntimePermissions.Permission.Granted)
            {
                contacts.sprite = done;
                Debug.Log("Contacts Permission state: " + result);
                permissionMsg.text = "Contacts Permission state: " + result;
            }
            else
            {
                Debug.Log("Contacts Permission state: " + result);
            }
        }

        // Requests all permissions
        public void AllowAll()
        {
            AndroidRuntimePermissions.Permission[] results = AndroidRuntimePermissions.RequestPermissions(
                "android.permission.WRITE_EXTERNAL_STORAGE",
                "android.permission.RECORD_AUDIO",
                "android.permission.ACCESS_FINE_LOCATION",
                "android.permission.READ_PHONE_STATE",
                "android.permission.CAMERA",
                "android.permission.READ_CONTACTS");

            if (results[0] == AndroidRuntimePermissions.Permission.Granted &&
                results[1] == AndroidRuntimePermissions.Permission.Granted &&
                results[2] == AndroidRuntimePermissions.Permission.Granted &&
                results[3] == AndroidRuntimePermissions.Permission.Granted &&
                results[4] == AndroidRuntimePermissions.Permission.Granted &&
                results[5] == AndroidRuntimePermissions.Permission.Granted)
            {
                storage.sprite = done;
                mic.sprite = done;
                location.sprite = done;
                phone.sprite = done;
                cam.sprite = done;
                contacts.sprite = done;
                All.sprite = done;

                Debug.Log("All Permission state granted!");
                permissionMsg.text = "All Permission state granted!";
            }
            else
            {
                Debug.Log("Some permission(s) are not granted...");
            }
        }

        // Displays permission information based on the selected type
        public void PermissionTypeButton(string type)
        {
            permissionType = type;
            UpdateMessageText(type);
            AllowPopup.SetActive(true);
        }

        // Logic to update message text based on the permission type
        private void UpdateMessageText(string type)
        {
            switch (type)
            {
                case "camera":
                    msg.text = "This PERMISSION is used to open camera for taking photos. This is the essential permission for our app. Please allow it.";
                    break;
                case "phone":
                    msg.text = "This PERMISSION is used by Unity to run App in Background. This is the essential permission for our app. Please allow it.";
                    break;
                case "location":
                    msg.text = "This PERMISSION is used to record the location information. This is the essential permission for our app. Please allow it.";
                    break;
                case "mic":
                    msg.text = "This PERMISSION is used to Record Audio. This is the essential permission for our app. Please allow it.";
                    break;
                case "storage":
                    msg.text = "This PERMISSION is used to store captured photos on the smartphone or SD card. This is the essential permission for our app. Please allow it.";
                    break;
                case "contacts":
                    msg.text = "This PERMISSION is used to take access of Contacts. Please allow it.";
                    break;
                case "all":
                    msg.text = "Please allow All Permissions: Camera, Phone State, Location, Microphone, Storage, and Contacts.";
                    break;
                default:
                    msg.text = "Unknown permission type.";
                    break;
            }
        }

        // Triggers the appropriate permission request based on the user's selection
        public void AllowPermissionButton()
        {
            AllowPopup.SetActive(false);
            switch (permissionType)
            {
                case "camera":
                    PermissionCamera();
                    break;
                case "phone":
                    PermissionPhone();
                    break;
                case "location":
                    PermissionLocation();
                    break;
                case "mic":
                    PermissionMicrophone();
                    break;
                case "storage":
                    PermissionStorage();
                    break;
                case "contacts":
                    PermissionContacts();
                    break;
                case "all":
                    AllowAll();
                    break;
            }
        }
    }
}