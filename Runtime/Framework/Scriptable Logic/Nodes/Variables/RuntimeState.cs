using UnityEngine;
using UDT.Scriptables.Utilities;
using UDT.System;
using XNode;
using UDT.StateMachines;
using NaughtyAttributes;

namespace UDT.Scriptables.Variables
{
    [CreateNodeMenu("Variables/Runtime/Runtime State")]
    public sealed class RuntimeState : VariableNode<(IRuntime, TypeNode)>
    {
        [Output] [ReadOnly] public string RuntimeName;
        [Output] [ReadOnly] public StateMachineManager.StateBase state;
    }
}
