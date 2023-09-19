using UnityEngine;

namespace Rich.MultipurposeEvents
{
    public class MultipurposeEventSubscriber : MonoBehaviour
    {
        public MultipurposeEvent[] events;

        void Start()
        {
            foreach(var @event in events)
            {
                @event.AddSubscriber(this);
            }
        }

        public virtual void OnEvent(string eventname, params object[] args)
        {

        }
    }
}