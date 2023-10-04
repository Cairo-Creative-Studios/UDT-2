using UnityEngine;

namespace UDT.ObjectPools
{
    public sealed class ObjectArchetype : MonoBehaviour
    {
        public GameObject prefab;

        void Awake()
        {
            var poolInstance = GetComponent<ObjectPoolInstance>();
            if(poolInstance != null)
            {
                var existingHead = ObjectPoolManager.GetPoolHead(prefab);

                if(existingHead == null)
                {
                    ObjectPoolManager.AddToPool(gameObject);
                }
            }
        }
    }
}