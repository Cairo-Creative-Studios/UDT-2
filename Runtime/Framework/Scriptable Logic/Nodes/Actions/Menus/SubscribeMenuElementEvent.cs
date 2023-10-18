using System;
using UDT.Scriptables.Utilities;
using NaughtyAttributes;
using UDT.Menus;
using UnityEngine.UIElements;
using System.Collections.Generic;
using UDT.Extensions;
using System.Linq;
using UDT.Scriptables.Events;

namespace UDT.Scriptables.Actions
{
    public class SubscribeMenuElementEvent : ActionNode
    {
        private List<string> events = new List<string>()
        {
            "MouseCaptureEvent",
            "MouseCaptureOutEvent",
            "PointerCaptureEvent",
            "PointerCaptureOutEvent",
            "ClickEvent",
            "DragExitedEvent",
            "DragUpdatedEvent",
            "DragPerformEvent",
            "DragEnterEvent",
            "DragLeaveEvent",
            "GeometryChangedEvent",
            "FocusOutEvent",
            "FocusInEvent",
            "BlurEvent",
            "FocusEvent",
            "InputEvent",
            "KeyDownEvent",
            "KeyUpEvent",
            "MouseDownEvent",
            "MouseUpEvent",
            "MouseMoveEvent",
            "WheelEvent",
            "MouseEnterWindowEvent",
            "MouseLeaveWindowEvent",
            "MouseEnterEvent",
            "MouseLeaveEvent",
            "MouseOverEvent",
            "NavigationMoveEvent",
            "NavigationSubmitEvent",
            "NavigationCancelEvent",
            "AttachToPanelEvent",
            "DetachFromPanelEvent",
            "PointerDownEvent",
            "PointerUpEvent",
            "PointerMoveEvent",
            "PointerEnterEvent",
            "PointerLeaveEvent",
            "PointerOutEvent",
            "PointerStationaryEvent",
            "PointerCancelEvent",
            "TooltipEvent",
            "TransitionRunEvent",
            "TransitionStartEvent",
            "TransitionEndEvent",
            "TransitionCancelEvent"
        };

        [Input] public MenuBase Menu;
        [Dropdown("GetElements")]
        public int[] element;
        [Dropdown("GetEvents")]
        public int callbackEvent;

        private MenuBase cachedMenu;
        private DropdownList<VisualElement> cachedDropdown;

