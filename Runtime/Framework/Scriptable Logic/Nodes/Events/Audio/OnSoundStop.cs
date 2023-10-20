
namespace UDT.Scriptables.Events
{
    [CreateNodeMenu("Audio/Events/On Sound Stop")]
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