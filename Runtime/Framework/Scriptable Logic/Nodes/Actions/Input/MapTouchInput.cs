using UnityEngine.InputSystem;
using UDT.Scriptables.Utilities;
using NaughtyAttributes;
using XNode;
using UDT.Controllables;

namespace UDT.Scriptables.Actions
{
	public class MapTouchInput : ActionNode
	{
		[Input] public string touchActionName;
		[Input] public string touchDeltaActionName;

		public override void Process()
		{
			var controller = ControllerManager.defaultController;
			base.Process();
		}
	}
}