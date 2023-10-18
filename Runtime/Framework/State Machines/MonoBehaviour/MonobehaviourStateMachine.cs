using NaughtyAttributes;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace UDT.StateMachines
{
    public sealed class MonobehaviourStateMachine : MonoBehaviour
    {
        public IStateMachine machine;
        public ComponentState stateHierarchy = new();
        private ComponentState currentState; 
        public ComponentState CurrentState { get => currentState; }
        private DropdownList<MonoBehaviour> cachedMonobehaviours;

        private void OnValidate()
        {
            foreach(var stateName in stateHierarchy.Keys)
            {
                if(currentState == null)
                {
                    currentState = stateHierarchy[stateName];
                }

                foreach(ComponentState nestedState in stateHierarchy[stateName])
                {
                    nestedState.name = stateName;

                    foreach (var reference in nestedState.monoBehaviours)
                    {
                        reference.gameObject = gameObject;
                        reference.controller = this;
                    }
                }
            }
        }

        public void SetStateHierarchyPath(string statePath)
        {
            var states = statePath.Split('/');
            ComponentState currentState = stateHierarchy;

            foreach (var state in states)
            {
                if(!currentState.ContainsKey(state))
                {
                    Debug.LogError("The requested State does not exist: " + state, this);
                }

                currentState = currentState[state];
            }

            this.currentState = currentState;
        }

        [Serializable]
        public class ComponentState : SerializableDictionary<string, ComponentState>
        {
            [ReadOnly] public string name;
            public List<MonoBehaviourReference> monoBehaviours = new();
        }

        [Serializable]
        public class MonoBehaviourReference
        {
            [Dropdown("GetMonobehaviours")]
            public MonoBehaviour behaviour;

            [HideInInspector]
            public GameObject gameObject;
            [HideInInspector]
            public MonobehaviourStateMachine controller;

            public DropdownList<MonoBehaviour> GetMonobehaviours()
            {
                if(controller.cachedMonobehaviours == null)
                {
                    DropdownList<MonoBehaviour> returnList = new()
                    {
                        { "None", null }
                    };

                    foreach (var mb in gameObject.GetComponents<MonoBehaviour>())
                    {
                        returnList.Add(mb.name, mb);
                    }

                    controller.cachedMonobehaviours = returnList;
                    return returnList;
                }
                return controller.cachedMonobehaviours;
            }
        }

    }
}