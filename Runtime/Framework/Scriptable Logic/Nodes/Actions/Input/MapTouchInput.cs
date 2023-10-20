using UDT.Scriptables.Utilities;
using UDT.Controllables;

namespace UDT.Scriptables.Actions
{
    [CreateNodeMenu("Input/Actions/Map Input To Touch")]
    public class MapTouchInput : ActionNode
	{
		[Input] public Variables.String touchActionName;
		[Input] public Variables.String touchDeltaActionName;

		public override void Process()
		{
			var controller = ControllerManager.DefaultController;
			base.Process();
		}
	}
}