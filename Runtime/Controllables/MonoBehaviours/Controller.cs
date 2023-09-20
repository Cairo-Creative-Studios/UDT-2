using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using Rich.Extensions;
using UnityEngine.Events;

namespace Rich.Controllables
{
    public class Controller : MonoBehaviour
    {
        private List<IControllable> possessingControllables = new();
        private UnityEvent<string, InputAction.CallbackContext> OnControllerInput = new();
        public Dictionary<string, List<InputAction>> inputActions = new();
        private string map = "";

        public void PossessControllable(IControllable controllable)
        {
            possessingControllables.Add(controllable);
            OnControllerInput.AddListener(controllable.OnInput);
            ControllerManager.AddControllable(controllable, map);
        }

        public void PossessGameObject(GameObject gameObject)
        {
            var controllables = gameObject.GetComponentsImplementing<IControllable>();
            foreach(var controllable in controllables)
            {
                PossessControllable(controllable);
            }
        }

        public void UnpossessControllable(IControllable controllable)
        {
            possessingControllables.Remove(controllable);
            OnControllerInput.RemoveListener(controllable.OnInput);
        }

        public void UnpossessGameObject(GameObject gameObject)
        {
            var controllables = gameObject.GetComponentsImplementing<IControllable>();
            foreach(var controllable in controllables)
            {
                UnpossessControllable(controllable);
            }
        }

        public void RemoveAllControllables()
        {
            foreach (var controllable in possessingControllables)
            {
                OnControllerInput.RemoveListener(controllable.OnInput);
            }
        }

        public void SetInputActions(InputActionAsset inputActionAsset, string map)
        {
            this.map = map;
            foreach(var inputAction in inputActionAsset.actionMaps.FirstOrDefault(x => x.name == map).actions)
            {
                inputAction.Enable();
                inputAction.started += OnInput;
                inputAction.performed += OnInput;
                inputAction.canceled += OnInput;

                if(!this.inputActions.ContainsKey(map))
                    this.inputActions.Add(map, new());
                else
                    this.inputActions[map].Add(inputAction);
            }
        }

        public void SetInputAction(string inputActionAssetName, string map)
        {
            ControllerManager.SetInputActions(this, map, inputActionAssetName);
        }

        public void RemoveInputActions()
        {
            foreach(var inputActionList in inputActions.Values)
            {
                foreach(var inputAction in inputActionList)
                {
                    inputAction.started -= OnInput;
                    inputAction.performed -= OnInput;
                    inputAction.canceled -= OnInput;
                }
            }
        }

        public void AddInputAction(InputAction inputAction)
        {
            inputAction.Enable();
            inputAction.started += OnInput;
            inputAction.performed += OnInput;
            inputAction.canceled += OnInput;

            if(!this.inputActions.ContainsKey(map))
                this.inputActions.Add(map, new());
            else
                this.inputActions[map].Add(inputAction);
        }

        public void SetMap(string map)
        {
            this.map = map;
            possessingControllables.ForEach(controllable => ControllerManager.GetControllableDefinition(controllable).map = map);
        }

        private void OnInput(InputAction.CallbackContext context)
        {
            if(inputActions[map].Contains(context.action))
                OnControllerInput.Invoke(map, context);
        }

        ~Controller()
        {
            RemoveAllControllables();
            RemoveInputActions();
        }
    }
}
