using Rich.Scriptables.Utilities;
using Rich.Scriptables.Variables;

namespace Rich.Scriptables.Events
{
    public class OnInstanceDestroyed : ScriptableEvent<OnInstanceDestroyed>
    {
        public override void OnInvoked(params object[] args)
        {
        }

        private void OnEnable()
        {
            if(Event == null)
                Event = this;
            else
                Event.AddListener((object[] args) => Invoke(args));

            parameters = new ScriptableVariable[] { new ScriptableVar_Instance() };
        }
    }
}