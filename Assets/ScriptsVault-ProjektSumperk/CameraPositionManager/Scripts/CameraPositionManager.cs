using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

namespace ProjektSumperk
{
    public class CameraPositionManager : MonoBehaviour
    {
        [Serializable]
        public class CameraPositionData
        {
            public Vector3 Position;
            public Quaternion Rotation;
            public UnityEvent OnReachedPosition; // Event to trigger when the camera reaches this position
        }

        [SerializeField] private GameObject cameraObject;
        [SerializeField] private List<CameraPositionData> cameraPositions;
        [SerializeField] private TMP_Text positionText; // Reference to the TMP_Text component

        private int currentPositionIndex = 0;
        private Coroutine cameraMovementCoroutine;

        private void Start()
        {
            // Cache references during initialization
            if (cameraObject == null)
            {
                Debug.LogError("Camera object reference is missing!");
                return;
            }

            // Set initial camera position
            SwitchToPosition(currentPositionIndex);
        }

        public void SwitchToPosition(int index)
        {
            if (index < 0 || index >= cameraPositions.Count)
            {
                Debug.LogError("Invalid camera position index!");
                return;
            }

            CameraPositionData positionData = cameraPositions[index];

            if (cameraMovementCoroutine != null)
                StopCoroutine(cameraMovementCoroutine);

            cameraMovementCoroutine = StartCoroutine(SmoothlyMoveCamera(positionData.Position, positionData.Rotation, index, positionData.OnReachedPosition));
        }

        public void Previous()
        {
            currentPositionIndex--;
            if (currentPositionIndex < 0)
                currentPositionIndex = cameraPositions.Count - 1;

            SwitchToPosition(currentPositionIndex);
        }

        public void Next()
        {
            currentPositionIndex++;
            if (currentPositionIndex >= cameraPositions.Count)
                currentPositionIndex = 0;

            SwitchToPosition(currentPositionIndex);
        }

        private IEnumerator SmoothlyMoveCamera(Vector3 newPosition, Quaternion newRotation, int index, UnityEvent onReachedPosition)
        {
            float duration = 2f;
            float elapsedTime = 0f;
            Vector3 initialPosition = cameraObject.transform.position;
            Quaternion initialRotation = cameraObject.transform.rotation;

            while (elapsedTime < duration)
            {
                float t = elapsedTime / duration;
                Vector3 interpolatedPosition = Vector3.Slerp(initialPosition, newPosition, t);
                Quaternion interpolatedRotation = Quaternion.Slerp(initialRotation, newRotation, t);

                cameraObject.transform.position = interpolatedPosition;
                cameraObject.transform.rotation = interpolatedRotation;

                elapsedTime += Time.deltaTime;
                yield return null;
            }

            cameraObject.transform.position = newPosition;
            cameraObject.transform.rotation = newRotation;

            // Trigger the event when the camera reaches the position
            if (onReachedPosition != null)
                onReachedPosition.Invoke();

            // Update the TMP_Text component with the index of the reached position
            if (positionText != null)
                positionText.text = "Camera Reached Position Index: " + index.ToString();
        }
    }
}