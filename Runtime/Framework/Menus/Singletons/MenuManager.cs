using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;
using Rich.System;
using UnityEngine.Events;
using Rich.DataTypes;
using Rich.Controllables;
using UnityEditor;
using System.Collections.Generic;
using System;

namespace Rich.Menus
{
    /// <summary>
    /// The Menu Manager that can be used to open and close "Menu" ScriptableObjects,
    /// Menus contain a VisualTreeAsset that is used to create a VisualElement that is then added to the UI Document
    /// The Menu Manager constructs a Hierarchy Tree of Menus.
    /// </summary>
    public sealed class MenuManager : Singleton<MenuManager, MenuSystemData>
    {
        public UnityEvent<MenuBase> OnMenuOpened;
        public UnityEvent<MenuBase> OnMenuClosed;

        /// <summary>
        /// Returns the Element at the given hierarchyIndex
        /// </summary>
        public VisualElement this[int[] hierarchyIndex]
        {
            get
            {
                foreach(var node in hierarchy.Flatten())
                {
                    if(node.index == hierarchyIndex)
                    {
                        return node.value.menuElement;
                    }
                }
                return null;
            }
        }
        /// <summary>
        /// Returns the Element with the given name
        /// </summary>
        public VisualElement this[string name]
        {
            get
            {
                foreach(var node in hierarchy.Flatten())
                {
                    if(node.value.name == name)
                    {
                        return node.value.menuElement;
                    }
                }
                return null;
            }
        }
        
        private GameObject rootMenuObject;
        private Tree<MenuBase> hierarchy = new();
        private List<MenuHolder> menuControllers = new();

    
        void Awake()
        {
            rootMenuObject = new GameObject("Menu Root");
            var rootUIDocument = rootMenuObject.AddComponent<UIDocument>();

            rootUIDocument.panelSettings = MenuManager.Data.panelSettings;
            rootUIDocument.visualTreeAsset = MenuManager.Data.menuSystemVisualTreeAsset;

            var rootVisualElement = rootUIDocument.rootVisualElement.ElementAt(0);
            rootVisualElement.style.position = Position.Absolute;
            rootVisualElement.style.width = Length.Percent(100);
            rootVisualElement.style.height = Length.Percent(100);

            var rootMenu = ScriptableObject.CreateInstance<MenuBase>();
            rootMenu.menuElement = rootVisualElement;

            hierarchy.Add(rootMenu, index: new int[]{ 0 });
        }

        void Update()
        {
            if (hierarchy == null) return;
            foreach (var menu in hierarchy.Flatten().OfType<MenuBase>())
            {
                menu.UpdateMenu();
            }
        }

        /// <summary>
        /// Adds 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="parent"></param>
        public static T OpenMenu<T>(MenuBase parent = null) where T : MenuBase
        {
            var menu = ScriptableObject.CreateInstance<T>();

            var menuObject = new GameObject(menu.name);
            menuObject.transform.SetParent(singleton.rootMenuObject.transform);
            var menuController = menuObject.AddComponent<MenuHolder>();
            menuController.menu = menu;
            menu.menuController = menuController;

            singleton.hierarchy.Add(menu, parent);
            menu.OpenMenu();
            singleton.OnMenuOpened.Invoke(menu);
            return menu;
        }

        /// <summary>
        /// Close the given Menu, and remove it from the Hierarchy Tree
        /// </summary>
        /// <param name="menu"></param>
        public static void CloseMenu(MenuBase menu)
        {
            singleton.OnMenuClosed.Invoke(menu);
            Destroy(menu.menuController.gameObject);
            menu.CloseMenu();
            singleton.hierarchy.Remove(menu);
            Destroy(menu);
        }

        /// <summary>
        /// Searches the Hierarchy Tree for the Menu with the given type (T), and closes it.
        /// The CloseMenu Method will be called, passing the found Menu to it. 
        /// The CloseMenu will call the CloseMenu Method for the Menu Script, and remove the Menu from the Hierarchy Tree.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="menuData"></param>
        public static void CloseMenu<T>(T menuData = null) where T : Menu<T>, IControllable
        {
            foreach (var menu in singleton.hierarchy.Flatten().OfType<MenuBase>())
            {
                if (menu.GetType() == typeof(T))
                {
                    CloseMenu(menu);
                    return;
                }
            }
        }

