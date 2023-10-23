using System;
using UnityEngine;

namespace App.Gameplay.AI.States
{
    [Serializable]
    public class StateMachine : IState
    {
        protected IState CurrentState;
        
        public virtual void Enter()
        {
            CurrentState?.Enter();
        }

        public virtual void Update(float deltaTime)
        {
            CurrentState?.Update(deltaTime);
        }

        public virtual void Exit()
        {
            CurrentState?.Exit();
        }

        public void SwitchState(IState state)
        {
            if (CurrentState == state)
            {
                return;
            }
            
            //Debug.Log("Switch state to " + state);
            CurrentState?.Exit();
            CurrentState = state;
            CurrentState?.Enter();
        }
    }
}