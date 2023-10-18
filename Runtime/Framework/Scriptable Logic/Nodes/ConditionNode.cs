using System.Collections.Generic;
using UnityEngine;
using XNode;

namespace UDT.Scriptables.Utilities
{
    [NodeTint("#655ADB")]
    public class ConditionNode : SequenceNode
    {
        [Input(backingValue = Node.ShowBackingValue.Never)] public new SequencePort input;
        [Output(backingValue = Node.ShowBackingValue.Never, dynamicPortList = true)] public new List<SequencePort> output;
        
        public bool Invert;
        public object value;
        [Input] public Variables.GenericObject target;

        public void Check()
        {
            if(OnCheck())
                base.Process();
        }

        protected virtual bool OnCheck()
        {
            base.Process();
            return true;
        }
    }

    public class ConditionNode<T> : ConditionNode
    {
        public new T value;
    }
}