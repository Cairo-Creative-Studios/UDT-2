using NaughtyAttributes;
using Rich.DynamicPostProcessing;
using System;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace Rich.Feedbacks
{
    [ExecuteAlways]
    public class PostProcessTween : Tween
    {
        [Foldout("Post Process Settings")]
        [Dropdown("GetPostEffects")]
        public Type postProcessEffect;
        [Dropdown("GetPostEffectParameters")]
        private string parameter;

        private VolumeComponent acquiredComponent;

        public DropdownList<Type> GetPostEffects()
        {
            DropdownList<Type> dropdown = new();

            dropdown.Add("None", null);

            foreach (var type in DynamicPostProcessingManager.postProcessComponentTypes)
            {
                dropdown.Add(type.Name, type);
            }

            return dropdown;
        }

        public DropdownList<string> GetPostEffectParameters()
        {
            DropdownList<string> dropdown = new();

            if (acquiredComponent != null)
            {
                foreach (var field in acquiredComponent.GetType().GetFields())
                {
                    if(typeof(VolumeParameter).IsAssignableFrom(field.FieldType))
                        if(field.FieldType.GetProperty("value").PropertyType == typeof(float) || field.FieldType.GetProperty("value").PropertyType == typeof(Vector2) || field.FieldType.GetProperty("value").PropertyType == typeof(Vector3))
                            dropdown.Add(field.Name, field.Name);
                }
                foreach (var property in acquiredComponent.GetType().GetProperties())
                {
                    if (typeof(VolumeParameter).IsAssignableFrom(property.PropertyType))
                        if (property.PropertyType.GetProperty("value").PropertyType == typeof(float) || property.PropertyType.GetProperty("value").PropertyType == typeof(Vector2) || property.PropertyType.GetProperty("value").PropertyType == typeof(Vector3))
                            dropdown.Add(property.Name, property.Name);
                }
            }

            return dropdown;
        }

        protected override void FeedbackStarted()
        {
            acquiredComponent = DynamicPostProcessingManager.GetPostEffect(postProcessEffect);
            var component = acquiredComponent.GetType().GetField(parameter).GetValue(acquiredComponent);
            var componentValue = component.GetType().GetProperty("value");
            SetTweenedValue<float>(component, componentValue.Name);
        }
    }
}
