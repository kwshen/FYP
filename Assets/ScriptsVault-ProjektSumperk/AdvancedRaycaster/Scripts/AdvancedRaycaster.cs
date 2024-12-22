using UnityEngine;
using UnityEngine.Events;
using TMPro;

namespace ProjektSumperk
{
    public class AdvancedRaycaster : MonoBehaviour
    {
        [Header("Raycast Settings")]
        public LayerMask raycastLayer;         // Layers to include in raycasting
        public float maxRaycastDistance = 100f; // Maximum raycast distance
        public Color rayColor = Color.red;     // Color for debug ray visualization

        [Header("Raycast Events")]
        public bool useRaycastEvents = true;   // Enable raycast events

        [System.Serializable]
        public class RaycastEvent : UnityEvent<GameObject> { }

        [Header("Raycast Events")]
        public RaycastEvent onRaycastEnter;    // Event when raycast enters an object
        public RaycastEvent onRaycastExit;     // Event when raycast exits an object

        [Header("Raycast Visualization")]
        public bool visualizeRay = true;       // Enable visualization of the ray
        public float rayVisualizationDuration = 0.1f; // Duration to display the ray

        [Header("Raycast Hit Info")]
        public bool displayRaycastHitInfo = true; // Display raycast hit information in the console

        private RaycastHit hitInfo;
        private Transform lastHitObject;

        private Color originalCubeColor;
        public GameObject cubeToChangeColor;
        public Color hitColor = Color.green;
        public TMP_Text Status;

        private void Start()
        {
            if (cubeToChangeColor != null)
            {
                Renderer cubeRenderer = cubeToChangeColor.GetComponent<Renderer>();
                if (cubeRenderer != null)
                {
                    originalCubeColor = cubeRenderer.material.color;
                }
            }
        }

        private void Update()
        {

            if (Input.GetKey(KeyCode.Space))
            {
                // Rotate the camera based on mouse movement
                float mouseX = Input.GetAxis("Mouse X");
                float mouseY = Input.GetAxis("Mouse Y");

                // Adjust the camera rotation speed as needed
                float rotationSpeed = 2.0f;

                // Rotate the camera
                transform.Rotate(Vector3.up, mouseX * rotationSpeed);
                transform.Rotate(Vector3.left, mouseY * rotationSpeed);
            }

            // Create a ray from the camera forward direction
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            // Perform the raycast
            if (Physics.Raycast(ray, out hitInfo, maxRaycastDistance, raycastLayer))
            {
                // Check if the hit object has changed
                if (hitInfo.transform != lastHitObject)
                {
                    if (lastHitObject != null)
                    {
                        // Handle raycast exit event
                        if (useRaycastEvents)
                            OnRaycastExit(lastHitObject.gameObject);

                        // Restore the original color
                        RestoreCubeColor();
                    }

                    // Handle raycast enter event
                    if (useRaycastEvents)
                        OnRaycastEnter(hitInfo.transform.gameObject);

                    // Update the last hit object
                    lastHitObject = hitInfo.transform;
                }

                // Handle raycast hit event
                if (useRaycastEvents)
                    hitInfo.transform.SendMessage("OnRaycastHit", SendMessageOptions.DontRequireReceiver);

                // Display raycast hit information
                if (displayRaycastHitInfo)
                {
                    Debug.Log("Raycast Hit: " + hitInfo.collider.gameObject.name);
                    Debug.Log("Hit Point: " + hitInfo.point);
                    Debug.Log("Hit Normal: " + hitInfo.normal);
                }

                // Change the color of the cube when hit
                if (cubeToChangeColor != null)
                {
                    Renderer cubeRenderer = cubeToChangeColor.GetComponent<Renderer>();
                    if (cubeRenderer != null)
                    {
                        cubeRenderer.material.color = hitColor;
                    }
                }


            }
            else
            {
                // Check if the last hit object existed
                if (lastHitObject != null)
                {
                    // Handle raycast exit event for the last hit object
                    if (useRaycastEvents)
                        OnRaycastExit(lastHitObject.gameObject);

                    // Restore the original color
                    RestoreCubeColor();

                    // Reset the last hit object
                    lastHitObject = null;
                }
            }

            // Visualize the ray
            if (visualizeRay)
                Debug.DrawRay(ray.origin, ray.direction * maxRaycastDistance, rayColor, rayVisualizationDuration);
        }

        private void OnRaycastEnter(GameObject obj)
        {
            onRaycastEnter.Invoke(obj);
        }

        private void OnRaycastExit(GameObject obj)
        {
            onRaycastExit.Invoke(obj);
        }

        private void RestoreCubeColor()
        {
            if (cubeToChangeColor != null)
            {
                Renderer cubeRenderer = cubeToChangeColor.GetComponent<Renderer>();
                if (cubeRenderer != null)
                {
                    cubeRenderer.material.color = originalCubeColor;
                }
            }
        }

        public void HandleOnEnter()
        {
            Status.text = "Cube Enters to Green";
        }

        public void HandleOnExit()
        {
            Status.text = "Cube Exits to Normal";
        }
    }
}