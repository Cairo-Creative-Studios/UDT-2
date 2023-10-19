using UnityEngine;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Linq;
using UnityEngine.InputSystem;
using UDT.System;
using System;
using NaughtyAttributes;

namespace UDT.Controllables
{
    public sealed class ControllerManager : Singleton<ControllerManager, SystemData>
    {
        private static DropdownList<InputActionAsset> cachedInputActionAssetList;
        public static DropdownList<InputActionAsset> InputActionAssetDropdownList
        {
            get
            {
                if (cachedInputActionAssetList == null)
                {
                    DropdownList<InputActionAsset> returnList = new();
                    var actionAssets = Resources.LoadAll<InputActionAsset>("Input");

                    foreach (var asset in actionAssets)
                    {
                        returnList.Add(asset.name, asset);
                    }

                    return returnList;
                }
                else
                    return cachedInputActionAssetList;
            }
        }

        public static Controller DefaultController;
        public static Controller TouchController;
        private List<Controller> controllers = new();
        private Dictionary<IControllable, ControllableDefinition> controllables = new();
        private InputActionAsset[] inputActionAssets;

        void Awake()
        {
            inputActionAssets = Resources.LoadAll<InputActionAsset>("");
            DefaultController = CreateController("default");
            touchController = CreateController("Touch");

            foreach (var inputAsset in inputActionAssets)
            {
                foreach(var actionMap in inputAsset.actionMaps)
                {
                    foreach(var action in actionMap.actions)
                    {
                        DefaultController.AddInputAction(action);
                        controllers.Add(DefaultController);
                    }
                }
            }

            touchController.useForTouch = true;
            foreach(var actionMap in Data.tounchControlsAsset.actionMaps)
            {
                foreach(var action in actionMap.actions)
                {
                    DefaultController.AddInputAction(action);
                    controllers.Add(DefaultController);
                }
            }
        }

        public static void AddControllable(IControllable controllable, string map)
        {
            if(singleton.controllables.ContainsKey(controllable))
            {
                singleton.controllables[controllable].map = map;
                return;
            }
            singleton.controllables.Add(controllable, new(map));
        }

        public static void RemoveControllable(IControllable controllable)
        {
            singleton.controllables.Remove(controllable);
        }

        public static ControllableDefinition GetControllableDefinition(IControllable controllable)
        {
            return singleton.controllables[controllable];
        }

        public static Controller CreateController(string name)
        {
            var controller = new GameObject(name + "_controller").AddComponent<Controller>();
            singleton.controllers.Add(controller);
            return controller;
        }

        public static Controller DestroyController(Controller controller)
        {
            singleton.controllers.Remove(controller);
            Destroy(controller.gameObject);
            return controller;
        }

        public static void SetInputActions(Controller controller, string map, string assetName)
        {
            controller.SetInputActions(singleton.inputActionAssets.FirstOrDefault(x => x.name == map), assetName);
        }


        public static InputAction GetInput(string inputName, int controllerIndex = -1)
        {
            if(controllerIndex == -1)
            {
                foreach(var controller in singleton.controllers)
                {
                    foreach(var inputActionList in controller.inputActions.Values)
                    {
                        foreach(var inputAction in inputActionList)
                        {
                            if(inputAction.name == inputName)
                            {
                                return inputAction;
                            }
                        }
                    }
                }
            }
            else
            {
                var controller = singleton.controllers[controllerIndex];
                foreach(var inputActionList in controller.inputActions.Values)
                {
                    foreach(var inputAction in inputActionList)
                    {
                        if(inputAction.name == inputName)
                        {
                            return inputAction;
                        }
                    }
                }
            }

            return null;
        }

        public static T GetInputValue<T>(string inputName, int controllerIndex = -1)
        {
            return (T)GetInput(inputName, controllerIndex).ReadValueAsObject();
        }

        public static bool InputWasPressed(string inputName, int controllerIndex = -1)
        {
            var input = GetInput(inputName, controllerIndex);
            if(input == null)
                return false;
            return input.phase == InputActionPhase.Started;
        }

        public static bool InputWasReleased(string inputName, int controllerIndex = -1)
        {
            return GetInput(inputName, controllerIndex).phase == InputActionPhase.Canceled;
        }

        public static bool InputIsDown(string inputName, int controllerIndex = -1)
        {
            return GetInput(inputName, controllerIndex).phase == InputActionPhase.Performed;
        }
    
        public class ControllableDefinition
        {
            public string map;
            public ControllableDefinition(string map)
            {
                this.map = map;
            }
        }
    }
}
