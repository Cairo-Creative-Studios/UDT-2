using UDT.Scriptables.Utilities;
using UnityEngine;

namespace UDT.Scriptables.Actions
{
    [CreateNodeMenu("Collision/Actions/Raycast")]
    public class Raycast : ActionNode
    {
        [Input(backingValue = ShowBackingValue.Unconnected)] public Variables.Vector3 origin;
        [Input(backingValue = ShowBackingValue.Unconnected)] public Variables.Vector3 direction;
        [Input(backingValue = ShowBackingValue.Unconnected)] public Variables.Float distance;
        [Input(backingValue = ShowBackingValue.Unconnected)] public Variables.LayerMask layerMask;
        [Output] public Variables.RaycastHitInfo hitInfo;

        public override void Process()
        {
            Physics.Raycast(origin.Value, direction.Value, out hitInfo.value, distance.Value, layerMask.Value);
            base.Process();
        }
    }
}