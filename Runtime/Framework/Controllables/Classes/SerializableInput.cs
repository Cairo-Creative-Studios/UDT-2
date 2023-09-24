using MemoryPack;
using System;
using UnityEngine.InputSystem;

namespace Rich.Controllables.Serialized
{
    [MemoryPackable]
    [Serializable]
    public partial class SerializableInput
    {
        public string map;
        public byte Value;

        public enum InputValueType
        {
            Vec2,
            Float,
            Bool
        }
        public InputValueType valueType;
        public InputActionPhase phase;

        [MemoryPackAllowSerialize]
        public InputAction referenceAction;

        public SerializableInput(string map, InputAction referenceAction)
        {
            this.map = map;
            this.referenceAction = referenceAction;
        }
    }
}
