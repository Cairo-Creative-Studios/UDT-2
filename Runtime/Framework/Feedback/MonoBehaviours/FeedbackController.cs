using NaughtyAttributes;
using Rich.Scriptables;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Rich.Feedbacks
{
    [ExecuteAlways]
    public class FeedbackController : MonoBehaviour
    {
        [SerializeField]
        public List<ScriptableObjectAsset<FeedbackTypeHandle>> _feedbackHandleAssets = new List<ScriptableObjectAsset<FeedbackTypeHandle>>();

        void Update()
        {
            if(!Application.isPlaying)
            {
                for (int i = 0; i < _feedbackHandleAssets.Count; i++)
                {
                    if (_feedbackHandleAssets.Where(x => x.scriptableObject == _feedbackHandleAssets[i].scriptableObject).Count() > 1)
                    {
                        _feedbackHandleAssets[i].Verify();
                    }
                }
            }

            foreach (var feedbackReference in _feedbackHandleAssets.Select(x => x.GetScriptableAs<FeedbackTypeHandle>()))
            {
                if(feedbackReference.feedbackType != "None" && feedbackReference.feedbackType != null)
                {
                    feedbackReference.asset.SetType(feedbackReference.feedbackType);
                    var feedback = feedbackReference.asset.GetScriptableAs<Feedback>(); 

                    if (feedbackReference.asset != null && feedback.isPlaying)
                    {
                        feedback.UpdateFeedback();
                        feedback.timeElapsed = Time.time - feedback.timeStarted;
                        if (feedback.timeElapsed * feedback.speed >= 1f)
                        {
                            feedback.Stop();
                        }
                    }
                    feedback.controller = this;
                }
            }
        }

        [Button("Play", EButtonEnableMode.Always)]
        public void Play()
        {
            _feedbackHandleAssets.Last().GetScriptableAs<FeedbackTypeHandle>().Play();
        }

        public T GetFeedback<T>(int nth) where T : Feedback
        {
            var _feedbacksOfType = _feedbackHandleAssets.Select(x => x.GetScriptableAs<FeedbackTypeHandle>()).OfType<T>();
            if (_feedbacksOfType.Count() < nth) return null;
            return (T)_feedbacksOfType.ToArray()[nth];
        }

        public void AddFeedback(Feedback feedback)
        {
            _feedbackHandleAssets.Add(new ScriptableObjectAsset<FeedbackTypeHandle>());
        }

        public void Play<T>()
        {
            foreach (var feedback in _feedbackHandleAssets.Select(x => x.GetScriptableAs<FeedbackTypeHandle>()).Select(x => x.asset.GetScriptableAs<Feedback>()))
            {
                if (feedback is Feedback feedbackT)
                {
                    feedbackT.Play();
                }
            }
        }

        public void Stop<T>()
        {
            foreach (var feedback in _feedbackHandleAssets.Select(x => x.GetScriptableAs<FeedbackTypeHandle>()).Select(x => x.asset.GetScriptableAs<Feedback>()))
            {
                if (feedback is Feedback feedbackT)
                {
                    feedbackT.Stop();
                }
            }
        }
    }
}
