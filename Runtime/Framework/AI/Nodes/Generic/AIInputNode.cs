using System.Collections.Generic;
using XNode;
using UDT.Scriptables.Variables;

namespace UDT.AI
{
    [CreateNodeMenu("AI/AI Input")]
    public class AIInputNode : Node
    {
        [Input(dynamicPortList = true)] public List<Float> NetworkInputs;
    }
}