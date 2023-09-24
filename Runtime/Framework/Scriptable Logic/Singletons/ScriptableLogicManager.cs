using System.Collections.Generic;
using System.Linq;
using Rich.Extensions;
using Rich.Menus;
using Rich.Scriptables.Utilities;
using Rich.System;
using UnityEngine;

namespace Rich.Scriptables
{
    /// <summary>
    /// Manages the Scriptable Logic system by providing methods to access and manipulate Scriptable Variables and Events.
    /// </summary>
    public class ScriptableLogicManager : Singleton<ScriptableLogicManager, SystemData>
    {

        private static ScriptableVariable[] scriptableVariableResources;
        private static ScriptableEvent[] scriptableEventResources;
        private List<ScriptableVariable> scriptableVariableInstances = new();

        void Awake()
        {
            scriptableVariableResources = Resources.LoadAll<ScriptableVariable>("");
            scriptableEventResources = Resources.LoadAll<ScriptableEvent>("");
            scriptableVariableInstances.AddRange(scriptableVariableResources);
        }

        void Update()
        {
            foreach(var variable in scriptableVariableInstances.ToArray())
            {
                if(variable == null)
                {
                    scriptableVariableInstances.Remove(variable);
                    continue;
                }
                variable.UpdateValues();
            }
        }

        public static void Bind<T>(string variableName, BindMode mode, object listener, string fieldOrPropertyName)
        {
            InstantiateSingleton();

            var variable = singleton.scriptableVariableInstances.FirstOrDefault(x => x.name == variableName);
            if(variable == null)
            {
                Debug.LogWarning($"No Scriptable Variable with name {variableName} was found, a new one will be created.");
                variable = ScriptableObject.CreateInstance<ScriptableVariable>();
                variable.name = variableName;
                singleton.scriptableVariableInstances.Add(variable);
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