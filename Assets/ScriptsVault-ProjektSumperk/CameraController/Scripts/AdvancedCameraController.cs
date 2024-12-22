using UnityEngine;

namespace ProjektSumperk
{
    public class AdvancedCameraController : MonoBehaviour
    {
        public Transform target; // Target to follow
        public Vector3 offset = new Vector3(0f, 5f, -10f); // Offset from the target
        public float smoothSpeed = 0.125f; // Smoothing speed for camera movement
        public float rotationSpeed = 5f; // Speed of camera rotation
        public bool followTarget = true; // Enable/disable following target
        public bool allowRotation = true; // Enable/disable camera rotation
        public bool smoothFollow = true; // Enable/disable smooth camera follow
        public bool allowZoom = true; // Enable/disable zooming
        public float minZoom = 5f; // Minimum zoom distance
        public float maxZoom = 15f; // Maximum zoom distance
        public float zoomSpeed = 2f; // Speed of zooming

        private Vector3 desiredPosition;

        private void FixedUpdate()
        {
            target.Translate(0, 0, -0.1f);
        }

        void LateUpdate()
        {
            if (target == null)
            {
                Debug.LogError("No target assigned to AdvancedCameraController!");
                return;
            }

            if (followTarget)
            {
                if (smoothFollow)
                {
                    desiredPosition = target.position + offset;
                    transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
                }
                else
                {
                    transform.position = target.position + offset;
                }
            }

            if (allowRotation)
            {
                if (Input.GetMouseButton(1)) // Right mouse button
                {
                    float horizontalInput = Input.GetAxis("Mouse X") * rotationSpeed;
                    float verticalInput = Input.GetAxis("Mouse Y") * rotationSpeed;

                    transform.RotateAround(target.position, Vector3.up, horizontalInput);
                    transform.RotateAround(target.position, transform.right, -verticalInput);
                }
            }

            if (allowZoom)
            {
                float scrollInput = Input.GetAxis("Mouse ScrollWheel");
                float zoomAmount = offset.z - scrollInput * zoomSpeed;
                offset.z = Mathf.Clamp(zoomAmount, -maxZoom, -minZoom);
            }
        }

        public void SetTarget(Transform newTarget)
        {
            target = newTarget;
        }
    }
}