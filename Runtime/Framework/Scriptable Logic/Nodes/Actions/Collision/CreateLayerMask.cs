using System.Collections.Generic;
using UDT.Scriptables.Utilities;
using UDT.Scriptables.Variables;

namespace UDT.Scriptables.Actions
{
    public class CreateLayerMask : ActionNode
    {
        [Input(dynamicPortList = true)] public List<int> layers = new();
        [Output] public LayerMask layerMask;

        public override void Process()
        {
            int layerMaskByte = 0;
            foreach(int layer in layers)
            {
                layerMaskByte = 1 << layer;
            }
            layerMask.Value = layerMaskByte;
            base.Process();
        }
    }
}