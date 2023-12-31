﻿using MemoryPack;
using System;
using UnityEngine.InputSystem;

namespace UDT.Controllables.Serialized
{
    [MemoryPackable]
    [Serializable]
    public partial class SerializableInput : UnityEngine.Object
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

    public class SerializableInput<T> : SerializableInput
    {
        public new T Value;

        public SerializableInput(string map, InputAction referenceAction) : base(map, referenceAction)
        { }
    }
}
