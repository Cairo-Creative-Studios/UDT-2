using System;

namespace UDT.Factories
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = false, AllowMultiple = true)]
    public class PrefabComponentAttribute : Attribute
    {
        public Type type;
        
        public PrefabComponentAttribute(Type componentType)
        {
            type = componentType;
        }
    }
}