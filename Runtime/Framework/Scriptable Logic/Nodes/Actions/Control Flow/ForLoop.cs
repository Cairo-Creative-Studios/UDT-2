using UnityEngine.InputSystem;
using UDT.Scriptables.Utilities;
using NaughtyAttributes;
using XNode;

namespace UDT.Scriptables.Actions
{
	public class ForLoop : ActionNode
	{
		[Input] public Variables.Int Start;
		[Input] public Variables.Int End;
		[Output] public Variables.Int Index;

		public override void Process()
		{
			for(Index.Value = 0; Index.Value < End.Value; Index.Value++)
			{
				base.Process();
			}
		}
	}
}