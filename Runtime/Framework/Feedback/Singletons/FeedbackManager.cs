using Rich.Extensions;
using Rich.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Rich.Feedbacks
{
    public sealed class FeedbackManager : Singleton<FeedbackManager, SystemData>
    {
        [SerializeField]
        private List<FeedbackController> feedbackControllers = new();
        private static Type[] feedbackTypes;

        void Awake()
        {
            feedbackTypes = typeof(IFeedbackHook).GetInterfacingTypes();
        }

        public static T AddFeedback<T>(GameObject gameObject) where T : Feedback
        {
            var feedbackTypeToCreate = feedbackTypes.Where(type => type.IsSubclassOf(typeof(T))).Select(type => (T)Activator.CreateInstance(type)).ToList();
            var createdFeedback = typeof(ScriptableObject).GetMethod("CreateInstance", new Type[] { }).MakeGenericMethod(typeof(T)).Invoke(null, null) as T;

            var feedbackController = gameObject.GetComponent<FeedbackController>();
            if (feedbackController == null)
            {
                feedbackController = gameObject.AddComponent<FeedbackController>();
                singleton.feedbackControllers.Add(feedbackController);
            }

            createdFeedback.controller = feedbackController;
            feedbackController.AddFeedback(createdFeedback);

            return createdFeedback;
        }

        public static T GetFeedback<T>(GameObject gameObject, int nth) where T : Feedback
        {
            var feedbackController = gameObject.GetComponent<FeedbackController>();
            if (feedbackController != null)
            {
                return feedbackController.GetFeedback<T>(nth);
            }
            return null;
        }
    }
}
