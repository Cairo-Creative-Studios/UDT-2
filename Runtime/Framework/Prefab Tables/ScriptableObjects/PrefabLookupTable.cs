using UDT.DataContainers;
using UnityEngine;
using System.Collections.Generic;

namespace UDT.PrefabTables
{
    [CreateAssetMenu(fileName = "Prefab Lookup Table", menuName = "Rich/Prefabs/Prefab Lookup Table")]
    public sealed class PrefabLookupTable : Data
    {
        public List<GameObject> prefabs;
    }
}