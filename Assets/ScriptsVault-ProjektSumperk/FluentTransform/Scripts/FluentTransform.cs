using UnityEngine;
using System;
using System.Collections;
using UnityEngine.Events;

namespace ProjektSumperk
{
    public class FluentTransform : MonoBehaviour
    {
        public enum TransformSpace
        {
            Global,
            Local
        }

        public enum EasingType
        {
            Linear,
            EaseIn,
            EaseOut,
            EaseInOut
        }

        public static class EasingFunctions
        {
            public static float ApplyEasing(float time, EasingType easingType)
            {
                switch (easingType)
                {
                    case EasingType.EaseIn:
                        return time * time;
                    case EasingType.EaseOut:
                        return 1f - (1f - time) * (1f - time);
                    case EasingType.EaseInOut:
                        return time < 0.5f ? 2f * time * time : -1f + (4f - 2f * time) * time;
                    default:
                        return time; // Linear
                }
            }
        }

        public UnityEvent onMovementComplete;
        public UnityEvent onRotationComplete;
        public UnityEvent onScalingComplete;

        public FluentTransform MoveTo(Vector3 newPosition, float duration, TransformSpace space = TransformSpace.Global, EasingType easingType = EasingType.Linear, Action onComplete = null)
        {
            Action<float> transformation = space == TransformSpace.Global ?
                (Action<float>)(elapsed => transform.position = Vector3.Lerp(transform.position, newPosition, EasingFunctions.ApplyEasing(elapsed / duration, easingType))) :
                (elapsed => transform.localPosition = Vector3.Lerp(transform.localPosition, newPosition, EasingFunctions.ApplyEasing(elapsed / duration, easingType)));

            StartCoroutine(TransformationRoutine(transformation, duration, onComplete, onMovementComplete));
            return this;
        }

        public FluentTransform RotateTo(Quaternion newRotation, float duration, TransformSpace space = TransformSpace.Global, EasingType easingType = EasingType.Linear, Action onComplete = null)
        {
            Action<float> transformation = space == TransformSpace.Global ?
                (Action<float>)(elapsed => transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, EasingFunctions.ApplyEasing(elapsed / duration, easingType))) :
                (elapsed => transform.localRotation = Quaternion.Lerp(transform.localRotation, newRotation, EasingFunctions.ApplyEasing(elapsed / duration, easingType)));

            StartCoroutine(TransformationRoutine(transformation, duration, onComplete, onRotationComplete));
            return this;
        }

        public FluentTransform ScaleTo(Vector3 newScale, float duration, EasingType easingType = EasingType.Linear, Action onComplete = null)
        {
            StartCoroutine(TransformationRoutine(
                elapsed => transform.localScale = Vector3.Lerp(transform.localScale, newScale, EasingFunctions.ApplyEasing(elapsed / duration, easingType)),
                duration,
                onComplete,
                onScalingComplete
            ));
            return this;
        }

        private IEnumerator TransformationRoutine(Action<float> transformation, float duration, Action onComplete, UnityEvent onEventComplete)
        {
            float elapsed = 0;
            while (elapsed < duration)
            {
                elapsed += Time.deltaTime;
                transformation(elapsed);
                yield return null;
            }
            onComplete?.Invoke();
            onEventComplete.Invoke();
        }
    }
}