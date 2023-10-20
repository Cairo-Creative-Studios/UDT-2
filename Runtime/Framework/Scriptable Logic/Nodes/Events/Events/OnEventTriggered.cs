namespace UDT.Scriptables.Events
{
    [CreateNodeMenu("On Event Triggered")]
    public class OnEventTriggered : EventNode<OnEventTriggered>
    {
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