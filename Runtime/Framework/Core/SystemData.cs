using UDT.DataContainers;
using UnityEngine.InputSystem;
using NaughtyAttributes;

namespace UDT.System
{
    public sealed class SystemData : Data
    {
        public bool EnableVisualScripting = true;

        public enum StartupMode
        {
            InitOnRuntimeStart,
            InitManually
        }
        public StartupMode startupMode = StartupMode.InitManually;

        public int PrefabPoolSize = 15;

        [BoxGroup("Input Settings")]
        public InputActionAsset playerInputActions;

        [BoxGroup("Input Settings")]
        public float LongPressTouchTime = 1f;

        [BoxGroup("Input Settings")]
        public float TouchDistanceForSwipe = 5f;
    }
}
