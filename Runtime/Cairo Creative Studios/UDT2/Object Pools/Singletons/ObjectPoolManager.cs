using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Rich.System;

namespace Rich.ObjectPools
{
    public class ObjectPoolManager : Singleton<ObjectPoolManager, SystemData>
    {
        private List<ObjectArchetype> objectArchetypes = new();
        private List<PrefabPool> objectPools = new();

        void Start()
        {
            objectArchetypes = Resources.LoadAll<ObjectArchetype>("").ToList();
        }

        /// <summary>
        /// Instantiates a new Instance Game Object of the specified object pool, by name.
        /// A new prefab pool is created if it does not exist.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static GameObject Instantiate(string name, Vector3 position = default, Quaternion rotation = default, Vector3 eulerAngles = default, bool forceAppend = false)
        {
            var prefabPool = singleton.objectPools.FirstOrDefault(x => x.name == name);
            if (prefabPool == null)
            {
                var archetype = singleton.objectArchetypes.FirstOrDefault(x => x.name == name).gameObject;
                if(archetype != null)
                {
                    prefabPool = new PrefabPool(singleton.objectArchetypes.FirstOrDefault(x => x.name == name).gameObject, Data.PrefabPoolSize);
                    singleton.objectPools.Add(prefabPool);
                }
                else
                {
                    Debug.LogWarning("Object Archetype: " + name + " does not exist in resources, or belong to an Object Pool Reference. A blank object will be isntantiated instead, as the head of a new Object Pool.");
                    var newArchetype = new GameObject(name);
                    newArchetype.SetActive(false);
                    prefabPool = new PrefabPool(new GameObject(name), Data.PrefabPoolSize);
                }   
            }
            return prefabPool.Instantiate(position, rotation, eulerAngles, forceAppend);
        }

        public static GameObject Instantiate(GameObject prefab, Vector3 position = default, Quaternion rotation = default, Vector3 eulerAngles = default, bool forceAppend = false)
        {
            var prefabPool = singleton.objectPools.FirstOrDefault(x => x.Prefab == prefab);
            if (prefabPool == null)
            {
                prefabPool = new PrefabPool(prefab, Data.PrefabPoolSize);
                singleton.objectPools.Add(prefabPool);
            }
            return prefabPool.Instantiate(position, rotation, eulerAngles, forceAppend);
        }

        /// <summary>
        /// Destroys an Instance Game Object if it exists in a prefab pool.
        /// </summary>
        /// <param name="gameObject"></param>
        public static void Destroy(GameObject gameObject)
        {
            var prefabPool = singleton.objectPools.FirstOrDefault(x => x.Contains(gameObject) == true);
            if (prefabPool!= null)
                prefabPool.Destroy(gameObject);
            else
                Debug.LogWarning("There is no Prefab Pool for: " + gameObject.name + ", it's destruction should not be handled by the Object Manager.");
        }

        /// <summary>
        /// Adds an Instance Game Object to a prefab pool if the pool exists, otherwise a new pool is created with the Instance's Archetype Prefab as the Head.
        /// </summary>
        /// <param name="instance"></param>
        public static void AddToPool(GameObject instance){
            var archetypeComponent = instance.GetComponent<ObjectArchetype>();
            if(archetypeComponent == null){
                Debug.LogWarning("The instance is not an Object Archetype, and cannot be added to a pool.", instance);
                return;
            }

            var prefab = instance.GetComponent<ObjectArchetype>().prefab;
            var prefabPool = singleton.objectPools.FirstOrDefault(x => x.Prefab == prefab);

            if(prefabPool == null){
                prefabPool = new PrefabPool(prefab, Data.PrefabPoolSize);
                prefabPool.Add(instance);
                singleton.objectPools.Add(prefabPool);
            }
        }

        /// <summary>
        /// Returns the Head of the given Prefab Pool only if the Prefab Pool has already been instantiated,
        /// otherwise returns null.
        /// </summary>
        /// <param name="prefab"></param>
        /// <returns></returns>
        public static GameObject GetPoolHead(GameObject prefab)
        {
            var prefabPool = singleton.objectPools.FirstOrDefault(x => x.Prefab == prefab);
            if(prefabPool != null)
                return prefabPool.Prefab;
            else
                return null;
        }

        public static PrefabPool GetPool(string name)
        {
            return singleton.objectPools.FirstOrDefault(x => x.name == name);
        }
    }

    public class PrefabPool
    {
        private List<GameObject> instances = new();
        private GameObject prefab;
        private int maxPoolSize = 10;
        public string name => prefab.name;
        public GameObject Prefab => prefab;
        public int Count => instances.Count;
        public int MaxPoolSize => maxPoolSize;

        public GameObject this[int n]
        {
            get
            {
                return instances[n];
            }
        }

        public PrefabPool(GameObject prefab, int poolSize)
        {
            this.prefab = prefab;
            maxPoolSize = poolSize;
        }

        public bool Contains(GameObject instance)
        {
            return instances.Contains(instance);
        }

        public GameObject Instantiate(Vector3 position, Quaternion rotation, Vector3 eulerAngles, bool forceAppend)
        {
            ObjectPoolInstance instanceComponent = null;
            if(forceAppend)
            {
                var forcedInstance = GameObject.Instantiate(prefab, position, (eulerAngles == default) ? rotation : Quaternion.Euler(eulerAngles));
                instanceComponent = forcedInstance.AddComponent<ObjectPoolInstance>();
                instanceComponent.InstanceEnabled = true;
                instanceComponent.pooled = true;
                return forcedInstance;
            }

            GameObject instance = instances.FirstOrDefault(x => x.activeSelf == false && x.GetComponent<ObjectPoolInstance>().InstanceEnabled == false);
            if(instance == null)
            {
                instance = GameObject.Instantiate(prefab, position, (eulerAngles == default) ? rotation : Quaternion.Euler(eulerAngles));
                instanceComponent = instance.AddComponent<ObjectPoolInstance>();
            }
            else
            {
                instance.SetActive(true);
                instance.transform.position = position;
                instanceComponent = instance.GetComponent<ObjectPoolInstance>();


                if(eulerAngles != Vector3.zero)
                    instance.transform.eulerAngles = eulerAngles;
                else
                    instance.transform.rotation = rotation;
            }

            if(instanceComponent != null)
            {
                instanceComponent.InstanceEnabled = true;
                instanceComponent.pooled = true;
            }

            return instance;
        }

        public GameObject Destroy(GameObject instance)
        {
            if(instances.Count > maxPoolSize)
            {
                instances.Remove(instance);
                Destroy(instance);
            }
            else
            {
                instance.GetComponent<ObjectPoolInstance>().InstanceEnabled = false;
                instance.SetActive(false);
            }

            return instance;
        }

        public void Add(GameObject instance)
        {
            instances.Add(instance);
        }
    }
}