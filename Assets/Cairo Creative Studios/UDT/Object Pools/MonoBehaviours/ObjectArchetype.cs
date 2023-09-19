using UnityEngine;

namespace Rich.ObjectPools
{
    public class ObjectArchetype : MonoBehaviour
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