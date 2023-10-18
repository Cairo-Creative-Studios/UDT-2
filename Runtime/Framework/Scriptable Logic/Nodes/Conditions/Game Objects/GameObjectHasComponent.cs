using UDT.Scriptables.Utilities;
using XNode;
using UDT.Scriptables.Variables;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEditor.Experimental.GraphView;


namespace UDT.Scriptables.Conditions
{
    [CreateNodeMenu("Conditions/Game Object/Has Component")]
    public class GameObjectHasComponent : ConditionNode
    {
        public GameObject gameObject; 
        public TypeNode componentType;
        protected override bool OnCheck()
        {
            if (gameObject.Value.GetComponent(componentType.Value) != null)
                return true;
            else
                return false;
        }
    }
}