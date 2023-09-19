using System;
using UnityEngine;

namespace Rich.ObjectPools
{
    [RequireComponent(typeof(ObjectArchetype))]
    [DisallowMultipleComponent]
    public class ObjectPoolInstance : MonoBehaviour
    {
        public bool InstanceEnabled = true;
        [NonSerialized] public bool pooled = false;

        void Awake()
        {
            if(!pooled)
                ObjectPoolManager.AddToPool(gameObject);
        }
    }
}