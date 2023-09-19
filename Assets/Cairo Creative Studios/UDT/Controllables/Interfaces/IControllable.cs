using Rich.Extensions;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Rich.Controllables 
{
    public interface IControllable
    {
        public GameObject gameObject { get; }
        public string map { get{ return ControllerManager.GetControllableDefinition(this).map; } set{ ControllerManager.GetControllableDefinition(this).map = value; } }
        public void OnInput(string map, InputAction.CallbackContext context)
        {
            if(map != "" && map != this.map)
                return;

            this.CallMethod("OnInput", context);
            this.CallMethod($"On_{ context.action.name }", context.ReadValueAsObject());
            this.CallMethod($"On{ context.action.phase }_{ context.action.name }");
        }
    } 
}
