
namespace UDT.Scriptables.Events
{
    [CreateNodeMenu("Events/Timing/On Timer Started")]
    public class OnTimerStarted : EventNode<OnTimerStarted>
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