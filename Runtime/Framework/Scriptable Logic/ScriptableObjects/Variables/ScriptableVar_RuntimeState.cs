using UnityEngine;
using Rich.Scriptables.Utilities;
using Rich.System;

namespace Rich.Scriptables.Variables
{
    [CreateAssetMenu(fileName = "New Runtime State Variable", menuName = "Rich/Scriptable Logic/Variables/Runtime State")]
    public sealed class ScriptableVar_RuntimeState : ScriptableVariable<IRuntime>
    {
    }
}
