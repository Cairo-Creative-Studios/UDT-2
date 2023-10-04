using UDT.Scriptables.Utilities;
using System;

namespace UDT.Scriptables.Conditions
{
    public class ScriptableCondition_GameObjectHasComponent : ScriptableCondition
    {
        public Type componentType;
        protected override bool OnCheck(params object[] args)
        {
            var gameObject = (UnityEngine.GameObject)args[0];
            var component = gameObject.GetComponent(componentType);
            return component != null;
        }
    }
}