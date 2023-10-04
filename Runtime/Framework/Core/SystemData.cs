using UDT.DataContainers;
using UnityEngine.InputSystem;


namespace UDT.System
{
    public sealed class SystemData : Data
    {
        public enum StartupMode
        {
            InitOnRuntimeStart,
            InitManually
        }
        public StartupMode startupMode = StartupMode.InitManually;

        public int PrefabPoolSize = 15;
        public InputActionAsset playerInputActions;
    }
}
