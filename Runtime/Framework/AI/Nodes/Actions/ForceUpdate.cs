
using UDT.Scriptables.Utilities;

namespace UDT.AI
{
    public class ForceUpdate : ActionNode
    {
        public override void Process()
        {
            ((AIGraph)graph).UpdateScript();
            base.Process();
        }
    }
}