namespace UDT.Scriptables.Events
{
    [CreateNodeMenu("Input/Events/On Controller Gesture")]
    public class OnControllerGesture : EventNode<OnControllerGesture> 
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