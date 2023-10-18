using System.Collections.Generic;
using UDT.Scriptables;
using XNode;

namespace UDT.Scriptables.Utilities
{
    [NodeTint("#DBC025")]
	public class ActionNode : SequenceNode
	{
        [Input(backingValue = Node.ShowBackingValue.Never)] public new SequencePort input;
        [Output(backingValue = Node.ShowBackingValue.Never, dynamicPortList = true)] public new List<SequencePort> output;
        
		public override void Process()
		{
			PerformAction();
			base.Process();
		}
		protected virtual void PerformAction()
		{

		}
	}
}