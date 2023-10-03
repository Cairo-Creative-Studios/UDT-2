using UnityEngine;
using Rich.Scriptables.Utilities;
using Rich.Controllables;

namespace Rich.Scriptables.Variables
{
    [CreateAssetMenu(fileName = "New Event Variable", menuName = "Rich/Scriptable Logic/Variables/Event")]
    public sealed class ScriptableVar_Event : ScriptableVariable<ScriptableEvent>
    {
    }
}
