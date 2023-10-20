using System;
using NaughtyAttributes;
using System.Collections.Generic;
using UDT.Scriptables.Variables;
using UnityEngine.UIElements;

namespace UDT.Scriptables.Events
{
    [CreateNodeMenu("Menus/Events/On Menu Event Callback")]
    public class OnMenuEvent : EventNode<OnMenuEvent>
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
        [Dropdown("GetElements")]
        public int @event;
        [Output] public EventBase eventCallback;
        [Output] public TypeNode eventType;  

        private new void OnEnable()
        {
            base.OnEnable();

            if (Event == null)
                Event = this;
            else
                Event.AddListener((object[] args) => Invoke(args));
        }

        public override void Process()
        {
            if (eventCallback.GetType().IsAssignableFrom(Type.GetType(events[@event])))
            {
                base.Process();
            }
        }

        public DropdownList<int> GetEvents()
        {
            DropdownList<int> returnList = new();

            foreach (var e in events)
            {
                returnList.Add(e, events.IndexOf(e));
            }

            return returnList;
        }
    }
}