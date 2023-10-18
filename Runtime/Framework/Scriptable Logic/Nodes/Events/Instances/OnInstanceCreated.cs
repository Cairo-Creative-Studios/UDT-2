namespace UDT.Scriptables.Events
{
    [CreateNodeMenu("Events/Instances/On Instance Created")]
    public class OnInstanceCreated : InstanceEventNode<OnInstanceCreated>
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