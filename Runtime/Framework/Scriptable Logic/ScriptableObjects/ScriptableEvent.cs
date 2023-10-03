using System;
using System.Collections.Generic;
using System.Linq;
using Rich.Extensions;
using Rich.Scriptables.Events;
using Rich.Scriptables.Utilities;
using UnityEngine;
using UnityEngine.Events;

namespace Rich.Scriptables
{
    [Serializable]
    public class ScriptableEvent : ScriptableObject, IScriptableEventHook
    {
        public UnityEvent<object[]> unityEvent = new();
        public ScriptableVariable[] variables;
        [Tooltip("Parameters that must have matching arguments passed to Invoke the Event (if no Parameters exist in this Array, it will be Invoked regardless of the Arguments passed.")]
        public ScriptableVariable[] parameters;
        public bool blocked = false;

        public void Invoke(params object[] args)
        {
            int matchingArgs = 0;
            List<ScriptableVariable> currentParameters = parameters.ToList();

            foreach (var arg in args)
            {
                var matchingParam = parameters.FirstOrDefault(x => x.GetType().GetGenericArguments()[0] == arg.GetType());
                if(matchingParam != null) 
                {
                    matchingArgs++;
                    matchingParam.Value = arg;
                    currentParameters.Remove(matchingParam);
                }
            }

            this.CallMethod("OnInvoked", args);

            Preprocess(args);
        }

        public string GetDescription()
        {
            return ConstructDescription();
        }   

        protected void Preprocess(params object[] args)
        {
            OnEventTriggered.Event?.Invoke(this, args);
        }

        protected void Process(params object[] args)
        {
            unityEvent.Invoke(args);
        }

        protected virtual string ConstructDescription()
        {
            return "";
        }

        public void Clear()
        {
            unityEvent.RemoveAllListeners();
        }

        public void AddListener(Action action)
        {
            var unityAction = new UnityAction<object[]>((args) => action());
            this.unityEvent.AddListener(unityAction);
        }

        public void RemoveGenericListener(Action action)
        {
            var unityAction = new UnityAction<object[]>((args) => action());
            this.unityEvent.RemoveListener(unityAction);
        }

        public void AddListener<T>(Action<T> action)
        {
            var unityAction = new UnityAction<object[]>((args) => action((T)args[0]));
            this.unityEvent.AddListener(unityAction);
        }

        public void RemoveGenericListener<T>(Action<T> action)
        {
            var unityAction = new UnityAction<object[]>((args) => action((T)args[0]));
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
    }

    public abstract class ScriptableEvent<T> : ScriptableEvent where T : ScriptableEvent<T>
    {
        public static T Event;

        /// <summary>
        /// This Method is called when the Event is used to process child nodes, and the Event has been Invoked.
        /// </summary>
        public abstract void OnInvoked(params object[] args);
    }

    public interface IScriptableEventHook
    {

    }
}
