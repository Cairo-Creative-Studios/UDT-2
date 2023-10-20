namespace UDT.Scriptables.Events
{
    [CreateNodeMenu("Collisions/Events/On Instance Trigger Entered")]
    public class OnInstanceTriggerEntered : InstanceEventNode<OnInstanceTriggerEntered>
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