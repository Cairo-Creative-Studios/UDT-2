using UnityEngine.InputSystem;
using UDT.Scriptables.Utilities;
using NaughtyAttributes;
using UDT.Controllables;
using UnityEngine;

namespace UDT.Scriptables.Actions
{
    [CreateNodeMenu("Input/Actions/Set Controller's Input Action Asset")]
    public class SetInputActionsAsset : ActionNode
	{
		[Input] public Controller controller;
		[Dropdown("GetInputActionAssets")]
		public InputActionAsset inputActionAsset;
		[Dropdown("GetMaps")]
		public string map;

		public override void Process()
		{
			controller.SetInputActions(inputActionAsset, map);
		}

		public DropdownList<string> GetMaps()
		{
			DropdownList<string> returnList = new();

			returnList.Add("None", "None");

			if(inputActionAsset != null)
			{
				foreach(var actionMap in inputActionAsset.actionMaps)
				{
					returnList.Add(actionMap.name, actionMap.name);
				}
			}

			return returnList;
		}

		public DropdownList<InputActionAsset> GetInputActionAssets()
		{
			return ControllerManager.InputActionAssetDropdownList;
		}

    }
}