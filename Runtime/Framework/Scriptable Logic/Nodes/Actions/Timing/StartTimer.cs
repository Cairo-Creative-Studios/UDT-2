using UDT.Scriptables.Utilities;
using NaughtyAttributes;
using UDT.Menus;
using UDT.Scriptables.Variables;
using UDT.System;

namespace UDT.Scriptables.Actions
{
    /// <summary>
    /// Gets the Main Runtime of the Game
    /// </summary>
    public class StarTimer : ActionNode
    {
        [Input(backingValue = ShowBackingValue.Unconnected)] public String Name;
        [Input(backingValue = ShowBackingValue.Unconnected)] public Float Duration;

        public override void Process()
        {
            ScriptableManager.StartTimer(Name.Value, Duration.Value);
            base.Process();
        }
    }
}