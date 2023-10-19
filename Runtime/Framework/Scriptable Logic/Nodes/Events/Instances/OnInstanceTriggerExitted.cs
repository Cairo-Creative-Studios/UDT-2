namespace UDT.Scriptables.Events
{
    [CreateNodeMenu("Events/Instances/On Instance Trigger Exitted")]
    public class OnInstanceTriggerExitted : InstanceEventNode<OnInstanceTriggerExitted>
    {
        [Output] public Variables.Collider collider;
        
        public override void OnInvoked(params object[] args)
        {
        }

        private new void OnEnable()
        {
            base.OnEnable();

            if (Event == null)
                Event = this;
            else
                Event.AddListener((object[] args) => Invoke(args));
        }
    }
}