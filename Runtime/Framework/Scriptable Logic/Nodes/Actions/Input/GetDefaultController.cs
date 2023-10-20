using UDT.Scriptables.Utilities;
using UDT.Controllables;

namespace UDT.Scriptables.Actions
{
    [CreateNodeMenu("Input/Actions/Get Default Controller")]
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