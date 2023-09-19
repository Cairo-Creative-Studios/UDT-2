using Rich.Controllables;
using UnityEngine;
using UnityEngine.UIElements;

namespace Rich.Menus
{
    public class MenuBase : ScriptableObject
    {
        private VisualTreeAsset asset;
    }
    public class Menu<T> : ScriptableObject where T : Menu<T>, IControllable
    {

        public static T Open()
        {
            var menu = CreateInstance<T>();
            return menu;
        }
    }
}