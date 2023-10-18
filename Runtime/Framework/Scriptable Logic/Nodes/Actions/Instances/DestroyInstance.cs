using UnityEngine.InputSystem;
using UDT.Scriptables.Utilities;
using UDT.Controllables;
using NaughtyAttributes;
using UDT.Scriptables.Variables;
using XNode;
using JetBrains.Annotations;
using UnityEngine;
using System.Collections.Generic;

namespace UDT.Scriptables.Actions
{
	public class DestroyInstances : ActionNode
	{
		[Input(dynamicPortList = true)] public List<Instances.Instance> instance;

		public override void Process()
		{
			foreach(var instance in instance) 
			{
                instance.Destroy();
			}
			base.Process();
		}
	}
}