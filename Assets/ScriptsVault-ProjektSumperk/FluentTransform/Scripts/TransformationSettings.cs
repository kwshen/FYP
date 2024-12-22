using UnityEngine;

namespace ProjektSumperk
{
    [CreateAssetMenu(fileName = "NewTransformationSettings", menuName = "Transformations/TransformationSettings")]
    public class TransformationSettings : ScriptableObject
    {
        public Vector3 position;
        public Quaternion rotation;
        public Vector3 scale;
        public float duration;
        public FluentTransform.EasingType easingType;
        public FluentTransform.TransformSpace transformSpace;
    }
}