using Rich.DataContainers;
using Rich.StateMachines;
using Rich.ObjectPools;
using Rich.PrefabTables;
using UnityEngine;
using Rich.Controllables;
using Rich.Instances;
using Rich.Menus;
using Rich.Audio;
using Rich.Scriptables;
using Rich.Feedbacks;

namespace Rich.System
{
    public interface IRuntime
    {
        public GameObject gameObject { get; }
        protected static void DestroyInstance(Instance instance)
        {
            instance.Destroy();
        }
    }
    
    /// <summary>
    /// Runtimes are Singletons that are Instantiated at the start of the Runtime.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TData"></typeparam>
    public class Runtime<T, TData> : Singleton<T, TData>, IRuntime, IStateMachine where T : Runtime<T, TData> where TData : Data
    {
        /// <summary>
        /// Creates an Instance of the Prefab with the given name.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        protected static Instance CreateInstance(string name)
        {
            var gameObjectInstance = PrefabTableManager.InstantiatePrefab(name);
            if(gameObjectInstance == null)
            {
                Debug.Log("The Prefab didn't exist in any Prefab Tables, so the Instance couldn't be created.", singleton);
                return null;
            }

            return new Instance(gameObjectInstance);
        }

        /// <summary>
        /// Destroys the given Instance.
        /// </summary>
        /// <param name="instance"></param>
        protected static void DestroyInstance(Instance instance)
        {
            instance.Destroy();
        }

        /// <summary>
        /// Get's the Nth Instance of the Pool with the given name.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="n"></param>
        /// <returns></returns>
        protected static Instance GetNthInstance(string name, int n)
        {
            return Instance.GetInstance(ObjectPoolManager.GetPool(name)[n]);
        }

        /// <summary>
        /// Gets the Value of the Input with the given Input Name.
        /// </summary>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="inputName"></param>
        /// <returns></returns>
        protected static TValue GetInputValue<TValue>(string inputName)
        {
            return ControllerManager.GetInputValue<TValue>(inputName);
        }

        /// <summary>
        /// Returns True if the Input with the given Input Name is currently held down.
        /// </summary>
        /// <param name="inputName"></param>
        /// <returns></returns>
        protected static bool InputIsDown(string inputName)
        {
            return ControllerManager.InputIsDown(inputName);
        }

        /// <summary>
        /// Returns True if the Input with the given Input Name was pressed this frame.
        /// </summary>
        /// <param name="inputName"></param>
        /// <returns></returns>
        protected static bool InputWasPressed(string inputName)
        {
            return ControllerManager.InputWasPressed(inputName);
        }

        /// <summary>
        /// Returns True if the Input with the given Input Name was released this frame.
        /// </summary>
        /// <param name="inputName"></param>
        /// <returns></returns>
        protected static bool InputWasReleased(string inputName)
        {
            return ControllerManager.InputWasReleased(inputName);
        }

        /// <summary>
        /// Opens the Menu with the given Type T. 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="parent"></param>
        /// <returns></returns>
        protected static T OpenMenu<T>(MenuBase parent = null) where T : MenuBase
        {
            return (T)MenuManager.OpenMenu<T>(parent);
        }

        /// <summary>
        /// Closes the given Menu, removing it's Visual Elements from the Menu Hierarchy.
        /// </summary>
        /// <param name="menu"></param>
        protected static void CloseMenu(MenuBase menu)
        {
            MenuManager.CloseMenu(menu);
        }

        /// <summary>
        /// Creates a Controller with the given name.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        protected static Controller CreateController(string name)
        {
            return ControllerManager.CreateController(name);
        }

        /// <summary>
        /// Destroys the given Controller.
        /// </summary>
        /// <param name="controller"></param>
        protected static void DestroyController(Controller controller)
        {
            ControllerManager.DestroyController(controller);
        }

        /// <summary>
        /// Sets the Input actions for the given Controller, to an Input Action Map found in Resources.
        /// </summary>
        /// <param name="controller"></param>
        /// <param name="map"></param>
        /// <param name="assetName"></param>
        public static void SetInputActions(Controller controller, string map, string assetName)
        {
            ControllerManager.SetInputActions(controller, map, assetName);
        }

        /// <summary>
        /// Plays an Audio Clip from the Resources folder
        /// </summary>
        /// <param name="clipName"></param>
        /// <param name="tag"></param>
        /// <param name="type"></param>
        public static void PlayAudio(string clipName, string tag, AudioClipType type)
        {
            AudioManager.Play(clipName: clipName, tag: tag, clipType: type);
        }

        /// <summary>
        /// Stops all AudioSources playing the Clip with the given name.
        /// </summary>
        /// <param name="clipName"></param>
        public static void StopAudioWithClipName(string clipName)
        {
            AudioManager.StopWithName(clipName);
        }

        /// <summary>
        /// Stops the AudioSources with the given Tag.
        /// </summary>
        /// <param name="tag"></param>
        public static void StopAudioWithTag(string tag)
        {
            AudioManager.StopWithTag(tag);
        }

        /// <summary>
        /// Binds a variable to a field on an object instance.
        /// </summary>
        /// <param name="instance">The object instance to bind the variable to.</param>
        /// <param name="variableName">The name of the variable to bind.</param>
        /// <param name="field">The field to bind the variable to.</param>
        public static void Bind<TValue>(string variableName, BindMode bindMode, object instance, string fieldOrPropertyName)
        {
            ScriptableManager.Bind<TValue>(variableName, bindMode, instance, fieldOrPropertyName);
        }

        /// <summary>
        /// Creates and Adds the Feedback with the given Type to this Game Object
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="gameObject"></param>
        public static void AddFeedback<TFeedback>(GameObject gameObject) where TFeedback : Feedback
        {
            FeedbackManager.AddFeedback<TFeedback>(gameObject);
        } 

        /// <summary>
        /// Gets the Nth Feedback with the given Type that has been added to the GameObject
        /// </summary>
        /// <typeparam name="TFeedback"></typeparam>
        /// <param name="gameObject"></param>
        /// <param name="nth"></param>
        public static void GetFeedback<TFeedback>(GameObject gameObject, int nth) where TFeedback : Feedback
        {
            FeedbackManager.GetFeedback<TFeedback>(gameObject, nth);
        }
    }
}