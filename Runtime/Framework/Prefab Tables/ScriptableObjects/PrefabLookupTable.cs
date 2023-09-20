using Rich.DataContainers;
using UnityEngine;
using System.Collections.Generic;

namespace Rich.PrefabTables
{
    [CreateAssetMenu(fileName = "Prefab Lookup Table", menuName = "Rich/Prefabs/Prefab Lookup Table")]
    public sealed class PrefabLookupTable : Data
    {
        public List<GameObject> prefabs;
    }
}