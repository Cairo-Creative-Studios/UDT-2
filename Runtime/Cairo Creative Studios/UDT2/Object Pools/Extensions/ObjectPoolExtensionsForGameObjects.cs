using UnityEngine;

namespace Rich.ObjectPools
{
    public static class ObjectPoolExtensionsForGameObjects
    {
        public static void CreatePoolFromThis(this GameObject instance)
        {
            var poolArchetype = instance.GetComponent<ObjectArchetype>();
            if(poolArchetype == null)
            {
                poolArchetype = instance.AddComponent<ObjectArchetype>();
                poolArchetype.prefab = instance;
            }
            
            ObjectPoolManager.AddToPool(instance);
        }

        public static void AddToPool(this GameObject instance)
        {
            ObjectPoolManager.AddToPool(instance);
        }
    }
}