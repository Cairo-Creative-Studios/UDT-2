using System.Data;
using UnityEngine;
using Rich.DataContainers;

namespace Rich.DataPairedMonoBehaviour
{
    public class DataPairedMonoBehaviourBase : MonoBehaviour
    {
        protected Data _data;
        public Data Data { get { return _data; } set { _data = value; } }
    }

    public class DataPairedMonoBehaviour<T> : DataPairedMonoBehaviourBase, IDataContainer<T> where T : Data
    {
        public new T Data { 
            get { 
                if(_data == null)
                {
                    _data = ScriptableObject.CreateInstance<T>();
                    return (T)_data;
                }
                else
                    return (T)_data; 
            } 
            set { 
                _data = value; 
                } 
            }
    }
}
