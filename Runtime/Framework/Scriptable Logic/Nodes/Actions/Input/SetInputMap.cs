using UnityEngine.InputSystem;
using UDT.Scriptables.Utilities;
using NaughtyAttributes;
using XNode;
using UDT.Controllables;

namespace UDT.Scriptables.Actions
{
	public class SetInputMap : ActionNode
	{
		[Input] public Controller controller;
		[Dropdown("GetControllersMaps")]
		public string map;

		public override void Process()
		{
			base.Process();
		}

		public DropdownList<string> GetControllersMaps()
		{
			DropdownList<string> returnList = new();

			returnList.Add("None", "None");

			foreach(var actionMap in controller.inputActions.Keys)
			{
				returnList.Add(actionMap, actionMap);
			}

			return returnList;
		}
	}
}