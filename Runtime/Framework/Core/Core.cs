using UDT.DataContainers;
using UnityEngine;
using System;
using UDT.Extensions;
using System.Reflection;
using UDT.Scriptables;
using UDT.Scriptables.Events;
using System.Collections.Generic;
using UDT.StateMachines;
using UDT.PrefabTables;
using UDT.Instances;
using UDT.Controllables;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace UDT.System
{
    public sealed class Core : Singleton<Core, SystemData>
    {
#if UNITY_EDITOR
        public static string CurrentProjectFolderPath
        {
            get
            {
                Type projectWindowUtilType = typeof(ProjectWindowUtil);
                MethodInfo getActiveFolderPath = projectWindowUtilType.GetMethod("GetActiveFolderPath", BindingFlags.Static | BindingFlags.NonPublic);
                object obj = getActiveFolderPath.Invoke(null, new object[0]);
                return obj.ToString();
            }
        }
#endif

        private static List<IRuntime> runtimes = new();
        public static List<IRuntime> Runtimes
        {
            get
            {
                return runtimes;
            }
        }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterAssembliesLoaded)]
        public static void Initialize(){
            if(Data.startupMode == SystemData.StartupMode.InitManually)
                return;

            if(singleton == null)
                new GameObject(typeof(Core).Name).AddComponent<Core>();
            
            singleton.GenerateData();
            singleton.GenerateRuntimes();

            ControllerManager.InstantiateSingleton();
            if(Data.EnableVisualScripting)
            {
                ScriptableManager.InstantiateSingleton();
            }
        }
        
        void GenerateData(){     
        }

        void GenerateRuntimes(){
            // Create the Runtime Types list and add all the Runtime Types to it that exist in the current project
            Type[] runtimeTypes = typeof(IRuntime).GetInterfacingTypes(); 

            foreach (var type in runtimeTypes){
                Debug.Log(type.Name);
                if (type.ContainsGenericParameters || type.Name == "Runtime`2"){
                    Debug.Log("Skipped "+ type);
                }
                else{
                    // Create the Runtime Singleton Game Object
                    var runtimeSingleton = InstantiateSingleton(type);
                    OnRuntimeStart.Invoke(runtimeTypes, runtimeSingleton);
                    Debug.Log("Created " + runtimeSingleton.gameObject.name);
                    runtimes.Add(runtimeSingleton);
                }
            }
        }

        private void Update()
        {
            foreach(var runtime in runtimes)
            {
                var asStateMachine = ((IStateMachine)runtime);
                if(asStateMachine.CurrentState != null)
                    OnRuntimeStateUpdate.Invoke(asStateMachine.CurrentState, runtime);
                OnRuntimeUpdate.Invoke(runtime);
            }

            foreach(var instance in Instance.Instances)
            {
                OnInstanceUpdate.Invoke(instance);
            }
        }
    }
}