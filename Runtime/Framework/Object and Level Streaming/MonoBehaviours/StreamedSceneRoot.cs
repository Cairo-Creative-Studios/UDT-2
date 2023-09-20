using Rich.Extensions;
using UnityEngine;
using UnityEngine.SceneManagement;
using NaughtyAttributes;
using UnityEngine.UIElements;
using Rich.ScriptableEvents;

#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;
#endif


namespace Rich.Streaming
{
    /// <summary>
    /// Allows you to Save/Load children of a GameObject as a Scene, so that it can be streamed in place of the object the StreamedSceneRoot is attached to.
    /// </summary>
    [ExecuteAlways]
    [DisallowMultipleComponent]
    internal class StreamedSceneRoot : MonoBehaviour
    {
        public Scene scene;
        [Tooltip("A Scriptable Event asset that can be used to make the Streamed Scene Load")]
        public ScriptableEvent loadEvent;

        void Awake()
        {
            if(loadEvent != null)
                loadEvent.AddListener(() => LoadScene());
        }

        [Button("Save as Scene")]
        public void SaveScene()
        {
            EditorSceneManager.SaveScene(gameObject.scene);
        }

        public void LoadScene()
        {
            if (scene == null || scene.isLoaded)
                return;

            SceneManager.LoadScene(gameObject.scene.name, LoadSceneMode.Additive);
            scene = SceneManager.GetSceneByName(gameObject.scene.name);
        }
    }
}