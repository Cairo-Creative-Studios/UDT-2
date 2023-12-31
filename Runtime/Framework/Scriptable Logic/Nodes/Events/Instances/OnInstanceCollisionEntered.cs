namespace UDT.Scriptables.Events
{
    [CreateNodeMenu("Collisions/Events/On Instance Collision Entered")]
    public class OnInstanceCollisionEntered : InstanceEventNode<OnInstanceCollisionEntered>
    {
        [Output] public Variables.CollisionInfo collisionInfo;
        
        public override void OnInvoked(params object[] args)
        {
        }

        private new void OnEnable()
        {
            base.OnEnable();

            if (Event == null)
                Event = this;
            else
                Event.AddListener((object[] args) => Invoke(args));
        }
    }
}