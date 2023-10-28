using System;

namespace Modules.Atomic.States.FSM
{
    public interface IStateMachine<TKey> : IState
    {
        event Action<TKey> OnStateSwitched;

        TKey CurrentState { get; }

        void SwitchState(TKey key);
    }
}