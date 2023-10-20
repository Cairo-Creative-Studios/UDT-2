using UDT.Scriptables.Utilities;
using UDT.Scriptables.Variables;

namespace UDT.Scriptables.Conditions
{
    [CreateNodeMenu("Conditions/Variable/Is Variable Null")]
    public class IsVariableNull : ConditionNode
    {
        [Input] public VariableNode variable;
        protected override bool OnCheck()
        {
            return ((bool)variable.Value);
        }
    }
}