using UnityEngine;
using Rich.Scriptables.Utilities;
using Rich.Instances;
using System.Collections.Generic;

namespace Rich.Scriptables.Variables
{
    [CreateAssetMenu(fileName = "New Parameter List Variable", menuName = "Rich/Scriptable Logic/Variables/Parameter List")]
    public sealed class ScriptableVar_ParameterList : ScriptableVariable<List<ScriptableVariable>>
    {
    }
}
