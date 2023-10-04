using System;
using UnityEngine;

namespace UDT.Serialization
{
    public static class SerializationExtensions
    {
        public static byte ConvertToByte(this Vector2 vector)
        {
            int value = 0;

            int x = (int)(vector.x / Mathf.Ceil(vector.x) * 15);
            int y = (int)(vector.y / Mathf.Ceil(vector.y) * 15);

            value |= x << 4;
            value |= y;

            return (byte)value;
        }

        public static Vector2 ConvertToVector2(this byte convertedByte)
        {
            int x = convertedByte >> 4;
            int y = convertedByte;

            y &= ~(1 << 4);
            y &= ~(1 << 5);
            y &= ~(1 << 6);
            y &= ~(1 << 7);

            return new Vector2(x/15f, y/15f);
        }

        
    }
}
