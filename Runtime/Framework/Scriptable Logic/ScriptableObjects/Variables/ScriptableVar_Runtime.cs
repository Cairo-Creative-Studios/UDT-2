using UnityEngine;
using Rich.Scriptables.Utilities;
using Rich.System;

namespace Rich.Scriptables.Variables
{
    [CreateAssetMenu(fileName = "New Runtime Variable", menuName = "Rich/Scriptable Logic/Variables/Runtime")]
    public sealed class ScriptableVar_Runtime : ScriptableVariable<IRuntime>
    {
    }
}
