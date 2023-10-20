using System.Collections.Generic;
using XNode;

namespace UDT.Scriptables.Utilities
{
    [NodeTint("#DBC025")]
	public class ActionNode : SequenceNode
	{
        [Input(backingValue = Node.ShowBackingValue.Never)] public new SequenceNode input;
        [Output(backingValue = Node.ShowBackingValue.Never, dynamicPortList = true)] public new List<SequenceNode> output;
        
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