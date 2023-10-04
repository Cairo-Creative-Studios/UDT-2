using UnityEngine;
using UDT.Scriptables.Utilities;
using UDT.Controllables;

namespace UDT.Scriptables.Variables
{
    [CreateAssetMenu(fileName = "New Event Variable", menuName = "Rich/Scriptable Logic/Variables/Event")]
    public sealed class ScriptableVar_Event : ScriptableVariable<ScriptableEvent>
    {
    }
}
