using NaughtyAttributes;
using UDT.Scriptables.Variables;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace UDT.Feedbacks
{
    [ExecuteAlways]
    public class Screenshake : Tween
    {
        public bool relativeToStartingPosition = false;
        private Transform cameraTransform;
        private Vector3 currentOffset;
        private Vector3 startingPosition;

        protected override void FeedbackStarted()
        {
            cameraTransform = Camera.main.transform;
            if(relativeToStartingPosition)
            {
                startingPosition = cameraTransform.position;
            }
        }

        public override void UpdateFeedback()
        {
            base.UpdateFeedback();

            if(valueType == ValueType.Vec3)
            {
                var vec3Value = SerializedVariable.GetScriptableAs<ScriptableVar_Vector3>().Value;
                currentOffset.x = Random.Range(-vec3Value.x, vec3Value.x);
                currentOffset.y = Random.Range(-vec3Value.y, vec3Value.y);
                currentOffset.z = Random.Range(-vec3Value.z, vec3Value.z);
            }

            cameraTransform.position = relativeToStartingPosition ? startingPosition + currentOffset : cameraTransform.position + currentOffset;
        }
    }
}
