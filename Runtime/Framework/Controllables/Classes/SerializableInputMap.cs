using MemoryPack;
using UDT.Controllables.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace UDT.Controllables.Serialized
{
    public class SerializableInputMap
    {
        /// <summary>
        /// Called when Input is recieved from a Networked Player or AI.
        /// </summary>
        public UnityEvent<SerializableInput> OnInputRecievedExternally = new();
        /// <summary>
        /// Called when Input is set by the Local Controller.
        /// </summary>
        public UnityEvent<InputAction, byte> OnInputSetLocally = new();

        public List<InputAction> InputActions = new();
        public List<SerializableInput> serializableInputActions = new();
        public List<byte[]> serializedInputs = new();

        /// <summary>
        /// Set the Input Map to use to Map Input between the Local Controller, and the Networked Game, or the AI.
        /// </summary>
        /// <param name="inputActions"></param>
        public void SetInputMap(InputActionAsset inputActions, bool simulated = false)
        {
            foreach(var actionMap in inputActions.actionMaps)
            {
                foreach(var action in actionMap.actions)
                {
                    if(!simulated)
                    {
                        action.Enable();
                        action.started += SetInput;
                        action.performed += SetInput;
                        action.canceled += SetInput;
                    }
                    InputActions.Add(action);
                    serializableInputActions.Add(new(actionMap.name, action));
                    serializedInputs.Add(MemoryPackSerializer.Serialize(serializableInputActions.Last()));
                }
            }
        }

        /// <summary>
        /// This is called by the Controller, and the Networked Game, to serialize Input.
        /// </summary>
        /// <param name="context"></param>
        public void SetInput(InputAction.CallbackContext context)
        {
            var actionIndex = InputActions.IndexOf(context.action);
            serializableInputActions[actionIndex].phase = context.phase;
            serializableInputActions[actionIndex].Value = context.action.ReadValueAsObject().GetAsByte();
            var serializedInput = MemoryPackSerializer.Serialize(serializableInputActions[actionIndex]);

            OnInputSetLocally.Invoke(context.action, serializableInputActions[actionIndex].Value);
        }

        /// <summary>
        /// This is called by the Networked Game, or an AI, to simulate Input.
        /// It updates th
        /// </summary>
        /// <param name="bytes"></param>
        public void RecieveInput(int actionIndex, int phase, byte value)
        {
            var serializableInput = serializableInputActions[actionIndex];

            serializableInput.phase = (InputActionPhase)phase;
            serializableInput.Value = value;

            OnInputRecievedExternally.Invoke(serializableInput);
        }
    }
}
