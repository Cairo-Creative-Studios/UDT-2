using UnityEngine;
using Rich.Scriptables.Utilities;
using System;
using UnityEditor;

namespace Rich.Scriptables.Variables
{
    [CreateAssetMenu(fileName = "New Component Variable", menuName = "Rich/Scriptable Logic/Variables/Game Object")]
    public sealed class ScriptableVar_Component : ScriptableVariable<UnityEngine.Object>
    {
        private void OnValidate()
        {
            value = EditorGUILayout.ObjectField(value, typeof(MonoScript), false) as MonoScript;
        }
    }
}
