using Rich.DataContainers;
using UnityEngine.InputSystem;


namespace Rich.System
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
