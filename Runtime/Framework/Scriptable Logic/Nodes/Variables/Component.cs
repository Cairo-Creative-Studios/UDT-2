using UnityEngine;
using UDT.Scriptables.Utilities;
using XNode;
using System;

#if UNITY_Editor
using UnityEditor;
#endif

namespace UDT.Scriptables.Variables
{
    [CreateNodeMenu("Variables/Objects/Component")]
    public sealed class Component : VariableNode<UnityEngine.Object>
    {
        private new void OnValidate()
        {
            base.OnValidate();
#if UNITY_Editor
            value = EditorGUILayout.ObjectField(value, typeof(MonoScript), false) as MonoScript;
#endif
        }
    }
}
