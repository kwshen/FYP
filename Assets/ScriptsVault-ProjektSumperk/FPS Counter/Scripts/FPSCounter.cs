using UnityEngine;
using TMPro;
using System.Diagnostics;

namespace ProjektSumperk
{
    public class FPSCounter : MonoBehaviour
    {
        public TMP_Text fpsText;
        public TMP_Text memoryText;
        public TMP_Text deviceText;

        private float updateInterval = 0.5f; // Update interval in seconds
        private float fpsAccumulator = 0f;
        private int framesAccumulated = 0;
        private float minFps = float.MaxValue;
        private float maxFps = 0f;

        private void Start()
        {
            int maxRefreshRate = Screen.currentResolution.refreshRate;
            Application.targetFrameRate = Mathf.Min(maxRefreshRate, 144); // Limit target frame rate to 144 or the monitor's refresh rate

            InvokeRepeating("UpdateSystemInfo", 0, updateInterval);
        }

        private void UpdateSystemInfo()
        {
            // Update FPS
            float currentFps = 1f / Time.deltaTime;
            fpsAccumulator += currentFps;
            framesAccumulated++;

            if (currentFps < minFps)
                minFps = currentFps;

            if (currentFps > maxFps)
                maxFps = currentFps;

            // Limit reported FPS to the monitor's refresh rate
            int maxRefreshRate = Screen.currentResolution.refreshRate;
            currentFps = Mathf.Min(currentFps, maxRefreshRate);

            // Update Memory Information
            Process process = Process.GetCurrentProcess();
            long monoMemory = UnityEngine.Profiling.Profiler.GetMonoUsedSizeLong() / (1024 * 1024); // MB
            long reservedMemory = process.PrivateMemorySize64 / (1024 * 1024); // MB
            long allocatedMemory = process.WorkingSet64 / (1024 * 1024); // MB

            // Update Device Information
            string screenResolution = Screen.currentResolution.ToString();
            string osName = SystemInfo.operatingSystem;
            string graphicsAPI = SystemInfo.graphicsDeviceType.ToString();
            string gpuName = SystemInfo.graphicsDeviceName;
            string cpuName = SystemInfo.processorType;
            int vramSize = SystemInfo.graphicsMemorySize; // MB
            int cpuCores = SystemInfo.processorCount;

            // Calculate average FPS
            float avgFps = fpsAccumulator / framesAccumulated;

            // Display information in TMP_Text components
            fpsText.text = $"Avg FPS: {avgFps:F2}\nMin FPS: {minFps:F2}\nMax FPS: {maxFps:F2}\nCurrent FPS: {currentFps:F2}\nFrame Time (ms): {Time.deltaTime * 1000:F2}";
            memoryText.text = $"Reserved Memory (MB): {reservedMemory}\nAllocated Memory (MB): {allocatedMemory}\nMono Memory (MB): {monoMemory}";
            deviceText.text = $"Screen Resolution: {screenResolution}\nOS: {osName}\nGraphics API: {graphicsAPI}\nGPU: {gpuName}\nCPU: {cpuName}\nVRAM (MB): {vramSize}\nCPU Cores: {cpuCores}";
        }
    }
}
