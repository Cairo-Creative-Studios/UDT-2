using System;

namespace Rich.Factories
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
    public class PrefabNameAttribute : Attribute
    {
        public string name;

        public PrefabNameAttribute(string name)
        {
            this.name = name;
        }
    }
}