using UnityEngine;

namespace Rich.DataContainers
{
    public interface IDataContainer<T> where T : Data
    {
        protected Data _data { get => _data; set => _data = value; }
        public T Data
        {
            get
            {
                if (_data == null)
                {
                    _data = ScriptableObject.CreateInstance<T>();
                    return (T)_data;
                }
                else
                    return (T)_data;
            }
            set
            {
                _data = value;
            }
        }
    }
}