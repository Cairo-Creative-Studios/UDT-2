using NaughtyAttributes;
using Rich.Scriptables;
using Rich.Scriptables.Utilities;
using Rich.Scriptables.Variables;
using UnityEngine;

namespace Rich.Feedbacks
{
    public abstract class Feedback : ScriptableObject
    {
        [Foldout("Feedback Settings")]
        [HideInInspector]
        public FeedbackController controller;
        [Foldout("Feedback Settings")]
        [HideInInspector]
        public bool isPlaying = false;

        [Foldout("Feedback Settings")]
        [Header(" - Scriptable Events - ")]
        public ScriptableEvent OnFeedbackStarted;
        [Foldout("Feedback Settings")]
        public ScriptableEvent OnFeedbackComplete;

        [Foldout("Feedback Settings")]
        [Header(" - Scriptable Variables - ")]
        [Tooltip("The Scriptable Variable associated with the current Value of the Feedback. You can tie this to a Field or Property in a Script," +
            "and the Feedback and also automatically handle acquiring these values to set.")]
        public ScriptableVariable Value;

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
            OnFeedbackStarted = CreateInstance<ScriptableEvent>();
            OnFeedbackComplete = CreateInstance<ScriptableEvent>();

            Value = Value != null ? Value : CreateInstance<ScriptableVar_Float>();
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
            OnFeedbackStarted.Invoke();
            FeedbackStarted();
        }

        public void Stop()
        {
            isPlaying = false;
            OnFeedbackComplete.Invoke();
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
