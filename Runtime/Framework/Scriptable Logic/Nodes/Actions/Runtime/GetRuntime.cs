using UDT.Scriptables.Utilities;
using UDT.Scriptables.Variables;
using UDT.System;

namespace UDT.Scriptables.Actions
{
    /// <summary>
    /// Gets the Main Runtime of the Game
    /// </summary>
    [CreateNodeMenu("Runtime/Actions/Get the Main Runtime")]
    public class GetRuntime : ActionNode
    {
        [Output] public Runtime runtime;

        public override void Process()
        {
            runtime.Value = Core.Runtimes[0];
            base.Process();
        }
    }
}