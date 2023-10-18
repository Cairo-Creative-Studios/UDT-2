using UnityEngine.InputSystem;
using UDT.Scriptables.Utilities;
using UDT.Controllables;
using NaughtyAttributes;
using System.Collections.Generic;

namespace UDT.Scriptables.Actions
{
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