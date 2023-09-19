using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using Rich.System;

namespace Rich.Menus
{
    public class MenuManager : Singleton<MenuManager, MenuSystemData>
    {
        public static UIDocument mainUIDocument;
        
        private GameObject menuRoot;
        private List<MenuBase> menus = new();


        //public static void Replace
    }
}