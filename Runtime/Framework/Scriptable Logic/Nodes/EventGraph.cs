using System;
using System.Collections.Generic;
using UnityEngine;
using XNode;
using NaughtyAttributes;
using UDT.Scriptables.Utilities;
using UDT.Scriptables.Events;
using UDT.Instances;
using UDT.Extensions;

namespace UDT.Scriptables
{
    [CreateAssetMenu(menuName = "UDT/Scripting/Event Graph")]
    public class EventGraph : NodeGraph
    {
        [HideInInspector]
        public Instance This;
        [HideInInspector]
        public bool Global = false;
        public List<VariableNodeExpander> variables;
        public List<InstanceEventNode> thisInstanceEventNodes;
        private List<OnUpdate> updateNodes = new();
        private MasterNode masterNode;

        void OnValidate()
        {
            updateNodes = new();

            // Find the MasterNode in the list of nodes
            foreach(var node in nodes)
            {
                if(node is MasterNode)
                {
                    var masterNode = ((MasterNode)node);
                    // Validate the master node
                    masterNode.OnValidate();
                    // Copy the variables from the master node
                    variables = new();
                    foreach(var variable in masterNode.LocalVariables)
                    {
                        variables.Add(new VariableNodeExpander(variable));
                    }
                }
                if(node is SequenceNode)
                {
                    ((SequenceNode)node).masterNode = masterNode;
                }
                if(node is EventNode)
                {
                    var _event = node as EventNode;
                    _event.AddListener(_event.OnInvoked);
                }

                node.CallMethod("OnValidate");
            }

            ScriptableManager.AddEventGraph(this);
        }

        public void InitializeScript()
        {
            
        }

        public void UpdateScript()
        {
        }
        
        [Serializable]
        public struct VariableNodeExpander
        {
            // The variable we are expanding.
            [Expandable]
            [AllowNesting]
            public VariableNode variable;

            public VariableNodeExpander(VariableNode variable)
            {
                this.variable = variable;
            }
        }
    }
}