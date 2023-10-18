using UDT.Scriptables.Utilities;
using NaughtyAttributes;
using UnityEngine;
using UDT.Menus;

namespace UDT.Scriptables.Actions
{
    public class ShowMenu : ActionNode
    {
        [Dropdown("GetMenus")]
        public MenuBase Menu;
        [Output] public MenuBase createdMenu;

        public override void Process()
        {
            createdMenu = MenuManager.OpenMenu(Menu);
            base.Process();
        }

        public DropdownList<MenuBase> GetMenus()
        {
            return MenuManager.MenusDropdownList;
        }
    }
}