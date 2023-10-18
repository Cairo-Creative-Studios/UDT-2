using UnityEngine;
using UDT.Scriptables.Utilities;
using UDT.Controllables;
using UnityEngine.InputSystem;
using UDT.Controllables.Serialized;
using XNode;


namespace UDT.Scriptables.Variables
{
    [CreateNodeMenu("Variables/Input/Serializable Input Action")]
    public sealed class InputAction : VariableNode<UnityEngine.InputSystem.InputAction>
    {    }
}
