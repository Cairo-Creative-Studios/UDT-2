
namespace UDT.Scriptables.Events
{
    [CreateNodeMenu("Runtime/Events/On Runtime Update")]
    public class OnRuntimeUpdate : EventNode<OnRuntimeUpdate>
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