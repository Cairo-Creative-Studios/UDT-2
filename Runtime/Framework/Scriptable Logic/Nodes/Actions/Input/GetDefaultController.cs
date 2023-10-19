using UnityEngine.InputSystem;
using UDT.Scriptables.Utilities;
using NaughtyAttributes;
using XNode;
using UDT.Controllables;

namespace UDT.Scriptables.Actions
{
	public class GetDefaultController : ActionNode
	{
		[Output] public Controller controller;

		public override void Process()
		{
			controller = ControllerManager.DefaultController;

			base.Process();
		}
	}
}