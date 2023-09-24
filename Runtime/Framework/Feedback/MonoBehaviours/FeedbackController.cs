using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Rich.Feedbacks
{
    public class FeedbackController : MonoBehaviour
    {
        [SerializeField]
        private List<Feedback> _feedbacks = new List<Feedback>();

        void Update()
        {
            foreach (var feedback in _feedbacks)
            {
                feedback.UpdateFeedback();
            }
        }

        public void AddFeedback(Feedback feedback)
        {
            _feedbacks.Add(feedback);
        }

        public T GetFeedback<T>(int nth) where T : Feedback
        {
            var _feedbacksOfType = _feedbacks.OfType<T>();
            if (_feedbacksOfType.Count() < nth) return null;
            return (T)_feedbacksOfType.ToArray()[nth];
        }

        public void Play<T>()
        {
            foreach (var feedback in _feedbacks)
            {
                if (feedback is Feedback feedbackT)
                {
                    feedbackT.Play();
                }
            }
        }

        public void Stop<T>()
        {
            foreach (var feedback in _feedbacks)
            {
                if (feedback is Feedback feedbackT)
                {
                    feedbackT.Stop();
                }
            }
        }
    }
}
