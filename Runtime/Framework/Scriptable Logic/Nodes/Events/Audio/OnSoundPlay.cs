
namespace UDT.Scriptables.Events
{
    [CreateNodeMenu("Audio/Events/On Sound Play")]
    public class OnSoundPlay : EventNode<OnSoundPlay>
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