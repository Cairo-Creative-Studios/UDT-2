using Codice.ThemeImages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UDT.Instances;
using UnityEngine;

namespace UDT.PrefabTables
{
    [Serializable]
    public class PrefabPools : SerializableDictionary<GameObject, List<Instance>>
    {
        private static PrefabPools prefabPoolSingleton;
        private static SerializableDictionary<GameObject, Prefab> prefabs = new();
        private static PrefabPools Collection
        {
            get
            {
                if(prefabPoolSingleton == null)
                    prefabPoolSingleton = new PrefabPools();
                return prefabPoolSingleton;
            }
        }

        public static int PoolCount
        {
            get { return Collection.Keys.Count; }
        }

        public static List<Instance> GetPool(GameObject gameObject, bool createNew = true)
        {
            if(gameObject == null)
            {
                Debug.LogError("The given Prefab is null, so no Pool can exist for it.");
                return null;
            }

            if (!Collection.ContainsKey(gameObject))
            {
                if (createNew)
                {
                    Collection.Add(gameObject, new());
                    prefabs.Add(gameObject, new Prefab(gameObject));
                }
                else
                    return null;
            }
            return Collection[gameObject];
        }

        public static void AddToPool(GameObject gameObject, Instance instance)
        {
            var pool = GetPool(gameObject);
            pool.Add(instance);
        }

        public static void AddToPool(Prefab prefab, Instance instance)
        {
            if(!prefabs.ContainsKey(prefab.GameObject))
            {
                Collection.Add(prefab.GameObject, new());
                prefabs.Add(prefab.GameObject, prefab);
            }
            Collection[prefab.GameObject].Add(instance);
        }

        public static void AddPrefab(GameObject gameObject, List<Instance> instances)
        {
            prefabs.Add(gameObject, new Prefab(gameObject));
            Collection.Add(gameObject, instances);
        }

        public static void RemovePrefab(GameObject gameObject) 
        { 
            prefabs.Remove(gameObject);
            Collection.Remove(gameObject);
        }

        public static void RemoveInstance(Instance instance)
        {
            var prefab = instance.Prefab;
            if (prefab != null)
            {
                Collection[prefab.GameObject].Remove(instance);
            }
        }

        public static void RemoveInstanceByIID(GameObject gameObject, int IID)
        {
            Collection[gameObject].RemoveAt(IID);
        }

        public static Instance GetNthInstance(string name, int IID)
        {
            var key = Collection.Keys.FirstOrDefault(x => x.name == name);

            if(key != null)
            {
                if (Collection[key].Count > IID)
                {
                    return Collection[key][IID];
                }
                else
                {
                    Debug.LogError($"You requested an Instance by an IID({IID}) that is larger than the number of Instances({Collection[key].Count}) of the Prefab: " + name);
                }
            }
            else
            {
                Debug.LogError("The requested Prefab, " + name + ", either does not exist, or has no instances.");
            }

            return null;
        }

        public static int PoolSize(GameObject prefab)
        {
            return Collection[prefab].Count;
        }
    }
}