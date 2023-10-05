using NaughtyAttributes;
using UnityEngine;

namespace UDT.Scriptables
{
    public class ScriptableEventObject : MonoBehaviour
    {
        [Expandable]
        public EventGraph sheet;

        void Awake()
        {
            sheet.This = gameObject;
        }
    }
}