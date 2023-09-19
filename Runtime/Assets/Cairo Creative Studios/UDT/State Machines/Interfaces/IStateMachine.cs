using System.Collections.Generic;
using Rich.DataTypes;
using UnityEngine;

namespace Rich.StateMachines
{
    public interface IStateMachine
    {
        public GameObject gameObject { get; }
        public Tree<StateMachineManager.Reference> stateReferences { get { return StateMachineManager.GetStateMachineReference(this).stateReferences; } set { StateMachineManager.GetStateMachineReference(this).stateReferences = value; } }
        public StateMachineManager.StateBase CurrentState { get{ return StateMachineManager.GetStateMachineReference(this).currentState; } set{ StateMachineManager.GetStateMachineReference(this).currentState = value; }}
    }
}