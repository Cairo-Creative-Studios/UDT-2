using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using XNode;

namespace UDT.Scriptables.Utilities
{
    public class SequenceNode : Node
    {
        [HideInInspector] public SequenceNode input;
        [HideInInspector] public List<SequenceNode> output;
        [HideInInspector] public MasterNode masterNode;
        [Input] public List<ParameterNode> parameters;
        public new EventGraph graph;

        public virtual void Process()
        {
            if(parameters == null || parameters.Count == 0)
            {
                ProcessChildren();
            }
            else
            {
                int metParameters = 0;

                foreach(var parameter in parameters)
                {
                    if(parameter.ParameterMet(graph.This))
                    {
                        metParameters++;
                    }
                }

                if(metParameters == parameters.Count)
                    ProcessChildren();
            }
        }

        protected void ProcessChildren()
        {
            foreach(var child in output.Select(x => x.output).OfType<SequenceNode>())
            {
                child.Process();
            }
        }
    }
}