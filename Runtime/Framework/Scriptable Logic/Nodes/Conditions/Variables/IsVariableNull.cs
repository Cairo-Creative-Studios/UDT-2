using UDT.Scriptables.Utilities;
using UDT.Scriptables.Variables;

namespace UDT.Scriptables.Conditions
{
    [CreateNodeMenu("Variable/Conditions/Is Variable Null")]
    public class IsVariableNull : ConditionNode
    {
        [Input] public VariableNode variable;
        protected override bool OnCheck()
        {
            return ((bool)variable.Value);
        }
    }
}