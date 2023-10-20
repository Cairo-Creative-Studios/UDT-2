using UDT.Scriptables.Utilities;

namespace UDT.Scriptables.Actions
{
    [CreateNodeMenu("For")]
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