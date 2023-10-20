using UDT.Scriptables.Utilities;
using NaughtyAttributes;
using UDT.Scriptables.Variables;

namespace UDT.Scriptables.Conditions
{
    [CreateNodeMenu("Arithmetic/Conditions/Compare Int")]
    public class CompareInt : ConditionNode
    {
        public enum Operation
        {
            LessThan,
            LessThanOrEqual,
            EqualTo,
            GreatThan,
            GreaterThanOrEqual,
        }
        [Dropdown("GetOperations")]
        public Operation operation;
        public Int a;
        public Int b;

        protected override bool OnCheck()
        {
            switch(operation)
            {
                case Operation.LessThan:return a.Value < b.Value;

                case Operation.LessThanOrEqual: return a.Value <= b.Value;

                case Operation.EqualTo: return a.Value == b.Value;

                case Operation.GreatThan: return a.Value > b.Value;

                case Operation.GreaterThanOrEqual: return a.Value >= b.Value;
            }
            return false;
        }

        public DropdownList<Operation> GetOperations()
        {
            DropdownList<Operation> operations = new()
            {
                { "<", Operation.LessThan },
                { "<=", Operation.LessThanOrEqual },
                { "=", Operation.EqualTo },
                { ">", Operation.GreatThan },
                { ">=", Operation.GreaterThanOrEqual }
            };

            return operations;
        }
    }
}