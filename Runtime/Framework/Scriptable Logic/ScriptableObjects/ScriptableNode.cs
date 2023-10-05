using System.Collections.Generic;
using UnityEngine;
using XNode;

namespace UDT.Scriptables.Utilities
{
    public class ScriptableNode : Node
    {

        [Input(backingValue = Node.ShowBackingValue.Never)] public SequencePort input;
        [Output(backingValue = Node.ShowBackingValue.Never)] public List<SequencePort> output;
        // public void Process(params object[] args)
        // {
        //     if(conditions.Count == 0)
        //     {
        //         children.ForEach(x => x.GetScriptableAs<ScriptableNode>().Process(args));
        //     }
        //     else
        //     {
        //         int metConditions = 0;
        //         foreach(var condition in conditions)
        //         {
        //             if(condition.GetScriptableAs<ScriptableCondition>().Check(metConditions > 0, args))
        //             {
        //                 metConditions++;
        //             }
        //         }
        //         if(metConditions == conditions.Count)
        //         {
        //             children.ForEach(x => x.GetScriptableAs<ScriptableNode>().Process(args));
        //         }
        //     }
        // }
    }
}