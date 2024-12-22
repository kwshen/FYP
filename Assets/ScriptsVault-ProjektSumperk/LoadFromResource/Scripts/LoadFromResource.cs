using UnityEngine;
using System.Collections.Generic;

namespace ProjektSumperk
{
    public static class LoadFromResource
    {
        private static Dictionary<string, Object> resourceCache = new Dictionary<string, Object>();

        public static T Load<T>(string resourceName, string subfolder = "") where T : Object
        {
            string fullPath = string.IsNullOrEmpty(subfolder) ? resourceName : $"{subfolder}/{resourceName}";

            if (resourceCache.ContainsKey(fullPath))
            {
                // Resource is already cached, return it
                return resourceCache[fullPath] as T;
            }
            else
            {
                // Resource is not cached, load it
                T resource = Resources.Load<T>(fullPath);

                if (resource != null)
                {
                    // Cache the loaded resource for future use
                    resourceCache.Add(fullPath, resource);
                    return resource;
                }
                else
                {
                    Debug.LogError($"Failed to load resource: {fullPath}");
                    return null;
                }
            }
        }

        public static T LoadJSON<T>(string jsonName, string subfolder = "")
        {
            string fullPath = string.IsNullOrEmpty(subfolder) ? jsonName : $"{subfolder}/{jsonName}";
            TextAsset jsonText = Resources.Load<TextAsset>(fullPath);

            if (jsonText != null)
            {
                return JsonUtility.FromJson<T>(jsonText.text);
            }
            else
            {
                Debug.LogError($"Failed to load JSON file: {jsonName}");
                return default(T); // Return a default value or handle the error as needed
            }
        }

        public static void ClearCache()
        {
            resourceCache.Clear();
        }
    }
}