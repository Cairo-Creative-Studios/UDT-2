using Rich.Scriptables;
using UnityEngine;

namespace Rich.Feedbacks
{
    public class Feedback : ScriptableObject
    {
        public ScriptableEvent OnFeedbackStarted;
        public ScriptableEvent OnFeedbackComplete;

        protected GameObject Target;
        protected bool isPlaying = false;
        protected float timeStarted = 0f;
        protected float timeElapsed = 0f;

        void Awake()
        {
            //Init events
            OnFeedbackStarted = ScriptableEvent.CreateInstance<ScriptableEvent>();
            OnFeedbackComplete = ScriptableEvent.CreateInstance<ScriptableEvent>();
        }

        public void UpdateFeedback()
        {
            if(isPlaying)
            {
                timeElapsed = Time.time - timeStarted;
                Update();
            }
        }

        protected virtual void Update()
        {
        }

        public void Play()
        {
            isPlaying = true;
            timeStarted = Time.time;
            OnFeedbackStarted.Invoke();
        }

        public void Stop()
        {
            isPlaying = false;
            OnFeedbackComplete.Invoke();
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
