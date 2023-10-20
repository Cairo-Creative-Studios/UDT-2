using System.Collections.Generic;
using XNode;
using UDT.Scriptables.Variables;
using UDT.Scriptables.Utilities;

namespace UDT.AI
{
    [CreateNodeMenu("AI/AI Output")]
    public class AIOutputNode : Node
    {
        [Output(backingValue = Node.ShowBackingValue.Never)] public List<SequenceNode> OnUpdated;
        [Output(dynamicPortList = true)] public List<Float> NetworkOutputs;
    }
}