        /// <summary>
        /// Removes all Menus from the screen, and destroys their Scriptable Object Instances
        /// </summary>
        public static void ClearMenu()
        {
            if (singleton.hierarchy.Count > 0)
            {
                singleton[new int[] {0}].Clear();

                foreach(var menu in singleton.hierarchy.Flatten().OfType<MenuBase>())
                {
                    CloseMenu(menu);
                }
            }
        }

        /// <summary>
        /// Queries the given Menu's VisualElement for the Element with the given name
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name"></param>
        /// <returns></returns>
        public static T Q<T>(MenuBase menu, string name) where T : VisualElement
        {
            return menu.menuElement.Q<T>(name);
        }

        /// <summary>
        /// Queries all Open Menus' VisualElements for the Element with the given name
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name"></param>
        /// <returns></returns>
        public static T Q<T>(string name) where T : VisualElement
        {
            foreach(var node in singleton.hierarchy.Flatten())
            {
                var element = node.value.menuElement.Q<T>(name);
                if(element != null)
                {
                    return element;
                }
            }
            return null;
        }

        /// <summary>
        /// Get all Elements of Type T from the given Menu's VisualElement
        /// If no Menu is given, all Elements of Type T from all Menus will be returned
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="menu"></param>
        /// <returns></returns>
        public static T[] GetAll<T>(MenuBase menu = null) where T : VisualElement
        {
            List<T> matchingElements = new();
            
            if(menu == null)
            {
                foreach(var node in singleton.hierarchy.Flatten())
                {
                    matchingElements.AddRange(SearchRecursivelyForElementsOfType<T>(node.value.menuElement));
                }
            }
            else
            {
                matchingElements.AddRange(SearchRecursivelyForElementsOfType<T>(menu.menuElement));
            }

            return matchingElements.ToArray();
        }

        /// <summary>
        /// Get all Elements of Type T from the given Menu's VisualElement that match the given Predicate
        /// If no Menu is given, all Elements of Type T, matching the predicate, from all Menus will be returned
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="menu"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public static T[] GetAllMatchingPredicate<T>(MenuBase menu, Predicate<T> predicate) where T : VisualElement
        {
            List<T> matchingElements = new();
            matchingElements.AddRange(GetAll<T>(menu).Where(x => predicate(x)));
            return matchingElements.ToArray();
        }

        private static List<T> SearchRecursivelyForElementsOfType<T>(VisualElement element) where T : VisualElement
        {
            List<T> matchingElements = new();
            foreach(var child in element.Children())
            {
                if(child.GetType().IsSubclassOf(typeof(T)))
                {
                    matchingElements.Add((T)child);
                }
                matchingElements.AddRange(SearchRecursivelyForElementsOfType<T>(child));
            }
            return matchingElements;
        }

        /// <summary>
        /// Returns the Root Element at the given hierarchyIndex
        /// </summary>
        /// <param name="hierarchyIndex"></param>
        /// <returns></returns>
        public static VisualElement GetRootElement(int[] hierarchyIndex)
        {
            return singleton[hierarchyIndex];
        }

        /// <summary>
        /// Returns the Root Element for the Menu with the given name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static VisualElement GetRootElement(string name)
        {
            return singleton[name];
        }

        /// <summary>
        /// Returns the Root Element for the Menu with the given type (T)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static VisualElement GetRootElement<T>() where T : MenuBase
        {
            foreach(var node in singleton.hierarchy.Flatten())
            {
                if(node.value.GetType() == typeof(T))
                {
                    return node.value.menuElement;
                }
            }
            return null;
        }

        /// <summary>
        /// Returns the Menu with the given type (T) from the Menu Hierarchy Tree
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T GetMenu<T>() where T : Menu<T>, IControllable
        {
            foreach(var node in singleton.hierarchy.Flatten())
            {
                if(node.value.GetType() == typeof(T))
                {
                    return node.value as T;
                }
            }
            return null;
        }
    }
}