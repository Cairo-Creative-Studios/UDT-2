using UDT.Scriptables.Utilities;
using UDT.Scriptables.Variables;


namespace UDT.Scriptables.Conditions
{
    [CreateNodeMenu("Game Object/Conditions/Has Component")]
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