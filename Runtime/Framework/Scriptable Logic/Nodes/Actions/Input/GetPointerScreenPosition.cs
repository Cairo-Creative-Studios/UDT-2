using UDT.Scriptables.Utilities;
using UDT.Controllables;
using UDT.Scriptables.Variables;

namespace UDT.Scriptables.Actions
{
    public class GetPointerScreenPosition : ActionNode
    {
        [Output] public Vector2 screenPosition;

        public override void Process()
        {
            screenPosition.Value = ControllerManager.TouchController.TouchPosition;
            base.Process();
        }
    }
}