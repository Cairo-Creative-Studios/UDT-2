using UDT.Controllables.Serialized;
using UDT.Extensions;
using UnityEngine;
using UnityEngine.InputSystem;

namespace UDT.Controllables 
{
    public interface IControllable
    {
        public GameObject gameObject { get; }
        public string map { get{ return ControllerManager.GetControllableDefinition(this).map; } set{ ControllerManager.GetControllableDefinition(this).map = value; } }

        public void OnInput(string map, SerializableInput input)
        {
            this.CallMethod($"On_{ input.referenceAction.name }", input.Value);
            this.CallMethod($"On{ input.phase }_{input.referenceAction.name }", input.Value);
        }
    } 
}
