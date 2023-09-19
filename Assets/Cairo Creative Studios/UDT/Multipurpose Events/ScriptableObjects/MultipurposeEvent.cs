using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

namespace Rich.MultipurposeEvents
{
    [Serializable]
    public class MultipurposeEventBase
    {
        public string name = "";
        public List<object> args = new();
        [NonSerialized]
        public bool isInvoked = false;
        public List<MultipurposeEventSubscriber> subscribers = new();

        public virtual void Invoke(params object[] args)
        {
            this.args = args.ToList();
            isInvoked = true;
            
            foreach (var subscriber in subscribers)
            {
                subscriber.OnEvent(this.name, args.ToArray());
            }

            OnInvoke();
        }

        public async Task OnInvoke()
        {
            await Task.Delay(1);
            isInvoked = false;
        }

        public void AddSubscriber(MultipurposeEventSubscriber subscriber)
        {
            subscribers.Add(subscriber);
        }

        public void RemoveSubscriber(MultipurposeEventSubscriber subscriber)
        {
            subscribers.Remove(subscriber);
        }
    }

    [Serializable]
    public class MultipurposeEvent : MultipurposeEventBase
    {
        public Action action;
        public UnityEvent unityEvent;

        public void AddListener(Action action)
        {
            this.action += action;
        }

        public void RemoveListener(Action action)
        {
            this.action -= action;
        }

        public void Clear()
        {
            this.action = null;
        }

        public override void Invoke(params object[] args)
        {
            base.Invoke(args);
            action?.Invoke();
            unityEvent?.Invoke();
        }
    }

    [Serializable]
    public class MultipurposeEvent<T> : MultipurposeEventBase
    {
        public Action<T> action;
        public UnityEvent<T> unityEvent;

        public void AddListener(Action<T> action)
        {
            this.action += action;
        }

        public void RemoveListener(Action<T> action)
        {
            this.action -= action;
        }

        public void Clear()
        {
            this.action = null;
        }

        public override void Invoke(params object[] args)
        {
            base.Invoke(args);
            action?.Invoke((T)args[0]);
            unityEvent?.Invoke((T)args[0]);
        }
    }

    [Serializable]
    public class MultipurposeEvent<T1, T2> : MultipurposeEventBase
    {
        public Action<T1, T2> action;
        public UnityEvent<T1, T2> unityEvent;

        public void AddListener(Action<T1, T2> action)
        {
            this.action += action;
        }

        public void RemoveListener(Action<T1, T2> action)
        {
            this.action -= action;
        }

        public void Clear()
        {
            this.action = null;
        }

        public override void Invoke(params object[] args)
        {
            base.Invoke(args);
            action?.Invoke((T1)args[0], (T2)args[1]);
            unityEvent?.Invoke((T1)args[0], (T2)args[1]);
        }
    }

    [Serializable]
    public class MultipurposeEvent<T1, T2, T3> : MultipurposeEventBase
    {
        public Action<T1, T2, T3> action;
        public UnityEvent<T1, T2, T3> unityEvent;

        public void AddListener(Action<T1, T2, T3> action)
        {
            this.action += action;
        }

        public void RemoveListener(Action<T1, T2, T3> action)
        {
            this.action -= action;
        }

        public void Clear()
        {
            this.action = null;
        }

        public override void Invoke(params object[] args)
        {
            base.Invoke(args);
            action?.Invoke((T1)args[0], (T2)args[0], (T3)args[0]);
            unityEvent?.Invoke((T1)args[0], (T2)args[0], (T3)args[0]);
        }
    }

    [Serializable]
    public class MultipurposeEvent<T1, T2, T3, T4> : MultipurposeEventBase
    {
        public Action<T1, T2, T3, T4> action;
        public UnityEvent<T1, T2, T3, T4> unityEvent;

        public void AddListener(Action<T1, T2, T3, T4> action)
        {
            this.action += action;
        }

        public void RemoveListener(Action<T1, T2, T3, T4> action)
        {
            this.action -= action;
        }

        public void Clear()
        {
            this.action = null;
        }

        public override void Invoke(params object[] args)
        {
            base.Invoke(args);
            action?.Invoke((T1)args[0], (T2)args[0], (T3)args[0], (T4)args[0]);
            unityEvent?.Invoke((T1)args[0], (T2)args[0], (T3)args[0], (T4)args[0]);
        }
    }
}
