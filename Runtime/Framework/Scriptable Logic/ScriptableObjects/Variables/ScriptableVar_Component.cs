using UnityEngine;
using UDT.Scriptables.Utilities;
using System;

#if UNITY_Editor
using UnityEditor;
#endif

namespace UDT.Scriptables.Variables
{
    [CreateAssetMenu(fileName = "New Component Variable", menuName = "Rich/Scriptable Logic/Variables/Game Object")]
    public sealed class ScriptableVar_Component : ScriptableVariable<UnityEngine.Object>
    {
        private void OnValidate()
        {
#if UNITY_Editor
            value = EditorGUILayout.ObjectField(value, typeof(MonoScript), false) as MonoScript;
#endif
        }
    }
}
