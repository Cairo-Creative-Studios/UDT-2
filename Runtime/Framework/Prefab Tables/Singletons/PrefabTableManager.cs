using System.Linq;
using UDT.System;
using UnityEngine;

namespace UDT.PrefabTables
{
    public sealed class PrefabTableManager : Singleton<PrefabTableManager>
    {
        private PrefabLookupTable[] prefabLookupTables;

        void Awake()
        {
            prefabLookupTables = Resources.LoadAll<PrefabLookupTable>("");
        }

        /// <summary>
        /// Finds the Prefab from the Prefab Lookup Tables and returns it.
        /// </summary>
        /// <param name="prefabName"></param>
        /// <returns></returns>
        public static GameObject FindPrefab(string prefabName)
        {
            foreach(var prefabLookupTable in singleton.prefabLookupTables)
            {
                foreach(var prefab in prefabLookupTable.prefabs)
                {
                    if(prefab.name == prefabName)
                        return prefab;
                }
            }

            return null;
        }

        /// <summary>
        /// Finds the Prefab from the Prefab Lookup Tables (or the specified one) and returns an Instance of it.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="table">Optionally provide the Table to Search on.</param>
        /// <returns></returns>
        public static GameObject InstantiatePrefab(string prefabName, string table = "")
        {
            GameObject prefab;

            if(table != "")
            {
                var prefabLookupTable = singleton.prefabLookupTables.FirstOrDefault(x => x.name == table);
                if(prefabLookupTable != null)
                {
                    prefab = prefabLookupTable.prefabs.FirstOrDefault(x => x.name == prefabName);
                    if(prefab != null)
                    {
                        return Instantiate(prefab);
                    }
                    return null;
                }
            }

            prefab = FindPrefab(prefabName);
            if(prefab != null)
            {
                return Instantiate(prefab);
            }

            return null;
        }
    }
}