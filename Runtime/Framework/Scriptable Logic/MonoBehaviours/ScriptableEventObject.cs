using NaughtyAttributes;
using UnityEngine;

namespace UDT.Scriptables
{
    public class ScriptableEventObject : MonoBehaviour
    {
        [Expandable]
        public ScriptableEventSheet sheet;
    }
}