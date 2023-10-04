using NaughtyAttributes;
using UDT.Scriptables;
using UnityEngine;

namespace UDT.Feedbacks
{
    public abstract class Feedback : ScriptableObject, IFeedbackHook
    {
        [Foldout("Feedback Settings")]
        [HideInInspector]
        public FeedbackController controller;
        [Foldout("Feedback Settings")]
        [HideInInspector]
        public bool isPlaying = false;

        [Foldout("Feedback Settings")]
        [Header(" - Scriptable Events - ")]
        [Expandable]
        [AllowNesting]
        public ScriptableObjectAsset<ScriptableEvent> OnFeedbackStarted;
        [Foldout("Feedback Settings")]
        [Expandable]
        [AllowNesting]
        public ScriptableObjectAsset<ScriptableEvent> OnFeedbackComplete;

        [Foldout("Feedback Settings")]
        [Tooltip("Multiplied by Time.deltaTime")]
        public float speed = 0.1f;

        protected GameObject Target;
        [Foldout("Feedback Settings")]
        public float timeStarted = 0f;
        [Foldout("Feedback Settings")]
        public float timeElapsed = 0f;

        void Awake()
        {
            //Init events
            OnFeedbackStarted.Verify();
            OnFeedbackComplete.Verify();
        }

        public void SetArguments(params (string, object)[] args)
        {

        }

        protected virtual void FeedbackStarted()
        { }

        public virtual void UpdateFeedback()
        { }

        protected virtual void FeedbackStopped()
        { }

        public void Play()
        {
            isPlaying = true;
            timeStarted = Time.time;
            OnFeedbackStarted.GetScriptableAs<ScriptableEvent>().Invoke();
            FeedbackStarted();
        }

        public void Stop()
        {
            isPlaying = false;
            OnFeedbackComplete.GetScriptableAs<ScriptableEvent>().Invoke();
            FeedbackStopped();
        }
    }
    public class Feedback<T> : Feedback, IFeedbackHook where T : Feedback<T>
    {
        public static T CreateFeedback(GameObject target)
        {
            var feedback = CreateInstance<T>();
            feedback.Target = target;
            return feedback;
        }
    }

    public interface IFeedbackHook
    {

    }
}
