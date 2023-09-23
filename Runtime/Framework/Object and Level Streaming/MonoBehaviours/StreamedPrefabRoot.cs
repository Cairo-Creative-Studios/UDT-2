using UnityEngine;
using Rich.Extensions;
using Rich.Scriptables;
using NaughtyAttributes;
using UnityEditor.SceneManagement;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Rich.Streaming
{
    /// <summary>
    /// Allows you to Save/Load children of a GameObject as a Prefab, so that it can be streamed in place of the object the StreamedPrefabRoot is attached to.
    /// </summary>
    [ExecuteAlways]
#if !UNITY_EDITOR
    public class StreamedPrefabRoot : MonoBehaviour
#endif
#if UNITY_EDITOR
    public class StreamedPrefabRoot : MonoBehaviour, IPreprocessBuildWithReport 
#endif

    {
        [Tooltip("A Scriptable Event asset that can be used to make the Streamed Prefab Load")]
        public ScriptableEvent loadEvent;

        private GameObject _instance;
        [SerializeField] private GameObject prefab;
        [SerializeField] private string path;
        private string _oldPath;
        private string _oldName;

#if UNITY_EDITOR
        public int callbackOrder => 0;
#endif

        private void Awake()
        {
            if (Application.isPlaying)
                Unload();

#if UNITY_EDITOR
            //Unload the Streamed Prefab when the scene changes
            EditorSceneManager.activeSceneChanged += (firstScene, secondScene) => { Unload(); EditorSceneManager.SaveScene(gameObject.scene); };
#endif
        }

        public void Load()
        {
            if (prefab == null) return;
            if (_instance != null) return;
            _instance = GameObject.Instantiate(prefab, transform);
        }

        [Button("Unload")]
        public void Unload()
        {
            foreach (Transform child in transform.GetChildren())
                if (child != transform)
                    Destroy(child.gameObject);
        }

        void Update()
        {
#if UNITY_EDITOR
            if (Application.isPlaying)
                return;

            if(Selection.activeGameObject == gameObject)
            {
                Load();
            }

            bool generate = prefab == null;

            if (_oldName != name || _oldPath != path)
            {
                _oldName = name;
                _oldPath = path;

                //Destroy the old Prefab if it exists
                if (!(prefab == null))
                {
                    DestroyImmediate(prefab, true);
                }
            }

            string fullPath = "Assets/" + path + name + ".prefab";
            if (prefab == null)
            {
                //Generate
                GameObject generated = new GameObject();
                prefab = PrefabUtility.SaveAsPrefabAsset(generated, fullPath);
                DestroyImmediate(generated);

                //Instantiate the Instance of the Prefab
                _instance = PrefabUtility.InstantiatePrefab(prefab, transform) as GameObject;
                Selection.activeObject = AssetDatabase.LoadMainAssetAtPath(fullPath);
            }

            if (_instance == null)
                _instance = transform.GetChild(0).gameObject;
            else
            {
                prefab = PrefabUtility.SaveAsPrefabAsset(_instance, fullPath);
            }
#endif
        }

#if UNITY_EDITOR
        public void OnPreprocessBuild(BuildReport report)
        {
            Unload();
        }
#endif
    }
}