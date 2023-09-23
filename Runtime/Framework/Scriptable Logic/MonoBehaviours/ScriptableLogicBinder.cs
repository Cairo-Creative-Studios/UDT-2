using UnityEngine;
using System.Collections.Generic;
using Rich.Scriptables.Utilities;
using System.Linq;
using System;
using System.Reflection;
using NaughtyAttributes;
using Rich.Menus;

namespace Rich.Scriptables
{
    /// <summary>
    /// A MonoBehaviour that binds scriptable variables to their corresponding MonoBehaviour fields and subscribes to scriptable events.
    /// </summary>
    [DisallowMultipleComponent]
    public class ScriptableLogicBinder : MonoBehaviour
    {
        [Tooltip("A list of scriptable event subscriptions. When the event is triggered, the corresponding method on the MonoBehaviour is invoked with the event variables as parameters.")]
        public List<ScriptableEventSubscription> scriptableEventSubscriptions = new();

        /// <summary>
        /// This method is called when the script instance is being loaded.
        /// It iterates through all the scriptable event subscriptions and adds a listener to each of them.
        /// When the event is triggered, it invokes the corresponding method on the MonoBehaviour with the event variables as parameters.
        /// </summary>
        void Awake()
        {
            foreach (var subscription in scriptableEventSubscriptions)
            {
                subscription.scriptableEvent.AddListener(() => subscription.methodName.Invoke(subscription.monoBehaviour, subscription.scriptableEvent.variables.Select(x => x.Value).ToArray()));
            }
        }

        /// <summary>
        /// A class representing a subscription to a scriptable event.
        /// </summary>
        [Serializable]
        public class ScriptableEventSubscription
        {
            /// <summary>
            /// The scriptable event to subscribe to.
            /// </summary>
            public ScriptableEvent scriptableEvent;

            /// <summary>
            /// The MonoBehaviour to bind the event to.
            /// </summary>
            [Dropdown("GetMonoBehaviours")]
            public MonoBehaviour monoBehaviour;

            private MonoBehaviour _previousMonoBehaviour;

            /// <summary>
            /// The method to call when the event is raised.
            /// </summary>
            [Dropdown("GetMethods")]
            public MethodInfo methodName;

            private DropdownList<MonoBehaviour> _cachedMonoBehaviours = new();
            private DropdownList<MethodInfo> _cachedMethods = new();

            /// <summary>
            /// Gets a list of all MonoBehaviours in the scene.
            /// </summary>
            /// <returns>A list of MonoBehaviours.</returns>
            public DropdownList<MonoBehaviour> GetMonoBehaviours()
            {
                if(_cachedMonoBehaviours != null && _cachedMonoBehaviours.Count() > 0)
                    return _cachedMonoBehaviours;

                _cachedMonoBehaviours = new();

                DropdownList<MonoBehaviour> dropdownList = new();
                foreach (var monoBehaviour in FindObjectsOfType<MonoBehaviour>())
                {
                    dropdownList.Add(monoBehaviour.name, monoBehaviour);
                    _cachedMonoBehaviours.Add(monoBehaviour.name, monoBehaviour);
                }
                return dropdownList;
            }

            /// <summary>
            /// Gets a list of all methods on the selected MonoBehaviour.
            /// </summary>
            /// <returns>A list of methods.</returns>
            public DropdownList<MethodInfo> GetMethods()
            {
                if(_previousMonoBehaviour != monoBehaviour)
                {
                    _cachedMethods = null;
                    _previousMonoBehaviour = monoBehaviour;
                }

                if(_cachedMethods != null && _cachedMethods.Count() > 0)
                    return _cachedMethods;

                _cachedMethods = new();

                DropdownList<MethodInfo> dropdownList = new();

                foreach (var method in monoBehaviour.GetType().GetMethods())
                {
                    dropdownList.Add(method.Name, method);
                    _cachedMethods.Add(method.Name, method);
                }
                return dropdownList;
            }
        }
    }
}