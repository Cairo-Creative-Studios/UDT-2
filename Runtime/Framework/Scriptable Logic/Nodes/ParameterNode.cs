using System.Collections.Generic;
using XNode;

namespace UDT.Scriptables.Utilities
{
	[NodeTint("#D64AF5")]
	public class ParameterNode : Node
	{
		[Input] public List<VariableNode> variables;

		public bool ParameterMet(object target)
		{
			return DoCheck(target);
		}

		protected virtual bool DoCheck(object target)
		{
			return true;
		}
	}
}