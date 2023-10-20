
namespace UDT.Scriptables.Events
{
    [CreateNodeMenu("Timing/Events/On Timer Ended")]
    public class OnTimerEnded : EventNode<OnTimerEnded>
    {
        [Output] public Variables.Runtime runtime;

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