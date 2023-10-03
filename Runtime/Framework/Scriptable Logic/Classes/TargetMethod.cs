using Rich.Scriptables.Utilities;
using Rich.System;
using System;

namespace Rich.Scriptables
{
    /// <summary>
    /// Target Methods search for a Target with the given Type T, based on the Scriptable Parameters
    /// that are passed to the Search Function
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class TargetMethod<TMethod, TValue> where TMethod : TargetMethod<TMethod, TValue>, new()
    {
        private static TMethod _method;
        public static TMethod method
        {
            get
            {
                if(_method == null)
                {
                    _method = new TMethod();
                }
                return _method;
            }
        }

        public static TValue[] Search(params ScriptableCondition[] parameters)
        {
            return method.search(parameters);
        }

        public abstract TValue[] search(params ScriptableCondition[] parameters);
    }
}