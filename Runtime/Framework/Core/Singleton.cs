using UnityEngine;
using UDT.DataContainers;
using UnityEngine.Windows;
using UDT.Extensions;
using System;
using UnityEngine.SceneManagement;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace UDT.System
{
    public abstract class SingletonBase : MonoBehaviour
    {
        protected abstract void Init();

        public static IRuntime InstantiateSingleton(Type type)
        {
            var singleton = new GameObject().AddComponent(type) as SingletonBase;
            singleton.Init();

            SceneManager.activeSceneChanged += singleton.SceneChanged;
            SceneManager.sceneLoaded += singleton.SceneLoaded;
            SceneManager.sceneUnloaded += singleton.SceneUnloaded;

            return (IRuntime)singleton;
        }

        protected virtual void SceneChanged(Scene previousScene, Scene nextScene)
        { }

        protected virtual void SceneLoaded(Scene scene, LoadSceneMode mode)
        { }

        protected virtual void SceneUnloaded(Scene scene)
        { }
    }

    public class Singleton<T, TData> : SingletonBase, IStaticDataContainer<TData> where TData : Data where T : Singleton<T, TData>
    {
        private static T _instance;
        public static T singleton { 
            get 
            {
                try
                {
                    if(_instance == null)
                        _instance = InstantiateSingleton();
                    return _instance;
                }
                catch
                {
                    return _instance;
                }
            } 
            private set
            {
                _instance = value;
            }
        }
        
        private TData _data;
        public static TData Data { 
            get{
                if(!Application.isPlaying)
                {
                    if(singleton._data != null)
                        return singleton._data;
                }

                var applicableData = Resources.LoadAll<TData>("");

                TData createdData = null;
                if(applicableData == null || applicableData.Length == 0){
                    createdData = ScriptableObject.CreateInstance<TData>();
    #if UNITY_EDITOR
                    if(!Directory.Exists("Assets/Resources"))
                        Directory.CreateDirectory("Assets/Resources");

                    AssetDatabase.CreateAsset(createdData, "Assets/Resources/" + typeof(TData).Name + ".asset");
                    AssetDatabase.SaveAssets();
    #endif
                }
                else
                {
                    createdData = applicableData[0];
                }
                return createdData;
            }
            
            protected set {
                singleton._data = value; 
            }
        }

        void Awake()
        {
            if(Core.Data.startupMode == SystemData.StartupMode.InitManually)
                Init();
        }

        protected override void Init()
        {
            singleton = (T)this;
            _data = Data;
            name = typeof(T).Name.AddSpacesBetweenCapitalization();
            DontDestroyOnLoad(gameObject);
            DontDestroyOnLoad(this);
        }

        public static T InstantiateSingleton()
        {
            if(_instance != null)
                return _instance;

            var singleton = new GameObject().AddComponent<T>() as T;
            singleton.Init();
                
            return singleton;
        }
    }

    public class Singleton<T> : SingletonBase where T : Singleton<T>
    {
        private static T _instance;
        public static T singleton { 
            get 
            {
                try
                {
                    if(_instance == null)
                        _instance = InstantiateSingleton();
                    return _instance;
                }
                catch
                {
                    return _instance;
                }
            } 
            private set
            {
                _instance = value;
            }
        }
    

        void Awake()
        {
            if(Core.Data.startupMode == SystemData.StartupMode.InitManually)
                Init();
        }

        protected override void Init()
        {
            singleton = (T)this;
            name = typeof(T).Name.AddSpacesBetweenCapitalization();
            DontDestroyOnLoad(gameObject);
            DontDestroyOnLoad(this);
        }

        public static T InstantiateSingleton()
        {
            if(_instance != null)
                return _instance;

            var singleton = new GameObject().AddComponent<T>() as T;
            singleton.Init();
                
            return singleton;
        }
    }

}