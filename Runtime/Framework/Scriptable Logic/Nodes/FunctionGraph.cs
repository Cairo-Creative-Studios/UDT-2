using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace UDT.Scriptables.Utilities
{
    public class FunctionGraph : EventGraph
    {
        public FunctionMasterNode functionMasterNode;
        void GetMasterNode()
        {
            var functionMasterNodesInGraph = nodes.OfType<FunctionMasterNode>();
            if (functionMasterNodesInGraph == null || functionMasterNodesInGraph.Count() == 0)
            {
                Debug.LogError("This Function can not be called because it does not have a Function Master Node,", this);
            }
            else
                functionMasterNode = functionMasterNodesInGraph.ToArray()[0];
        }

        /// <summary>
        /// Calls the given function graph. The Values that are hooked up to the Action Node calling the Function will be passed to the Function Graph
        /// </summary>
        /// <param name="parameters"></param>
        public void Call(SerializableDictionary<string, object> parameters)
        {
            GetMasterNode();
            foreach(var parameter in functionMasterNode.parameters.Keys)
            {
                if(parameters.ContainsKey(parameter))
                {
                    functionMasterNode.parameters[parameter] = functionMasterNode.parameters[parameter];
                }
            }
        }
    }
}