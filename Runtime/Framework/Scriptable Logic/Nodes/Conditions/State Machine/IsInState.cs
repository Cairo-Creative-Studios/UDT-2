using UDT.Scriptables.Utilities;
using System;
using NaughtyAttributes;
using UDT.Scriptables.Variables;
using UDT.StateMachines;

namespace UDT.Scriptables.Conditions
{
    [CreateNodeMenu("State Machine/Conditions/Compare Field")]
    public class IsInState : ConditionNode
    {
        [Input] public Runtime runtime;
        [Dropdown("GetStates")]
        public string state;

        protected override bool OnCheck()
        {
            if (runtime != null && state != "None" && ((IStateMachine)runtime.value).GetCurrentState().GetType().IsAssignableFrom(Type.GetType(state)))
                return true;
            return false;
        }

        public DropdownList<string> GetStates()
        {
            DropdownList<string> states = new()
            {
                { "None", "None" }
            };

            if(runtime != null)
            {
                foreach (var state in ((IStateMachine)runtime.Value).stateReferences.Flatten())
                {
                    states.Add(state.value.type.Name, state.value.type.AssemblyQualifiedName);
                }
            }

            return states;
        }
    }
}