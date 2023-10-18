namespace UDT.Scriptables.Events
{
    [CreateNodeMenu("Events/Instances/On Instance Destroyed")]
    public class OnInstanceDestroyed : InstanceEventNode<OnInstanceDestroyed>
    {
        
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