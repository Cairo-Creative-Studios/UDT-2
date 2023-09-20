using System.Linq;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;
using Rich.StateMachines;
using Rich.Extensions;

namespace Rich.StateMachines.ForGameObjects
{
    public static class StateMachineForGameObjectsExtensions
    {
        public static void SetState<TState>(this GameObject gameObject) where TState : StateMachineManager.StateBase
        {
            StateMachineManager.SetStateForGameObject<TState>(gameObject);
        }

        public static List<StateMachineManager.StateBase> GetCurrentStates(this GameObject gameObject)
        {
            var stateMachines = gameObject.GetComponentsImplementing<IStateMachine>();
            List<StateMachineManager.StateBase> states = stateMachines.Select(stateMachine => stateMachine.CurrentState).ToList(); 
            return states;
        }
    }
}