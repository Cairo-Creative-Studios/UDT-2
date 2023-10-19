using UnityEngine.InputSystem;
using UDT.Scriptables.Utilities;
using NaughtyAttributes;
using XNode;

namespace UDT.Scriptables.Actions
{
	public class ForLoop : ActionNode
	{
		[Input (backingValue = ShowBackingValue.Unconnected)] public Variables.Int Start;
		[Input (backingValue = ShowBackingValue.Unconnected)] public Variables.Int End;
		[Output (backingValue = ShowBackingValue.Unconnected)] public Variables.Int Index;

		public override void Process()
		{
			for(Index.Value = 0; Index.Value < End.Value; Index.Value++)
			{
				base.Process();
			}
		}
	}
}