using UDT.Serialization;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace UDT.Controllables.Extensions
{
    public static class ControllableSerializationExtensions
    {
        public static byte GetAsByte(this object value)
        {
            switch (value)
            {
                case bool b:
                    return b ? (byte)1 : (byte)0;
                case float f:
                    return (byte)f;
                case Vector2 v:
                    return (byte)v.ConvertToByte();
                default:
                    throw new Exception("Unsupported type");
            }
        }
    }
}
