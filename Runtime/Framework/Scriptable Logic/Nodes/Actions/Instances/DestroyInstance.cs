using UDT.Scriptables.Utilities;
using UnityEngine;
using System.Collections.Generic;

namespace UDT.Scriptables.Actions
{
    [CreateNodeMenu("Instances/Actions/Destroy Instance")]
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