        public override void Process()
        {
            VisualElement instanceElement = GetRecursivelyFromRoot(element, 0, Menu.menuElement);
            
            if (instanceElement != null)
            {
                switch (callbackEvent)
                {
                    case 0:
                        instanceElement.RegisterCallback<MouseCaptureEvent>((evt) => OnMenuEvent.Invoke(evt, evt.GetType()));
                        break;
                    case 1:
                        instanceElement.RegisterCallback<MouseCaptureOutEvent>((evt) => OnMenuEvent.Invoke(evt, evt.GetType()));
                        break;
                    case 2: 
                        instanceElement.RegisterCallback<PointerCaptureEvent>((evt) => OnMenuEvent.Invoke(evt, evt.GetType()));
                        break;
                    case 3:
                        instanceElement.RegisterCallback<PointerCaptureOutEvent>((evt) => OnMenuEvent.Invoke(evt, evt.GetType()));
                        break;
                    case 4:
                        instanceElement.RegisterCallback<ClickEvent>((evt) => OnMenuEvent.Invoke(evt, evt.GetType()));
                        break;
                    case 5:
                        instanceElement.RegisterCallback<DragExitedEvent>((evt) => OnMenuEvent.Invoke(evt, evt.GetType()));
                        break;
                    case 6:
                        instanceElement.RegisterCallback<DragUpdatedEvent>((evt) => OnMenuEvent.Invoke(evt, evt.GetType()));
                        break;
                    case 7:
                        instanceElement.RegisterCallback<DragPerformEvent>((evt) => OnMenuEvent.Invoke(evt, evt.GetType()));
                        break;
                    case 8:
                        instanceElement.RegisterCallback<DragEnterEvent>((evt) => OnMenuEvent.Invoke(evt, evt.GetType()));
                        break;
                    case 9:
                        instanceElement.RegisterCallback<DragLeaveEvent>((evt) => OnMenuEvent.Invoke(evt, evt.GetType()));
                        break;
                    case 10:
                        instanceElement.RegisterCallback<GeometryChangedEvent>((evt) => OnMenuEvent.Invoke(evt, evt.GetType()));
                        break;
                    case 11:
                        instanceElement.RegisterCallback<FocusOutEvent>((evt) => OnMenuEvent.Invoke(evt, evt.GetType()));
                        break;
                    case 12:
                        instanceElement.RegisterCallback<FocusInEvent>((evt) => OnMenuEvent.Invoke(evt, evt.GetType()));
                        break;
                    case 13:
                        instanceElement.RegisterCallback<BlurEvent>((evt) => OnMenuEvent.Invoke(evt, evt.GetType()));
                        break;
                    case 14:
                        instanceElement.RegisterCallback<FocusEvent>((evt) => OnMenuEvent.Invoke(evt, evt.GetType()));
                        break;
                    case 15:
                        instanceElement.RegisterCallback<InputEvent>((evt) => OnMenuEvent.Invoke(evt, evt.GetType()));
                        break;
                    case 16:
                        instanceElement.RegisterCallback<KeyDownEvent>((evt) => OnMenuEvent.Invoke(evt, evt.GetType()));
                        break;
                    case 17:
                        instanceElement.RegisterCallback<KeyUpEvent>((evt) => OnMenuEvent.Invoke(evt, evt.GetType()));
                        break;
                    case 18:
                        instanceElement.RegisterCallback<MouseDownEvent>((evt) => OnMenuEvent.Invoke(evt, evt.GetType()));
                        break;
                    case 19:
                        instanceElement.RegisterCallback<MouseUpEvent>((evt) => OnMenuEvent.Invoke(evt, evt.GetType()));
                        break;
                    case 20:
                        instanceElement.RegisterCallback<MouseMoveEvent>((evt) => OnMenuEvent.Invoke(evt, evt.GetType()));
                        break;
                    case 21:
                        instanceElement.RegisterCallback<WheelEvent>((evt) => OnMenuEvent.Invoke(evt, evt.GetType()));
                        break;
                    case 22:
                        instanceElement.RegisterCallback<MouseEnterWindowEvent>((evt) => OnMenuEvent.Invoke(evt, evt.GetType()));
                        break;
                    case 23:
                        instanceElement.RegisterCallback<MouseLeaveWindowEvent>((evt) => OnMenuEvent.Invoke(evt, evt.GetType()));
                        break;
                    case 24:
                        instanceElement.RegisterCallback<MouseEnterEvent>((evt) => OnMenuEvent.Invoke(evt, evt.GetType()));
                        break;
                    case 25:
                        instanceElement.RegisterCallback<MouseLeaveEvent>((evt) => OnMenuEvent.Invoke(evt, evt.GetType()));
                        break;
                    case 26:
                        instanceElement.RegisterCallback<MouseOverEvent>((evt) => OnMenuEvent.Invoke(evt, evt.GetType()));
                        break;
                    case 27:
                        instanceElement.RegisterCallback<NavigationMoveEvent>((evt) => OnMenuEvent.Invoke(evt, evt.GetType()));
                        break;
                    case 28:
                        instanceElement.RegisterCallback<NavigationSubmitEvent>((evt) => OnMenuEvent.Invoke(evt, evt.GetType()));
                        break;
                    case 29:
                        instanceElement.RegisterCallback<NavigationCancelEvent>((evt) => OnMenuEvent.Invoke(evt, evt.GetType()));
                        break;
                    case 30:
                        instanceElement.RegisterCallback<AttachToPanelEvent>((evt) => OnMenuEvent.Invoke(evt, evt.GetType()));
                        break;
                    case 31:
                        instanceElement.RegisterCallback<DetachFromPanelEvent>((evt) => OnMenuEvent.Invoke(evt, evt.GetType()));
                        break;
                    case 32:
                        instanceElement.RegisterCallback<PointerDownEvent>((evt) => OnMenuEvent.Invoke(evt, evt.GetType()));
                        break;
                    case 33:
                        instanceElement.RegisterCallback<PointerUpEvent>((evt) => OnMenuEvent.Invoke(evt, evt.GetType()));
                        break;
                    case 34:
                        instanceElement.RegisterCallback<PointerMoveEvent>((evt) => OnMenuEvent.Invoke(evt, evt.GetType()));
                        break;
                    case 35:
                        instanceElement.RegisterCallback<PointerEnterEvent>((evt) => OnMenuEvent.Invoke(evt, evt.GetType()));
                        break;
                    case 36:
                        instanceElement.RegisterCallback<PointerLeaveEvent>((evt) => OnMenuEvent.Invoke(evt, evt.GetType()));
                        break;
                    case 37:
                        instanceElement.RegisterCallback<PointerOutEvent>((evt) => OnMenuEvent.Invoke(evt, evt.GetType()));
                        break;
                    case 38:
                        instanceElement.RegisterCallback<PointerStationaryEvent>((evt) => OnMenuEvent.Invoke(evt, evt.GetType()));
                        break;
                    case 39:
                        instanceElement.RegisterCallback<PointerCancelEvent>((evt) => OnMenuEvent.Invoke(evt, evt.GetType()));
                        break;
                    case 40:
                        instanceElement.RegisterCallback<TooltipEvent>((evt) => OnMenuEvent.Invoke(evt, evt.GetType()));
                        break;
                    case 41:
                        instanceElement.RegisterCallback<TransitionRunEvent>((evt) => OnMenuEvent.Invoke(evt, evt.GetType()));
                        break;
                    case 42:
                        instanceElement.RegisterCallback<TransitionStartEvent>((evt) => OnMenuEvent.Invoke(evt, evt.GetType()));
                        break;
                    case 43:
                        instanceElement.RegisterCallback<TransitionEndEvent>((evt) => OnMenuEvent.Invoke(evt, evt.GetType()));
                        break;
                    case 44:
                        instanceElement.RegisterCallback<TransitionCancelEvent>((evt) => OnMenuEvent.Invoke(evt, evt.GetType()));
                        break;
                }
            }
            base.Process();
        }

