using System;
using System.Collections.Generic;
using UDT.Instances;
using UnityEngine;

namespace UDT.PrefabTables
{
    [Serializable]
    public class Prefab
    {
        private GameObject gameObject;
        public GameObject GameObject { get => gameObject; }

        public Prefab(GameObject gameObject)
        {
            this.gameObject = gameObject;
        }
    }
}
