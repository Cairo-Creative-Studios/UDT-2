using System.Collections.Generic;
using UnityEngine;

namespace UDT.Scriptables.Utilities
{
    public class ScriptableNode : ScriptableObject
    {
        public List<ScriptableObjectAsset> children = new();
        public List<ScriptableObjectAsset> conditions = new();

        public void Process(params object[] args)
        {
            if(conditions.Count == 0)
            {
                children.ForEach(x => x.GetScriptableAs<ScriptableNode>().Process(args));
            }
            else
            {
                int metConditions = 0;
                foreach(var condition in conditions)
                {
                    if(condition.GetScriptableAs<ScriptableCondition>().Check(metConditions > 0, args))
                    {
                        metConditions++;
                    }
                }
                if(metConditions == conditions.Count)
                {
                    children.ForEach(x => x.GetScriptableAs<ScriptableNode>().Process(args));
                }
            }
        }
    }
}