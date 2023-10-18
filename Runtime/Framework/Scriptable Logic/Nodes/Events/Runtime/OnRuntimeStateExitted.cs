using UDT.Scriptables.Utilities;
using UDT.Scriptables.Variables;
using XNode;

namespace UDT.Scriptables.Events
{
    [CreateNodeMenu("Events/Runtime/On Runtime Exitted")]
    public class OnRuntimeStateExitted : EventNode<OnRuntimeStateExitted>
    {
        [Output] public Variables.Runtime runtime;
        [Output] public Variables.RuntimeState runtimeState;  
        
        public override void OnInvoked(params object[] args)
        {
        }

        private new void OnEnable()
        {
            base.OnEnable();

            if(Event == null)
                Event = this;
            else
                Event.AddListener((object[] args) => Invoke(args));
        }
    }
}