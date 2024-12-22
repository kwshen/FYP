using UnityEngine;

namespace ProjektSumperk
{
    public class PinchZoom : MonoBehaviour
    {
        public float SCALE_FACTOR = 0.07f;
        public float MAX_SCALE = 10.0f, MIN_SCALE = 0.5f;
        public float DRAG_SENSITIVITY = 200f;
        private Vector2 originalMousePos = Vector2.zero;

        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                originalMousePos = Input.mousePosition;
            }
            else if (Input.GetKey(KeyCode.Space))
            {
                ResetTransform();
            }
            else if (Input.GetMouseButton(0))
            {
                Drag();
            }
            else if (Input.mouseScrollDelta.y != 0)
            {
                float scrollDelta = Input.mouseScrollDelta.y;
                Zoom(scrollDelta);
            }
        }

        void Drag()
        {
            Vector2 delta = (Vector2)Input.mousePosition - originalMousePos;
            originalMousePos = Input.mousePosition;
            transform.Translate(delta * Time.deltaTime * DRAG_SENSITIVITY);
        }

        void Zoom(float scrollDelta)
        {
            Vector3 scale = transform.localScale;
            scale.x += SCALE_FACTOR * scrollDelta;
            scale.y += SCALE_FACTOR * scrollDelta;
            scale.x = Mathf.Clamp(scale.x, MIN_SCALE, MAX_SCALE);
            scale.y = Mathf.Clamp(scale.y, MIN_SCALE, MAX_SCALE);

            transform.localScale = scale;
        }

        void ResetTransform()
        {
            transform.localScale = Vector3.one;
            transform.localPosition = Vector3.zero;
        }
    }
}

