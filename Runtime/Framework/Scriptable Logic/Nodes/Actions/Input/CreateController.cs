using UnityEngine.InputSystem;
using UDT.Scriptables.Utilities;
using UDT.Controllables;
using NaughtyAttributes;

namespace UDT.Scriptables.Actions
{
	public class CreateController : ActionNode
	{
		public string controllerName = "New Controller";
		[Output] public Controller controller;

		public override void Process()
		{
			controller = ControllerManager.CreateController(controllerName);
			base.Process();
		}
	}
}