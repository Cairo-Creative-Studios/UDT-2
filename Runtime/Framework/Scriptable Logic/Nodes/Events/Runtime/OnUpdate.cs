using UDT.Scriptables.Utilities;
using UDT.Scriptables.Variables;
using XNode;

namespace UDT.Scriptables.Events
{
    [CreateNodeMenu("Events/On Update")]
    public class OnUpdate : EventNode<OnUpdate>
    {
        [Output] public Variables.Runtime runtime;  
        
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