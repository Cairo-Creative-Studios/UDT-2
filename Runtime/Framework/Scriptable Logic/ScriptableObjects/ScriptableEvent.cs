using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Rich.Scriptables.Utilities;
using UnityEngine;
using UnityEngine.Events;

namespace Rich.Scriptables
{
    [Serializable]
    public class ScriptableEvent : ScriptableObject
    {
        public UnityEvent<object[]> unityEvent = new();
        public ScriptableVariable[] variables;

        public void Invoke(params object[] args)
        {
            unityEvent.Invoke(args);
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
}
