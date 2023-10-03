using UnityEngine;
using Rich.Scriptables.Utilities;
using Rich.Controllables;
using UnityEngine.InputSystem;

namespace Rich.Scriptables.Variables
{
    [CreateAssetMenu(fileName = "New InputAction Variable", menuName = "Rich/Scriptable Logic/Variables/InputAction")]
    public sealed class ScriptableVar_InputAction : ScriptableVariable<InputAction>
    {
    }
}
