
using System.Collections.Generic;
using UDT.Scriptables.Utilities;
using XNode;

namespace UDT.Scriptables
{
    [CreateNodeMenu("Enum")]
    public sealed class Enum : Node
    {
        public List<string> values = new();

        private void OnValidate()
        {
            var eventGraph = ((EventGraph)graph);
            if (!eventGraph.enums.Contains(this))
            {
                eventGraph.enums.Add(this);
            }
        }
    }
}
