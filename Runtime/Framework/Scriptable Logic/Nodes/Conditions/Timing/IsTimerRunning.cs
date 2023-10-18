using UDT.Scriptables.Utilities;
using UDT.Scriptables.Variables;

namespace UDT.Scriptables.Conditions
{
    [CreateNodeMenu("Conditions/Timing/Is Timer Running")]
    public class IsTimerRunning : ConditionNode
    {
        [Input(backingValue = ShowBackingValue.Unconnected)] public String timerName;
        protected override bool OnCheck()
        {
            return ScriptableManager.IsTimerRunning(timerName.Value);
        }
    }
}