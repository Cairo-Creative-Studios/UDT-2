using UDT.Scriptables.Utilities;
using UDT.Controllables;
using UDT.Scriptables.Variables;
using UnityEngine;

namespace UDT.Scriptables.Actions
{
    public class GetPointerWorldPosition : ActionNode
    {
        [Input(backingValue = ShowBackingValue.Unconnected)] public float depth;
        [Output] public Variables.Vector3 WorldPosition;

        public override void Process()
        {
            var touchPosition = ControllerManager.TouchController.TouchPosition;
            WorldPosition.Value = Camera.main.ScreenToWorldPoint(new UnityEngine.Vector3(touchPosition.x, touchPosition.y, depth));
            base.Process();
        }
    }
}