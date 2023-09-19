using Rich.DataContainers;
using UnityEngine;
using System;
using Rich.Extensions;
using System.Text.RegularExpressions;
using UnityEditor;
using System.Reflection;

namespace Rich.System
{
    public class Core : Singleton<Core, SystemData>
    {
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

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterAssembliesLoaded)]
        public static void Initialize(){
            if(Data.startupMode == SystemData.StartupMode.InitManually)
                return;

            if(singleton == null)
                new GameObject(typeof(Core).Name).AddComponent<Core>();
            
            singleton.GenerateData();
            singleton.GenerateRuntimes();
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
                    Debug.Log("Created " + runtimeSingleton.gameObject.name);
                }
            }
        }
    }
}