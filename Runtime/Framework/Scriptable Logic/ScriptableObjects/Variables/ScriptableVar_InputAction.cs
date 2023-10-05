using UnityEngine;
using UDT.Scriptables.Utilities;
using UDT.Controllables;
using UnityEngine.InputSystem;
using UDT.Controllables.Serialized;


namespace UDT.Scriptables.Variables
{
    [CreateAssetMenu(fileName = "New InputAction Variable", menuName = "Rich/Scriptable Logic/Variables/InputAction")]
    public sealed class ScriptableVar_InputAction : ScriptableVariable<SerializableInput>
    {    }
}
