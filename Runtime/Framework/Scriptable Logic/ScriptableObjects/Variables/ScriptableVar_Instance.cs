using UnityEngine;
using UDT.Scriptables.Utilities;
using UDT.Instances;

namespace UDT.Scriptables.Variables
{
    [CreateAssetMenu(fileName = "New Instance Variable", menuName = "Rich/Scriptable Logic/Variables/Instance")]
    public sealed class ScriptableVar_Instance : ScriptableVariable<Instance>
    {
    }
}
