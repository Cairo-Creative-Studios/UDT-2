using System;
using UnityEngine;
using UnityEngine.Windows;
using NaughtyAttributes;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace UDT.Scriptables
{
    [Serializable]
    public class ScriptableObjectAsset
    {
        [AllowNesting]
        [Expandable]
        public ScriptableObject scriptableObject = null;
        public bool Valid
        {
            get
            {
                return scriptableObject != null;
            }
        }
        protected Type _type;
        protected Type _previousType;

        public ScriptableObjectAsset(Type type)
        {
            _type = type;
        }

        public virtual void Verify()
        {
            if (_type != null && _type != _previousType && scriptableObject == null)
            {
                scriptableObject = CreateScriptableAsset(_type);
                _previousType = _type;
            }
        }

        public virtual void SetType<T>() where T : ScriptableObject
        {
            _type = typeof(T);
            scriptableObject = null;
            Verify();
        }

        public virtual void SetType(Type type)
        {
            _type = type;
            Verify();
        }

        public virtual void SetType(string typeName)
        {
            _type = Type.GetType(typeName);
            Verify();
        }

        public virtual T GetScriptableAs<T>(bool convertType = false) where T : ScriptableObject
        {
            return (T)scriptableObject;
        }

        private ScriptableObject CreateScriptableAsset(Type scriptableObjectType, string folderPath = "Assets/Temp")
        {
            var asset = typeof(ScriptableObject).GetMethod("CreateInstance", new Type[] { }).MakeGenericMethod(scriptableObjectType).Invoke(null, null) as ScriptableObject;

#if UNITY_EDITOR
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
                AssetDatabase.Refresh();
            }

            string assetPathAndName = AssetDatabase.GenerateUniqueAssetPath($"{folderPath}/_{nameof(_type)}.asset");

            AssetDatabase.CreateAsset(asset, assetPathAndName);

            asset = AssetDatabase.LoadAssetAtPath<ScriptableObject>(assetPathAndName);

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            Selection.activeObject = asset;
#endif

            return asset;
        }
    }

    [Serializable]
    public class ScriptableObjectAsset<T> : ScriptableObjectAsset
    {
        public ScriptableObjectAsset() : base(typeof(T))
        { }
        public new bool Valid 
        {
            get
            {
                return scriptableObject != null;
            }
        }
    }
}