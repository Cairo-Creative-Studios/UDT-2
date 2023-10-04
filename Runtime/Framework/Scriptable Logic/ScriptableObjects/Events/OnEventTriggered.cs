using UDT.Scriptables.Utilities;
using UDT.Scriptables.Variables;

namespace UDT.Scriptables.Events
{
    public class OnEventTriggered : ScriptableEvent<OnEventTriggered>
    {
        public override void OnInvoked(params object[] args)
        {
        }

        private void OnEnable()
        {
            if (Event == null)
                Event = this;
            else
                Event.AddListener((object[] args) => Invoke(args));

            parameters = new ScriptableVariable[] { new ScriptableVar_Event(), new ScriptableVar_ParameterList() };
        }
    }
}