namespace UDT.Scriptables.Events
{
    [CreateNodeMenu("Instances/Events/On Instance Update")]
    public class OnInstanceUpdate : InstanceEventNode<OnInstanceUpdate>
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