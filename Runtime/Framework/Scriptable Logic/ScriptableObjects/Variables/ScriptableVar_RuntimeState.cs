using UnityEngine;
using UDT.Scriptables.Utilities;
using UDT.System;

namespace UDT.Scriptables.Variables
{
    [CreateAssetMenu(fileName = "New Runtime State Variable", menuName = "Rich/Scriptable Logic/Variables/Runtime State")]
    public sealed class ScriptableVar_RuntimeState : ScriptableVariable<IRuntime>
    {
    }
}
