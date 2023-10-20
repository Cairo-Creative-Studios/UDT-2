
using UDT.Scriptables.Utilities;

namespace UDT.AI
{
    [CreateNodeMenu("AI/Force Update")]
    public class ForceUpdate : ActionNode
    {
        public override void Process()
        {
            ((AIGraph)graph).UpdateScript();
            base.Process();
        }
    }
}