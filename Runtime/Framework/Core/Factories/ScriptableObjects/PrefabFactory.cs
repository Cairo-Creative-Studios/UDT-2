using UnityEngine;
using System.IO;
using System.Threading.Tasks;
using UDT.System;


#if UNITY_EDITOR
using UnityEditor;
#endif

namespace UDT.Factories
{
    public abstract class PrefabFactory<T>
    {
        public virtual void SetupPrefab(GameObject instance)
        {

        }
        
        [MenuItem("Assets/Create/Prefab/" +  nameof(T))]
        private void Create()
        {
            var newInstance = new GameObject();

            var components = GetType().GetCustomAttributes(typeof(PrefabComponentAttribute), true);
            var nameAttribute = GetType().GetCustomAttributes(typeof(PrefabNameAttribute), true);

            foreach (var component in components)
            {
                var componentType = ((PrefabComponentAttribute)component).type;
                if(componentType.IsSubclassOf(typeof(Component)))
                    newInstance.AddComponent(componentType);
            }
            if (nameAttribute.Length > 0) newInstance.name = ((PrefabNameAttribute)nameAttribute[0]).name;

            SetupPrefab(newInstance);

#if UNITY_EDITOR
            var directory = Core.CurrentProjectFolderPath;
            
            var path = $"{ directory }/{ newInstance.name }.prefab";
            var increments = 0;
            var canCreate = false;

            while (!canCreate)
            {
                if (AssetDatabase.LoadAssetAtPath(path, typeof(GameObject)) != null)
                {
                    increments++;
                    path = $"{ directory }/{ newInstance.name }{ increments }.prefab";
                }
                else
                {
                    canCreate = true;
                }
            }

            PrefabUtility.SaveAsPrefabAsset(newInstance, path);
            Object.DestroyImmediate(newInstance);

            AssetDatabase.Refresh();
#endif
        }
    }
}