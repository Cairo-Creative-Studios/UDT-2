using UDT.Scriptables.Utilities;
using NaughtyAttributes;
using UnityEngine;
using UDT.Menus;
using UDT.Scriptables.Variables;
using UDT.System;

namespace UDT.Scriptables.Actions
{
    /// <summary>
    /// Gets the Main Runtime of the Game
    /// </summary>
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