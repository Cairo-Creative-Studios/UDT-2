using UDT.Scriptables.Utilities;
using System;
using XNode;
using UDT.Scriptables.Variables;

namespace UDT.Scriptables.Conditions
{
    [CreateNodeMenu("Conditions/Input/Input Action Is In Phase")]
    public class InputActionIsInPhase : ConditionNode
    {
        public InputAction inputAction;
        public InputActionPhase phase;
        protected override bool OnCheck()
        {
            return inputAction.Value.phase == phase.Value;
        }
    }
}