using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using Rich.Extensions;
using UnityEngine.Events;
using Rich.Controllables.Serialized;
using Rich.Serialization;
using System;
using Rich.Controllables.Extensions;
using UnityEngine.Windows;

namespace Rich.Controllables
{
    public class Controller : MonoBehaviour
    {
        public enum ControllerType
        {
            Local,
            Networked,
            AI
        }
        public ControllerType controllerType;

        public SerializableInputMap inputMap = new();
        public Dictionary<string, List<InputAction>> inputActions = new();

        private List<IControllable> possessingControllables = new();
        private UnityEvent<string, SerializableInput> OnControllerInput = new();
        private string map = "";

        public void PossessControllable(IControllable controllable)
        {
            if(inputActions.Keys.Count == 0)
            {
                Debug.LogError("No Input Actions have been set for this Controller. Please set them before possessing a Controllable.", this);
                return;
            }

            possessingControllables.Add(controllable);
            OnControllerInput.AddListener(controllable.OnInput);
            ControllerManager.AddControllable(controllable, map);
        }

        public void PossessGameObject(GameObject gameObject)
        {
            if (inputActions.Keys.Count == 0)
            {
                Debug.LogError("No Input Actions have been set for this Controller. Please set them before possessing a Controllable.", this);
                return;
            }

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
            if(controllerType == ControllerType.Local)
            {
                foreach (var inputAction in inputActionAsset.actionMaps.FirstOrDefault(x => x.name == map).actions)
                {
                    inputAction.Enable();
                    inputAction.started += OnInput;
                    inputAction.performed += OnInput;
                    inputAction.canceled += OnInput;

                    if (!this.inputActions.ContainsKey(map))
                        this.inputActions.Add(map, new());
                    else
                        this.inputActions[map].Add(inputAction);
                }
            }
            else
            {
                inputMap.SetInputMap(inputActionAsset);
                inputMap.OnInputRecievedExternally.AddListener(OnInput);
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

            if (inputActions[map].Contains(context.action))
            {
                var serializableInput = inputMap.serializableInputActions.FirstOrDefault(x => x.referenceAction == context.action);
                serializableInput.phase = context.phase;
                serializableInput.Value = context.ReadValueAsObject().GetAsByte();
                OnInput(serializableInput);
            }
        }

        private void OnInput(SerializableInput input)
        {
            OnControllerInput.Invoke(map, input);
        }

        /// <summary>
        /// Simulate Input Action
        /// </summary>
        /// <param name="index"></param>
        /// <param name="phase"></param>
        /// <param name="value"></param>
        public void SetInput(int index, int phase, byte value)
        {
            inputMap.RecieveInput(index, phase, value);
        }

        /// <summary>
        /// Simulate Input Action
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name"></param>
        /// <param name="phase"></param>
        /// <param name="value"></param>
        public void SetInput<T>(string name, InputActionPhase phase, T value)
        {
            SetInput(name, phase, value.GetAsByte());
        }

        /// <summary>
        /// Simulate Input Action
        /// </summary>
        /// <param name="name"></param>
        /// <param name="phase"></param>
        /// <param name="value"></param>
        public void SetInput(string name, InputActionPhase phase, byte value)
        {
            inputMap.RecieveInput(inputMap.InputActions.FindIndex(x => x.name == name), (int)phase, value);
        }

        ~Controller()
        {
            RemoveAllControllables();
            RemoveInputActions();
        }
    }
}
