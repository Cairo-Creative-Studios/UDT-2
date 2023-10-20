using UDT.Scriptables.Utilities;

namespace UDT.Scriptables.Actions
{
    [CreateNodeMenu("For Each")]
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