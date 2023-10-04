using UnityEngine;
using UDT.Scriptables.Utilities;
using UDT.System;

namespace UDT.Scriptables.Variables
{
    [CreateAssetMenu(fileName = "New Runtime Variable", menuName = "Rich/Scriptable Logic/Variables/Runtime")]
    public sealed class ScriptableVar_Runtime : ScriptableVariable<IRuntime>
    {
    }
}
