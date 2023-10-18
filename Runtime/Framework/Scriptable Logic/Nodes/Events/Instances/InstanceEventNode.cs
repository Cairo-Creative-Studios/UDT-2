using NaughtyAttributes;
using UDT.Instances;
using UnityEngine;

namespace UDT.Scriptables.Events
{
    public class InstanceEventNode : EventNode<InstanceEventNode>
    {
        [HideInInspector]
        public Instance thisInstance;

        public bool This;
        
        [Output]
        [HideIf("This")]
        public Variables.Instance Instance;

        public override void Process()
        {
            if(This && Instance.Value == thisInstance)
                base.Process();  
            else if(!This)
                base.Process();
        }
    }

    public class InstanceEventNode<T> : InstanceEventNode
    {
        public new static T Event;
    }
}