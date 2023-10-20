using UDT.Scriptables.Utilities;
using UDT.Scriptables.Variables;

namespace UDT.Scriptables.Actions
{
    /// <summary>
    /// Gets the Main Runtime of the Game
    /// </summary>
    [CreateNodeMenu("Timers/Actions/Start Timer")]
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