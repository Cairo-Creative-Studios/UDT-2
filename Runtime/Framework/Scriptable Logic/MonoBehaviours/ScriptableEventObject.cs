using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using UDT.Instances;
using UDT.StateMachines;
using System;
using System.Linq;

namespace UDT.Scriptables
{
    public class ScriptableEventObject : MonoBehaviour
    {
        private Instance instance;
        public bool runtime = false;

        [Expandable]
        public IStateMachine machine;
        public EventState stateHierarchy = new();
        private EventState currentState;
        public EventState CurrentState { get => currentState; }

        void Awake()
        {
            instance = Instance.GetAsInstance(gameObject);

            foreach (var stateName in stateHierarchy.Keys)
            {
                foreach (EventState nestedState in stateHierarchy[stateName])
                {
                    foreach (EventGraph graph in nestedState)
                    {
                        var createdGraph = ScriptableObject.Instantiate(graph);
                        createdGraph.This = instance;
                    }
                }
            }
        }

        private void OnValidate()
        {
            foreach (var stateName in stateHierarchy.Keys)
            {
                if (currentState == null)
                {
                    currentState = stateHierarchy[stateName];
                }

                foreach (EventState nestedState in stateHierarchy[stateName])
                {
                    nestedState.name = stateName;

                    foreach (var reference in nestedState.scripts)
                    {
                        reference.gameObject = gameObject;
                        reference.controller = this;
                    }
                }
            }
        }

        private void Update()
        {
            foreach(var graphReference in currentState.scripts)
            {
                graphReference.graph.UpdateScript();
            }
        }

        public void SetStateHierarchyPath(string statePath)
        {
            var states = statePath.Split('/');
            EventState currentState = stateHierarchy;

            foreach (var state in states)
            {
                if (!currentState.ContainsKey(state))
                {
                    Debug.LogError("The requested State does not exist: " + state, this);
                }

                currentState = currentState[state];
            }

            this.currentState = currentState;
        }

        public void AddState(string stateName)
        {
            stateHierarchy.Add(stateName, new());
        }

        public void AddScript(EventGraph eventGraph)
        {
            if (stateHierarchy.Keys.Count() < 1)
                AddState("Default");
            if (currentState == null)
                currentState = stateHierarchy[stateHierarchy.Keys.ToArray()[0]];

            currentState.AddScript(eventGraph);
        }

        [Serializable]
        public class EventState : SerializableDictionary<string, EventState>
        {
            [ReadOnly] public string name;
            public List<EventGraphReference> scripts = new();
        
            public void AddScript(EventGraph graph)
            {
                scripts.Add(new EventGraphReference(graph));
            }
        }

        [Serializable]
        public class EventGraphReference
        {
            [Dropdown("GetMonobehaviours")]
            public EventGraph graph;

            [HideInInspector]
            public GameObject gameObject;
            [HideInInspector]
            public ScriptableEventObject controller;

            public DropdownList<EventGraph> GetEventGraphs()
            {
                return ScriptableManager.EventGraphDropdownList;
            }

            public EventGraphReference(EventGraph graph)
            {
                this.graph = graph;
            }
        }
    }
}