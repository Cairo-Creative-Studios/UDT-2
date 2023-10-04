using UnityEngine;
using UDT.Scriptables.Utilities;
using UDT.Instances;
using System.Collections.Generic;

namespace UDT.Scriptables.Variables
{
    [CreateAssetMenu(fileName = "New Parameter List Variable", menuName = "Rich/Scriptable Logic/Variables/Parameter List")]
    public sealed class ScriptableVar_ParameterList : ScriptableVariable<List<ScriptableVariable>>
    {
    }
}
