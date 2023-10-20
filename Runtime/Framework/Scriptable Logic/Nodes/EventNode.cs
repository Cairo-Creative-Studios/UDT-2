using System;
using System.Collections.Generic;
using System.Linq;
using UDT.Scriptables.Utilities;
using UnityEngine;
using UnityEngine.Events;
using XNode;

namespace UDT.Scriptables
{
    [NodeTint("#DB3B39")]
    [Serializable]
    public abstract class EventNode : SequenceNode, IScriptableEventHook
    {
        [Output(backingValue = Node.ShowBackingValue.Never)] public new List<SequenceNode> output;
        [HideInInspector]
        public UnityEvent<object[]> unityEvent = new();
        [HideInInspector]
        public VariableNode[] variables;
        [HideInInspector]
        public bool blocked = false;
        public static List<EventNode> nodeInstances = new();

        void OnValidate()
        {
            if(!nodeInstances.Contains(this))
                nodeInstances.Add(this);
        }

        public static void Invoke(params object[] args)
        {
            
        }

        public string GetDescription()
        {
            return ConstructDescription();
        }   

        // Preprocess the event args.
        protected void Preprocess(params object[] args)
        {
            // Invoke the event trigger.
            // OnEventTriggered.Event?.Invoke(this, args);
        }

        // This code is a helper function that is used to process the given arguments.
        // This function is called by the Process function, which is called by the UnityEvent.
        public override void Process()
        {
            base.Process();
        }

        protected virtual string ConstructDescription()
        {
            return "";
        }

        public void Clear()
        {
            unityEvent.RemoveAllListeners();
        }

         public void AddListener(Action<object[]> action)
        {
            var unityAction = new UnityAction<object[]>((args) => action(args));
            this.unityEvent.AddListener(unityAction);
        }

        public void RemoveGenericListener(Action action)
        {
            // Convert the Action to a UnityAction so that it can be removed from the UnityEvent.
            var unityAction = new UnityAction<object[]>((args) => action());
            this.unityEvent.RemoveListener(unityAction);
        }

        public void AddListener<T>(Action<T> action)
        {
            // Create a UnityAction for our listener
            var unityAction = new UnityAction<object[]>((args) => action((T)args[0]));

            // Add the listener
            this.unityEvent.AddListener(unityAction);
        }

        public void RemoveGenericListener<T>(Action<T> action)
        {
            // Create a UnityAction for our listener
            var unityAction = new UnityAction<object[]>((args) => action((T)args[0]));

            // Remove the listener
            this.unityEvent.RemoveListener(unityAction);
        }

        public void AddListener<T1, T2>(Action<T1, T2> action)
        {
            var unityAction = new UnityAction<object[]>((args) => action((T1)args[0], (T2)args[1]));
            this.unityEvent.AddListener(unityAction);
        }

        public void RemoveGenericListener<T1, T2>(Action<T1, T2> action)
        {
            var unityAction = new UnityAction<object[]>((args) => action((T1)args[0], (T2)args[1]));
            this.unityEvent.RemoveListener(unityAction);
        }

        public void AddListener<T1, T2, T3>(Action<T1, T2, T3> action)
        {
            var unityAction = new UnityAction<object[]>((args) => action((T1)args[0], (T2)args[1], (T3)args[2]));
            this.unityEvent.AddListener(unityAction);
        }

        public void RemoveGenericListener<T1, T2, T3>(Action<T1, T2, T3> action)
        {
            var unityAction = new UnityAction<object[]>((args) => action((T1)args[0], (T2)args[1], (T3)args[2]));
            this.unityEvent.RemoveListener(unityAction);
        }

        public void AddListener<T1, T2, T3, T4>(Action<T1, T2, T3, T4> action)
        {
            var unityAction = new UnityAction<object[]>((args) => action((T1)args[0], (T2)args[1], (T3)args[2], (T4)args[3]));
            this.unityEvent.AddListener(unityAction);
        }

        public void RemoveGenericListener<T1, T2, T3, T4>(Action<T1, T2, T3, T4> action)
        {
            var unityAction = new UnityAction<object[]>((args) => action((T1)args[0], (T2)args[1], (T3)args[2], (T4)args[3]));
            this.unityEvent.RemoveListener(unityAction);
        }

        public void OnInvoked(params object[] args)
        {
        }
    }

    public abstract class EventNode<T> : EventNode where T : EventNode<T>
    {
        public static T Event;

        /// <summary>
        /// This Method is called when the Event is used to process child nodes, and the Event has been Invoked.
        /// </summary>
        public new virtual void OnInvoked(params object[] args)
        {

        }

        public new static void Invoke(params object[] args)
        {
            for (int i = 0; i < nodeInstances.Count; i++)
            {
                var instance = nodeInstances[i];

                if (i == 0)
                {
                    var argsCopy = args.ToList();
                    var fields = instance.GetType().GetFields();
                    foreach (var field in fields)
                    {
                        var argsOfType = argsCopy.Where(x => x.GetType() == field.GetType()).ToArray();
                        field.SetValue(instance, argsOfType[0]);
                        argsCopy.Remove(argsOfType[0]);
                    }
                    foreach(var field in fields)
                    {
                        var fieldAsVariable = ((VariableNode)field.GetValue(instance));
                        var argsOfType = argsCopy.Where(x => x.GetType() == fieldAsVariable.Value.GetType()).ToArray();
                        fieldAsVariable.Value = argsOfType[0];
                        argsCopy.Remove(argsOfType[0]);
                    }
                }
                else
                {
                    instance = nodeInstances[0];
                }
                instance.OnInvoked(args);
                instance.Process();
            }
        }
    }

    public interface IScriptableEventHook
    {

    }
}
