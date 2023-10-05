using UnityEngine;
using XNode;

namespace UDT.Scriptables.Utilities
{
    public class ScriptableCondition : ScriptableNode
    {
        public enum Operator
        {
            And,
            Or
        }
        public Operator op = Operator.And;
        public bool Invert;
        public object value;

        public bool Check(bool previous, params object[] args)
        {
            return op == Operator.And ? OnCheck(args) == !Invert : previous || OnCheck(args) == !Invert;
        }

        protected virtual bool OnCheck(params object[] args)
        {
            return true;
        }
    }

    public class ScriptableCondition<T> : ScriptableCondition
    {
        public new T value;
    }
}