using NaughtyAttributes;
using Rich.Scriptables;
using System;
using UnityEngine;

namespace Rich.Feedbacks
{
    public class FeedbackTypeHandle : ScriptableObject
    {
        [Dropdown("GetFeedbackTypes")]
        public string feedbackType;
        public ScriptableObjectAsset asset;
        public DropdownList<string> GetFeedbackTypes()
        {
            DropdownList<string> list = new()
                {
                    { "None", "None" }
                };
            foreach (var type in FeedbackManager.feedbackTypes)
            {
                list.Add(type.Name, type.AssemblyQualifiedName);
            }
            return list;
        }

        private string _previousFeedbackType;


        [Button("Play", EButtonEnableMode.Always)]
        public void Play()
        {
            asset.GetScriptableAs<Feedback>().Play();
        }
    }
}