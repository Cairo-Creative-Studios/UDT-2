using UDT.Scriptables.Utilities;

namespace UDT.Scriptables.Actions
{
    [CreateNodeMenu("Call Function")]
    public class CallFunction : ActionNode
    {
        public FunctionGraph function;
        public SerializableDictionary<string, VariableNode> functionParameters = new();

        public override void Process()
        {
            var parameters = new SerializableDictionary<string, object>();

            foreach(var functionParam in functionParameters.Keys)
            {
                parameters.Add(functionParam, functionParameters[functionParam]);
            }

            function.Call(parameters);
            base.Process();
        }
    }
}