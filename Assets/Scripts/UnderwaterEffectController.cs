//using UnityEditor;
//using UnityEngine;
//using UnityEngine.Rendering;
//using UnityEngine.Rendering.Universal;

//public class UnderwaterEffectController : MonoBehaviour
//{
//    private static UnderwaterEffectController _instance;

//    // Modify the Instance property to be accessed correctly
//    public static UnderwaterEffectController Instance
//    {
//        get
//        {
//            if (_instance == null)
//                _instance = FindObjectOfType<UnderwaterEffectController>();
//            return _instance;
//        }
//    }

//    void Awake()
//    {
//        if (_instance == null)
//        {
//            _instance = this;
//            DontDestroyOnLoad(gameObject);
//        }
//        else
//        {
//            Destroy(gameObject);
//        }
//    }

//    public void ToggleUnderwaterEffect(bool enable)
//    {
//        // Get the URP asset
//        var pipeline = GraphicsSettings.currentRenderPipeline as UniversalRenderPipelineAsset;
//        if (pipeline == null)
//        {
//            Debug.LogError("URP is not set up properly!");
//            return;
//        }

//        // Get the renderer data using SerializedObject
//        var serializedPipeline = new SerializedObject(pipeline);
//        var rendererDataList = serializedPipeline.FindProperty("m_RendererDataList");
//        var rendererData = rendererDataList.GetArrayElementAtIndex(0).objectReferenceValue as UniversalRendererData;

//        if (rendererData == null)
//        {
//            Debug.LogError("Could not get renderer data!");
//            return;
//        }

//        // Find and toggle the underwater feature
//        foreach (var feature in rendererData.rendererFeatures)
//        {
//            if (feature.name == "Underwater Effects") // Make sure this matches your feature's exact name
//            {
//                feature.SetActive(enable);
//                return;
//            }
//        }

//        Debug.LogWarning("Underwater renderer feature not found!");
//    }
//}

using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class UnderwaterEffectController : MonoBehaviour
{
    private static UnderwaterEffectController _instance;

    // Singleton Instance
    public static UnderwaterEffectController Instance
    {
        get
        {
            if (_instance == null)
                _instance = FindObjectOfType<UnderwaterEffectController>();
            return _instance;
        }
    }

    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ToggleUnderwaterEffect(bool enable)
    {
        // Get the URP asset
        var pipeline = GraphicsSettings.currentRenderPipeline as UniversalRenderPipelineAsset;
        if (pipeline == null)
        {
            Debug.LogError("URP is not set up properly!");
            return;
        }

        // Get the default renderer data
        var rendererData = pipeline.GetRenderer(0); // Default renderer, usually at index 0

        if (rendererData == null)
        {
            Debug.LogError("Could not get renderer data!");
            return;
        }

        // Find and toggle the underwater feature
        foreach (var feature in rendererData.rendererFeatures)
        {
            if (feature != null && feature.name == "Underwater Effects") // Make sure this matches your feature's exact name
            {
                feature.SetActive(enable); // Enable or disable the feature
                Debug.Log($"Underwater effect {(enable ? "enabled" : "disabled")}.");
                return;
            }
        }

        Debug.LogWarning("Underwater renderer feature not found!");
    }
}
