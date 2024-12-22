using UnityEngine;

namespace ProjektSumperk
{
    public class ExampleUsageFluentTransform : MonoBehaviour
    {
        public FluentTransform transformComponent;
        public TransformationSettings settings;

        void Start()
        {
            transformComponent.MoveTo(settings.position, settings.duration, settings.transformSpace, settings.easingType, () => Debug.Log("Movement Complete"))
                              .RotateTo(settings.rotation, settings.duration, settings.transformSpace, settings.easingType, () => Debug.Log("Rotation Complete"))
                              .ScaleTo(settings.scale, settings.duration, settings.easingType, () => Debug.Log("Scaling Complete"));

            //StartTransform();
        }

        void StartTransform()
        {
            gameObject.GetComponent<FluentTransform>()
              .MoveTo(new Vector3(5.0f, 0f, -20f), 2.0f, FluentTransform.TransformSpace.Global, FluentTransform.EasingType.EaseInOut, () => Debug.Log("Movement Complete"))
              .RotateTo(Quaternion.Euler(0f, 90.0f, 0), 2.0f, FluentTransform.TransformSpace.Global, FluentTransform.EasingType.EaseInOut, () => Debug.Log("Rotation Complete"))
              .ScaleTo(new Vector3(5.0f, 5.0f, 5.0f), 3.0f, FluentTransform.EasingType.EaseInOut, () => Debug.Log("Rotation Complete"));

        }
    }
}
