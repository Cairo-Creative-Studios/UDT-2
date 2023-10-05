using UnityEngine;
using XNode;

namespace UDT.Scriptables
{
    [CreateAssetMenu(menuName = "UDT/Scripting/Event Graph")]
    public class EventGraph : NodeGraph
    {
        public GameObject This;
        public bool Global = false;
        public ScriptableObjectAsset[] nodes;
    }
}