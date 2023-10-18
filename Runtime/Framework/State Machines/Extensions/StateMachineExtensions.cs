using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using UDT.Scriptables.Events;
using UDT.System;
using UnityEngine;

namespace UDT.StateMachines
{
    public static class StateMachineExtensions
    {
        public static void ActivateStateMachine(this IStateMachine stateEnabledMB)
        {
            StateMachineManager.ActivateStateMachine(stateEnabledMB);
        }

        public static void SetState<TState>(this IStateMachine stateMachine) where TState : StateMachineManager.StateBase
        {
            if (stateMachine != null)
            {
                if (stateMachine.stateReferences == null)
                {
                    ActivateStateMachine(stateMachine);
                }

                // If the State Machine object is a Runtime, call the corresponding Events
                if (stateMachine.GetType().IsAssignableFrom(typeof(IRuntime)))
                {
                    OnRuntimeStateExitted.Invoke(stateMachine.CurrentState, stateMachine);
                    OnRuntimeStateEntered.Invoke(nameof(TState), stateMachine);
                }

                StateMachineManager.SetState<TState>(stateMachine);
            }
            else
                Debug.LogError("This MonoBehaviour does not implement IFSM, it can not be used as a State Machine.", stateMachine.gameObject);
        }

        public static StateMachineManager.StateBase GetCurrentState(this IStateMachine stateMachine)
        {
            return StateMachineManager.GetStateMachineReference(stateMachine).currentState;
        }

        public static List<StateMachineManager.StateReference> GetStateReferences(this IStateMachine stateMachine)
        {
            return StateMachineManager.GetStateMachineReference(stateMachine).stateReferences.Flatten().Select(x => x.value).OfType<StateMachineManager.StateReference>().ToList();
        }
    }
}