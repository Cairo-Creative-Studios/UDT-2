using NaughtyAttributes;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Rich.Feedbacks
{
    [ExecuteAlways]
    public class Screenshake : Tween
    {
        void OnEnable()
        {
            shakeTween = CreateInstance<Tween>();
            shakeTween.SetTweenedValue<Vector3>(this, "currentOffset");
        }
        
        void OnValidate()
        {
            shakeTween.maxVector3 = Vector3.one * strength;
        }

        protected override void FeedbackStarted()
        {
            controller.AddFeedback(shakeTween);
            cameraTransform = Camera.main.transform;
            shakeTween.speed = speed;
            shakeTween.Play();
        }

        public override void UpdateFeedback()
        {
            currentOffset.x += Random.Range(-strength, strength);
            currentOffset.y += Random.Range(-strength, strength);
            currentOffset.z += Random.Range(-strength, strength);

            cameraTransform.position += currentOffset;
        }
    }
}
