using UnityEngine.InputSystem;
using UDT.Scriptables.Utilities;
using NaughtyAttributes;

namespace UDT.Scriptables.Actions
{
	public class ForEachLoop : ActionNode
	{
		[Input] public Variables.NodeList list;
		[Output (backingValue = ShowBackingValue.Unconnected)] public Variables.GenericObject currentItem;

		public override void Process()
		{
			foreach(var item in list.Value)
			{
				currentItem.Value = item.Value;
				base.Process();
			}
		}
	}
}