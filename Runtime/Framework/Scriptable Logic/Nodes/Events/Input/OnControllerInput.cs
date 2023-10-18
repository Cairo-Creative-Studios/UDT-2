namespace UDT.Scriptables.Events
{
    [CreateNodeMenu("Events/Input/On Controller Input")]
    public class OnControllerInput : EventNode<OnControllerInput>
    {
        [Output] public Variables.InputAction inputAction;

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