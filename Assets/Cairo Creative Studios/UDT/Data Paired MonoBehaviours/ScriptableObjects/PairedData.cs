using UnityEngine;
using Rich.DataContainers;

namespace Rich.DataPairedMonoBehaviour
{
    public class PairedData<T> : Data where T : DataPairedMonoBehaviourBase
    {
        public T InstantiateObject()
        {
            var obj = new GameObject(typeof(T).Name).AddComponent<T>();
            obj.Data = this;
            return obj;
        }
    }
}
