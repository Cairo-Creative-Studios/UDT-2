using System.Buffers.Text;
using System.Collections.Generic;
using Rich.DataContainers;
using Rich.StateMachines;
using Rich.ObjectPools;
using Rich.PrefabTables;
using UnityEngine;
using System.Linq;
using Rich.Controllables;
using Rich.Instances;

namespace Rich.System
{
    public interface IRuntime
    {
        public GameObject gameObject { get; }
    }

    /// <summary>
    /// Runtimes are Singletons that are Instantiated at the start of the Runtime.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TData"></typeparam>
    public class Runtime<T, TData> : Singleton<T, TData>, IRuntime, IStateMachine where T : Runtime<T, TData> where TData : Data
    {
        //public SerializableDictionary<StateBase, List<GameObject>> stateObjects = new();
        
        protected static Instance CreateInstance(string name)
        {
            var gameObjectInstance = PrefabTableManager.InstantiatePrefab(name);
            if(gameObjectInstance == null)
            {
                Debug.Log("The Prefab didn't exist in any Prefab Tables, so the Instance couldn't be created.", singleton);
            }
            return new Instance(gameObjectInstance);
        } 

        protected static void DestroyInstance(Instance instance)
        {
            instance.Destroy();
        }

        protected static Instance GetNthInstance(string name, int n)
        {
            return Instance.GetInstance(ObjectPoolManager.GetPool(name)[n]);
        }

        protected static TValue GetInputValue<TValue>(string inputName)
        {
            return ControllerManager.GetInputValue<TValue>(inputName);
        }

        protected static bool InputIsDown(string inputName)
        {
            return ControllerManager.InputIsDown(inputName);
        }

        protected static bool InputWasPressed(string inputName)
        {
            return ControllerManager.InputWasPressed(inputName);
        }

        protected static bool InputWasReleased(string inputName)
        {
            return ControllerManager.InputWasReleased(inputName);
        }
    }

    public class Runtime<T> : Singleton<T>, IRuntime where T : Runtime<T>
    {
        public static GameObject CreateInstance(string name)
        {
            return PrefabTableManager.InstantiatePrefab(name);
        } 

        protected static Instance GetNthInstance(string name, int n)
        {
            return Instance.GetInstance(ObjectPoolManager.GetPool(name)[n]);
        }

        protected static TValue GetInputValue<TValue>(string inputName)
        {
            return ControllerManager.GetInputValue<TValue>(inputName);
        }

        protected static bool InputIsDown(string inputName)
        {
            return ControllerManager.InputIsDown(inputName);
        }

        protected static bool InputWasPressed(string inputName)
        {
            return ControllerManager.InputWasPressed(inputName);
        }

        protected static bool InputWasReleased(string inputName)
        {
            return ControllerManager.InputWasReleased(inputName);
        }
    }
}