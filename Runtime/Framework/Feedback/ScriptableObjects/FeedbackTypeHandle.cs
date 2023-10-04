using NaughtyAttributes;
using UDT.Scriptables;
using System;
using UnityEngine;

namespace UDT.Feedbacks
{
    public class FeedbackTypeHandle : ScriptableObject
    {
        [Dropdown("GetFeedbackTypes")]
        public string feedbackType = "";
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


        [Button("Play", EButtonEnableMode.Always)]
        public void Play()
        {
            asset.GetScriptableAs<Feedback>().Play();
        }
    }
}