using UDT.Scriptables.Utilities;
using UDT.Scriptables.Variables;
using XNode;
using UDT.Controllables.Serialized;
using NaughtyAttributes;
using UnityEngine;

namespace UDT.Scriptables.Events
{
    [CreateNodeMenu("Events/Audio/On Music Play")]
    public class OnMusicPlay : EventNode<OnMusicPlay>
    {
        [Output] public Variables.AudioClip audioClip;  

        public override void OnInvoked(params object[] args)
        {
        }

        private new void OnEnable()
        {
            base.OnEnable();

            if(Event == null)
                Event = this;
            else
                Event.AddListener((object[] args) => Invoke(args));
        }
    }
}