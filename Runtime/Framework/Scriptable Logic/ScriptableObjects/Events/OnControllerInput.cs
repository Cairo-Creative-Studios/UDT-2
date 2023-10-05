using UDT.Scriptables.Utilities;
using UDT.Scriptables.Variables;
using XNode;
using UDT.Controllables.Serialized;

namespace UDT.Scriptables.Events
{
    public class OnControllerInput : ScriptableEvent<OnControllerInput>
    {
        [Output] public SerializableInput inputAction;
        [Input] public bool foo;

        public override void OnInvoked(params object[] args)
        {
        }

        private void OnEnable()
        {
            base.OnEnable();

            if(Event == null)
                Event = this;
            else
                Event.AddListener((object[] args) => Invoke(args));

            parameters = new ScriptableVariable[] { new ScriptableVar_Controller(), new ScriptableVar_InputAction() };
        }

    }
}