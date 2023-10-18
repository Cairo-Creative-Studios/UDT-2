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
        public static Prefab FindPrefab(string prefabName)
        {
            foreach (var prefabLookupTable in singleton.prefabLookupTables)
            {
                foreach (var prefab in prefabLookupTable.prefabs)
                {
                    if (prefab.name == prefabName)
                        return new Prefab(prefab);
                }
            }

            return null;
        }
    }
}