using System.Collections.Generic;
using System.Linq;
using UDT.Scriptables.Utilities;
using UDT.System;
using UnityEngine;
using System;
using JetBrains.Annotations;
using NaughtyAttributes;
using UDT.Instances;
using UDT.Scriptables.Events;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace UDT.Scriptables
{
    /// <summary>
    /// Manages the Scriptable Logic system by providing methods to access and manipulate Scriptable Variables and Events.
    /// </summary>
    public class ScriptableManager : Singleton<ScriptableManager, SystemData>
    {
        private static VariableNode[] scriptableVariableResources;
        private static List<EventGraph> eventGraphs = new();
        private static DropdownList<EventGraph> cachedEventGraphDropdownList;
        private static ScriptableEventObject[] scriptablePrefabs;
        private static List<Timer> timers = new();

        public static DropdownList<EventGraph> EventGraphDropdownList
        {
            get
            {
                if (cachedEventGraphDropdownList == null)
                {
                    DropdownList<EventGraph> graphs = new()
                    {
                        { "None", null }
                    };

                    foreach (var graph in Resources.LoadAll<EventGraph>(""))
                    {
                        graphs.Add(graph.name, graph);
                    }

                    cachedEventGraphDropdownList = graphs;

                    return graphs;
                }
                else
                    return cachedEventGraphDropdownList;
            }
        }


        void Awake()
        {
            scriptableVariableResources = Resources.LoadAll<VariableNode>("");
            scriptablePrefabs = Resources.LoadAll<ScriptableEventObject>("");

            foreach(var graph in eventGraphs)
            {
                if (graph.Global)
                    CreateEventSingleton(graph);
            }

            foreach(var scriptablePrefab in scriptablePrefabs)
            {
                if (scriptablePrefab.runtime)
                    new Instance(scriptablePrefab.gameObject);
            }
        }

        public static void Bind<T>(string variableName, BindMode mode, object listener, string fieldOrPropertyName)
        {
            InstantiateSingleton();

            var variable = scriptableVariableResources.FirstOrDefault(x => x.name == variableName);
            if(variable == null)
            {
                Debug.LogWarning($"No Scriptable Variable with name {variableName} was found, a new one will be created.");
                variable = ScriptableObject.CreateInstance<VariableNode>();
                variable.name = variableName;
            }

            if(mode == BindMode.Get)
                variable?.BindGetter<T>(listener, fieldOrPropertyName);
            else
                variable?.BindSetter<T>(listener, fieldOrPropertyName);

            //return variable as ScriptableVariable<T>;
        }

        public static void Unbind<T>(string variableName, object listener, string fieldOrPropertyName)
        {
            InstantiateSingleton();

            var variable = scriptableVariableResources.FirstOrDefault(x => x.name == variableName);
            if(variable == null)
            {
                Debug.LogWarning($"No Scriptable Variable with name {variableName} was found, nothing to unbind.");
                return;
            }

            variable.Unbind<T>(listener, fieldOrPropertyName);
        }

        /// <summary>
        /// Unbinds a listener from a Scriptable Variable with the given name.
        /// </summary>
        /// <typeparam name="T">The type of the Scriptable Variable.</typeparam>
        /// <param name="variableName">The name of the Scriptable Variable.</param>
        /// <param name="listener">The listener to unbind.</param>
        public static void UnbindAll<T>(string variableName, object listener)
        {
            InstantiateSingleton();

            var variable = scriptableVariableResources.FirstOrDefault(x => x.name == variableName);
            if(variable == null)
            {
                Debug.LogWarning($"No Scriptable Variable with name {variableName} was found, nothing to unbind.");
                return;
            }

            variable.UnbindAll(listener);
        }

        public static void AddEventGraph(EventGraph eventGraph)
        {
            eventGraphs.Add(eventGraph);
        }

        /// <summary>
        /// Creates a Scriptable Event Singleton from the given Graph.
        /// </summary>
        /// <param name="eventGraph"></param>
        public static void CreateEventSingleton(EventGraph eventGraph)
        {
            var gameObject = new GameObject("Event Singleton " + eventGraph.name);
            gameObject.AddComponent<ScriptableEventObject>();
            DontDestroyOnLoad(gameObject);
        }

        public static void StartTimer(string name, float duration)
        {
            timers.Add(new Timer(name, duration));
        }

        public static void StopTimer(string name)
        {
            var timer = timers.FirstOrDefault(x => x.name == name);
            if(timer != null)
            {
                OnTimerEnded.Invoke(name);
                timers.Remove(timers.FirstOrDefault(x => x.name == name));
                return;
            }
            Debug.LogWarning("The Timer you attempted to stop, " + name + " does not exist.");
        }

        public static bool IsTimerRunning(string name)
        {
            var timer = timers.FirstOrDefault(x => x.name == name);
            if (timer != null)
            {
                return true;
            }
            return false;
        }

        public static float GetTimerStatus(string name)
        {
            var timer = timers.FirstOrDefault(x => x.name == name);
            if (timer != null)
            {
                return Mathf.Clamp(timer.duration / timer.currentTime, 0, 1);
            }
            return -1;
        }
    }
    /// <summary>
    /// "Set" will make the Scriptable Variable take the value of the given field or property.
    /// "Get" will make the field or property take the value of the Scriptable Variable.
    /// </summary>
    public enum BindMode
    {
        Set,
        Get
    }
}