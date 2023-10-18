using UDT.Scriptables.Utilities;
using UDT.Scriptables.Variables;
using XNode;
using UDT.Controllables.Serialized;
using NaughtyAttributes;

namespace UDT.Scriptables.Events
{
    [CreateNodeMenu("Events/Audio/On Sound Stop")]
    public class OnSoundStop : EventNode<OnSoundStop>
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