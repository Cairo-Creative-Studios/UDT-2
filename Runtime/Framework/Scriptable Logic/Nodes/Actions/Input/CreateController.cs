using UDT.Scriptables.Utilities;
using UDT.Controllables;

namespace UDT.Scriptables.Actions
{
    [CreateNodeMenu("Input/Actions/Create Controller")]
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