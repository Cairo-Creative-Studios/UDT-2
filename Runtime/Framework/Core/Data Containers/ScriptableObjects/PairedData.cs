using UnityEngine;
using Rich.DataContainers;

namespace Rich.DataPairedMonoBehaviour
{
    public class PairedData<TThis, TMonoBehaviour> : Data where TMonoBehaviour : MonoBehaviour, IDataContainer<TThis> where TThis : Data
    {
        public TMonoBehaviour InstantiateObject()
        {
            var obj = new GameObject(typeof(TMonoBehaviour).Name).AddComponent<TMonoBehaviour>();
            obj.Data = (TThis)(Data)this;
            return obj;
        }
    }
}
