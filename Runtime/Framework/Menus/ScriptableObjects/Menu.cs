using UDT.Controllables;
using UnityEngine;
using UnityEngine.UIElements;

namespace UDT.Menus
{
    public class MenuBase : ScriptableObject
    {
        public  VisualTreeAsset menuAsset;
        [HideInInspector] public VisualElement menuElement;
        [HideInInspector] public MenuHolder menuController;

        public virtual void OpenMenu()
        {
        }

        public virtual void UpdateMenu()
        {
        }

        public virtual void CloseMenu()
        {
        }
    }
    public class Menu<T> : ScriptableObject where T : Menu<T>, IControllable
    {

    }
}