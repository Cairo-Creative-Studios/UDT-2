using System;
using System.Collections.Generic;
using Rich.MultipurposeEvents;
using UnityEngine;
using System.Linq;
using Rich.System;
using Rich.DataTypes;

namespace Rich.StateMachines
{
    public class StateMachineManager : Singleton<StateMachineManager, SystemData>
    {
        private Dictionary<IStateMachine, StateMachineReference> stateMachineReferences = new();

        /// <summary>
        /// Activates the StateMachine for the class that implements IFSM.
        /// </summary>
        /// <param name="machine"></param>
        public static void ActivateStateMachine(IStateMachine machine)
        {
            var machineReference = new StateMachineReference(machine.GetType(), new());
            machineReference.machine = machine;
            var nestedStates = new Tree<Reference>(machineReference);

            nestedStates.rootNode.AddNodes(GetNestedStates(nestedStates.rootNode));

            machineReference.stateReferences = nestedStates;
            machineReference.currentState = ((StateReference)nestedStates[0]).state;
            singleton.stateMachineReferences.Add(machine, machineReference);

            machine.CurrentState.OnEnter();
            machine.CurrentState.OnEnterEvent?.Invoke();
        }

        private static Node<Reference>[] GetNestedStates(Node<Reference> currentNode)
        {
            var nestedTypes = currentNode.value.type.GetNestedTypes().Where(x => x.IsSubclassOf(typeof(StateBase))).ToArray();
            var nestedStates = new Node<Reference>[nestedTypes.Length];

            for (int i = 0; i < nestedTypes.Length; i++)
            {
                var createdReference = new StateReference(nestedTypes[i]);
                createdReference.state._context = currentNode.tree.rootNode.value.machine;
                createdReference.state.machine = currentNode.tree.rootNode.value.machine;
                nestedStates[i] = new Node<Reference>(currentNode.tree, createdReference, currentNode.index.Append(i).ToArray(), currentNode);
            }

            foreach (var state in nestedStates)
            {
                state.AddNodes(GetNestedStates(state));
            }

            return nestedStates;
        }

        /// <summary>
        /// Set the State of the MonoBehaviour to TState.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="machine"></param>
        public static void SetState<TState>(IStateMachine machine) where TState : StateBase
        {
            var stateReference = singleton.stateMachineReferences[machine].stateReferences.Flatten().Select(x => x.value).OfType<StateReference>().FirstOrDefault(x => x.state is TState);

            var previousState = singleton.stateMachineReferences[machine].currentState;
            singleton.stateMachineReferences[machine].currentState = stateReference.state;

            previousState.OnExit();
            previousState.OnExitEvent?.Invoke();
            
            singleton.stateMachineReferences[machine].currentState.OnEnter();
            singleton.stateMachineReferences[machine].currentState.OnEnterEvent?.Invoke();
        }

        public static void SetStateForGameObject<TState>(GameObject gameObject) where TState : StateBase
        {
            var machines = singleton.stateMachineReferences.Keys.Where(x => x.gameObject == gameObject);
            foreach(var machine in machines)
            {
                machine.SetState<TState>();
            }
        }

        public static List<StateBase> GetCurrentStatesForGameObject(GameObject gameObject)
        {
            List<StateBase> states = new();
            var machines = singleton.stateMachineReferences.Keys.Where(x => x.gameObject == gameObject);
            foreach(var machine in machines)
            {
                states.Add(machine.CurrentState);
            }
            return states;
        }

        void Update()
        {
            foreach(var machine in stateMachineReferences.Keys)
            {
                if(machine.gameObject.activeSelf)
                {
                    machine.CurrentState.OnUpdate();
                    machine.CurrentState.OnUpdateEvent?.Invoke();
                }
            }
        }

        public static StateMachineReference GetStateMachineReference(IStateMachine machine)
        {
            if(!singleton.stateMachineReferences.ContainsKey(machine))
                ActivateStateMachine(machine);
            return singleton.stateMachineReferences[machine];
        }

        
        public class Reference
        {
            public IStateMachine machine; 
            public Type type;
        }

        public class StateMachineReference : Reference
        {
            public StateBase currentState;
            public Tree<Reference> stateReferences;

            public StateMachineReference(Type type, Tree<Reference> stateReferences)
            {
                currentState = null;
                this.stateReferences = stateReferences;
                this.type = type;
            }
        }

        [Serializable]
        public class StateBase
        {
            public MultipurposeEvent OnEnterEvent = new();
            public MultipurposeEvent OnUpdateEvent = new();
            public MultipurposeEvent OnExitEvent = new();
            public object _context;
            public IStateMachine machine;
            public GameObject gameObject { get { return machine.gameObject; } }

            

            public virtual void OnEnter() { }
            public virtual void OnUpdate() { }
            public virtual void OnExit() { }
        }


        [Serializable]
        public class StateReference : Reference
        {
            public string stateName;
            public StateBase state;

            public StateReference(Type type)
            {
                stateName = type.Name;
                state = (StateBase)Activator.CreateInstance(type);
                
                this.type = type;
            }
        }
    }
    [Serializable]
    public class State<T> : StateMachineManager.StateBase
    {
        public T Context { get{return (T)_context;} set{_context = value;}}
        public void SetState<TState>() where TState : StateMachineManager.StateBase
        {
            machine.SetState<TState>();
        }
    }
}