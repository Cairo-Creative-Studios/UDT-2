using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace Rich.Scriptables.Utilities
{
    public unsafe class ScriptableVariable : ScriptableObject
    {
        public enum DataType
        {
            Field,
            Property
        }

        /// <summary>
        /// The fields that are set to the value of this variable.
        /// </summary>
        protected Dictionary<object, List<(DataType, FieldInfo, PropertyInfo)>> _GetBinds = new();
        protected (object, DataType, FieldInfo, PropertyInfo) _SetBind;
        
        protected object _value;

        public object Value 
        {
            get
            {
                return _value; 
            }
            set
            {
                _value = value;

                foreach(var boundObject in _GetBinds.Keys)
                {
                    foreach((DataType, FieldInfo, PropertyInfo) bind in _GetBinds[boundObject])
                    {
                        if(bind.Item1 == DataType.Field)
                        {
                            bind.Item2.SetValue(boundObject, Value);
                        }
                        else
                        {
                            bind.Item3.SetValue(boundObject, Value);
                        }
                    }
                }
            } 
        }

        class ReferenceType
        {
            public object Value;
        }

        /// <summary>
        /// Binds a listener object's field or property setter to this ScriptableVariable.
        /// </summary>
        /// <typeparam name="T">The type of the listener object.</typeparam>
        /// <param name="listener">The listener object to bind.</param>
        /// <param name="fieldOrPropertyName">The name of the field or property to bind.</param>
        public void BindSetter<T>(object listener, string fieldOrPropertyName)
        {
            var asField = listener.GetType().GetField(fieldOrPropertyName, BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public | BindingFlags.Static);
            var asProperty = listener.GetType().GetProperty(fieldOrPropertyName, BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public | BindingFlags.Static);
            _SetBind = asField != null ? (listener, DataType.Field, asField, null) : (listener, DataType.Property, null, asProperty);
        }

        /// <summary>
        /// Binds a getter function to a listener object's field or property with the specified name.
        /// </summary>
        /// <typeparam name="T">The type of the getter function's return value.</typeparam>
        /// <param name="listener">The object that contains the field or property to bind to.</param>
        /// <param name="fieldOrPropertyName">The name of the field or property to bind to.</param>
        public void BindGetter<T>(object listener, string fieldOrPropertyName)
        {
            var asField = listener.GetType().GetField(fieldOrPropertyName, BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public | BindingFlags.Static);
            var asProperty = listener.GetType().GetProperty(fieldOrPropertyName, BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public | BindingFlags.Static);
            (DataType, FieldInfo, PropertyInfo) bind = asField != null ? (DataType.Field, asField, null) : (DataType.Property, null, asProperty);
            
            if(bind.Item1 == DataType.Field)
            {
                bind.Item2.SetValue(listener, Value);
            }
            else
            {
                bind.Item3.SetValue(listener, Value);
            }

            if(!_GetBinds.ContainsKey(listener)) _GetBinds.Add(listener, new List<(DataType, FieldInfo, PropertyInfo)>());

            _GetBinds[listener].Add(bind);
        }

        /// <summary>
        /// Removes the binding between the specified listener and the specified field or property.
        /// </summary>
        /// <typeparam name="T">The type of the field or property.</typeparam>
        /// <param name="listener">The listener to unbind.</param>
        /// <param name="fieldOrPropertyName">The name of the field or property to unbind.</param>
        public void Unbind<T>(object listener, string fieldOrPropertyName)
        {
            if(_GetBinds.ContainsKey(listener))
            {
                var bind = _GetBinds[listener].FirstOrDefault(x => x.Item2.Name == fieldOrPropertyName || x.Item3.Name == fieldOrPropertyName);
                if(bind != default)
                {
                    _GetBinds[listener].Remove(bind);
                    return;
                }
            }
            if(_SetBind.Item1 == listener)
            {
                _SetBind = (null, DataType.Field, null, null);
                return;
            }
        }

        /// <summary>
        /// Unbinds the specified listener from this ScriptableVariable.
        /// </summary>
        /// <param name="listener">The listener to unbind.</param>
        public void UnbindAll(object listener)
        {
            if(_SetBind.Item1 == listener)
            {
                _SetBind = (null, DataType.Field, null, null);
            }
            else
            {
                _GetBinds.Remove(listener);
            }
        }

        /// <summary>
        /// Updates the value of this ScriptableVariable by retrieving the value from the bound object.
        /// This subsequently updates all bound Getter variables.
        /// </summary>
        public virtual void UpdateValues()
        {
            if(_SetBind.Item1 == null)
            {
                return;
            }
            var newValue = _SetBind.Item3 != null ? _SetBind.Item3.GetValue(_SetBind.Item1) : _SetBind.Item4 != null ? _SetBind.Item4.GetValue(_SetBind.Item1) : null;
            if(_value != newValue)
                Value = newValue;
        }
    }

    public unsafe class ScriptableVariable<T> : ScriptableVariable
    {
        public new T Value 
        { 
            get
            {
                return (T)_value; 
            }
            set
            {
                _value = value;

                foreach(var boundObject in _GetBinds.Keys)
                {
                    foreach((DataType, FieldInfo, PropertyInfo) bind in _GetBinds[boundObject])
                    {
                        if(bind.Item1 == DataType.Field)
                        {
                            bind.Item2.SetValue(boundObject, Value);
                        }
                        else
                        {
                            bind.Item3.SetValue(boundObject, Value);
                        }
                    }
                }
            } 
        }
    }
}
