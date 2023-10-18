using System;
using System.Collections.Generic;
using System.Linq;
using UDT.Extensions;
using UDT.Scriptables.Events;
using UDT.Scriptables.Utilities;
using UnityEngine;
using UnityEngine.Events;
using XNode;

namespace UDT.Scriptables.Utilities
{
	[CreateNodeMenu("Master Node")]
	public class MasterNode : Node
	{
		private static MasterNode baseMasterNode;
		public static List<VariableNode> Variables { get => baseMasterNode.LocalVariables; set => baseMasterNode.LocalVariables = value; }
		[SerializeField]
		[Output(dynamicPortList = true)] public List<VariableNode> LocalVariables = new();


		new void OnEnable()
		{
			base.OnEnable();
			baseMasterNode = this;
		}

		public void OnValidate()
		{
			if(baseMasterNode == null)
				baseMasterNode = this;
			else if(baseMasterNode != this)
				Destroy(this);

			Variables = new();
			LocalVariables = new();

			foreach(var node in graph.nodes)
			{
				if(node is VariableNode)
				{
					Variables.Add(node as VariableNode);
				}
			}
			LocalVariables = Variables;
		}

		public void UpdateVariables()
		{
			foreach(var variable in Variables)
			{
				variable.UpdateFromGet();
			}
		}
	}
}