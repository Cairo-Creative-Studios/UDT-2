using UDT.Scriptables.Events;
using UnityEngine;

namespace UDT.Instances
{
    public class CollisionEventTracker : MonoBehaviour
    {
        void OnCollisionEnter(UnityEngine.Collision collision)
        {
            OnInstanceCollisionEntered.Invoke(Instance.GetAsInstance(gameObject), collision);
        }
        void OnCollisionExit(UnityEngine.Collision collision)
        {
            OnInstanceCollisionExitted.Invoke(Instance.GetAsInstance(gameObject), collision);
        }
        void OnTriggerEnter(UnityEngine.Collider other)
        {
            OnInstanceCollisionEntered.Invoke(Instance.GetAsInstance(gameObject), other);
        }
        void OnTriggerExit(UnityEngine.Collider other)
        {
            OnInstanceCollisionExitted.Invoke(Instance.GetAsInstance(gameObject), other);
        }
    }
}