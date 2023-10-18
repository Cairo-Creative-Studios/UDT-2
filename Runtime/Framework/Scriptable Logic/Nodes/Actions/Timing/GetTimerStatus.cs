using UDT.Scriptables.Utilities;
using UDT.Scriptables.Variables;

namespace UDT.Scriptables.Actions
{
    /// <summary>
    /// Gets the Main Runtime of the Game
    /// </summary>
    public class GetTimerStatus : ActionNode
    {
        [Input(backingValue = ShowBackingValue.Unconnected)] public String Name;
        [Output] public Float percentageComplete;

        public override void Process()
        {
            percentageComplete.Value = ScriptableManager.GetTimerStatus(Name.Value);
            base.Process();
        }
    }
}