using UnityEngine;
using Rich.Scriptables.Utilities;
using Rich.Instances;

namespace Rich.Scriptables.Variables
{
    [CreateAssetMenu(fileName = "New Instance Variable", menuName = "Rich/Scriptable Logic/Variables/Instance")]
    public sealed class ScriptableVar_Instance : ScriptableVariable<Instance>
    {
    }
}
