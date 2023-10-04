using UDT.Extensions;
using UDT.Scriptables;
using UDT.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace UDT.Feedbacks
{
    public sealed class FeedbackManager : Singleton<FeedbackManager, SystemData>
    {
        [SerializeField]
        private List<FeedbackController> feedbackControllers = new();
        private static Type[] _feedbackTypes;
        public static Type[] feedbackTypes
        {
            get
            {
                _feedbackTypes = _feedbackTypes != null ? _feedbackTypes : typeof(IFeedbackHook).GetInterfacingTypes().Where(x => x != typeof(Feedback) && x.BaseType != typeof(Feedback)).ToArray();
                return _feedbackTypes;
            }
        }

        void Awake()
        {
            _feedbackTypes = typeof(IFeedbackHook).GetInterfacingTypes();
        }

        public static T AddFeedback<T>(GameObject gameObject) where T : Feedback
        {
            var createdFeedbackHandle = new ScriptableObjectAsset<T>();
            var createdFeedback = createdFeedbackHandle.GetScriptableAs<T>();

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

        public static Type[] GetFeedbackType(Type feedbackType)
        {
            var feedbackTypeToCreate = feedbackTypes.Where(type => type.IsSubclassOf(feedbackType));
            return feedbackTypeToCreate.ToArray();
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