        private VisualElement GetRecursivelyFromRoot(int[] index, int depth, VisualElement currentElement)
        {
            var childElement = currentElement.Children().ToArray()[index[depth]];
            if(depth < index.Length)
                 return GetRecursivelyFromRoot(index, depth++, childElement);
            else
                return childElement;
        }

        public DropdownList<int[]> GetElements()
        {
            DropdownList<int[]> returnList = new()
            {
                { "None", null }
            };

            if(Menu != cachedMenu || cachedDropdown == null)
            {
                var visualElements = RecursivelySearchElements(new(){0}, Menu.menuElement);

                foreach(var visualElement in visualElements) 
                {
                    returnList.Add(visualElement.Item2.name, visualElement.Item1.ToArray());
                }

                return returnList;
            }
            return returnList;
        }

        private List<(List<int>, VisualElement)> RecursivelySearchElements(List<int> index, VisualElement element)
        {
            List<(List<int>, VisualElement)> searchElements = new();

            foreach(var e in element.Children())
            {
                index[^1]++;
                searchElements.Add((index, e));
            }

            index.Add(0);
            searchElements.AddRange(RecursivelySearchElements(index, element));
            return searchElements;
        }

        public DropdownList<int> GetEvents()
        {
            DropdownList<int> returnList = new();

            foreach(var e in events)
            {
                returnList.Add(e, events.IndexOf(e));
            }

            return returnList;
        }
    }
}