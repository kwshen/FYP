using UnityEngine;

namespace ProjektSumperk
{
    public class SwipeRotateCamera : MonoBehaviour
    {
        public Transform target;
        public float rotationSpeed = 5.0f;
        public float inertiaDamping = 0.1f; // Damping for inertia

        private Vector2 inputStartPos;
        private Vector2 lastInputPos;
        private bool isInputActive = false;
        private bool isRotatingAutomatically = true;

        private float swipeVelocity; // Velocity from swiping

        private Quaternion initialRotation;
        Vector3 initialPos;



        private void Start()
        {
            initialRotation = transform.rotation;
            initialPos = transform.position;
        }

        private void Update()
        {
            if (isRotatingAutomatically)
            {
                // Rotate the camera automatically until the user interacts
                transform.RotateAround(target.position, Vector3.up, rotationSpeed * Time.deltaTime);
            }

            if (Input.touchCount == 1)
            {
                Touch touch = Input.GetTouch(0);
                HandleInput(touch.position, touch.phase);
            }
            else
            {
                HandleInput(Input.mousePosition, GetMousePhase());
            }

            if (!isInputActive && Mathf.Abs(swipeVelocity) > 0.01f)
            {
                // Apply inertia damping to swipeVelocity
                swipeVelocity *= (1 - inertiaDamping);

                // Apply the swipe velocity as a rotation
                transform.RotateAround(target.position, Vector3.up, swipeVelocity * Time.deltaTime);
            }
        }

        private TouchPhase GetMousePhase()
        {
            if (Input.GetMouseButtonDown(0))
            {
                return TouchPhase.Began;
            }
            else if (Input.GetMouseButton(0))
            {
                return TouchPhase.Moved;
            }
            else if (Input.GetMouseButtonUp(0))
            {
                return TouchPhase.Ended;
            }
            return TouchPhase.Canceled;
        }

        private void HandleInput(Vector2 inputPosition, TouchPhase touchPhase)
        {
            switch (touchPhase)
            {
                case TouchPhase.Began:
                    if (isRotatingAutomatically)
                    {
                        isRotatingAutomatically = false;
                    }
                    inputStartPos = inputPosition;
                    lastInputPos = inputPosition;
                    isInputActive = true;
                    swipeVelocity = 0f;
                    break;

                case TouchPhase.Moved:
                    if (isInputActive)
                    {
                        Vector2 inputDelta = inputPosition - lastInputPos;
                        float rotationAngle = inputDelta.x * rotationSpeed * Time.deltaTime;
                        transform.RotateAround(target.position, Vector3.up, rotationAngle);
                        lastInputPos = inputPosition;
                        swipeVelocity = rotationAngle / Time.deltaTime;
                    }
                    break;

                case TouchPhase.Ended:
                case TouchPhase.Canceled:
                    isInputActive = false;
                    break;
            }
        }

        public void ReturnToInitialPosition()
        {
            isRotatingAutomatically = false;
            transform.position = initialPos;
            transform.rotation = initialRotation;
        }
    }
}