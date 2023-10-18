using UDT.Controllables;
using UDT.Scriptables;
using UnityEngine;
using UnityEngine.UIElements;

namespace UDT.Menus
{
    public class MenuBase : ScriptableObject
    {
        public VisualTreeAsset menuAsset;
        public EventGraph menuEventGraph;
        [HideInInspector] public VisualElement menuElement;
        [HideInInspector] public MenuHolder menuController;

        public virtual void OpenMenu()
        {
            //Adds the Event Graph of the Menu to the Menu Controller's Scriptable Event Object Component
            var eventObject = menuController.gameObject.AddComponent<ScriptableEventObject>();
            eventObject.AddState("Menu Scripts");
            eventObject.stateHierarchy["Menu Scripts"].AddScript(Instantiate(menuEventGraph));
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