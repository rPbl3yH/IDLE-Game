using Atomic.Elements;

namespace App.GameEngine.AI
{
    public class MoveToResourceState : IState
    {
        private readonly IState _moveToPositionState;
        private readonly IAtomicAction _resourceAction;
        
        public MoveToResourceState(IState moveToPositionState, IAtomicAction resourceDetection)
        {
            _moveToPositionState = moveToPositionState;
            _resourceAction = resourceDetection;
        }

        public void Enter()
        {
            _resourceAction?.Invoke();
            _moveToPositionState.Enter();
        }
        
        public void Update(float deltaTime)
        {
            _moveToPositionState.Update(deltaTime);
        }

        public void Exit()
        {
            _moveToPositionState.Exit();
        }
    }
}