using UnityEngine;

namespace Rich.Menus
{
    /// <summary>
    /// This class holds a reference to a MenuBase scriptable object and is used by the MenuSystem to get the MenuBase component using GetComponent.
    /// </summary>
    public class MenuHolder : MonoBehaviour
    {
        public MenuBase menu;
        public void Start()
        {
        }
    }
}