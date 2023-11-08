using App.Gameplay.Character.Scripts.Model;
using App.Gameplay.LevelStorage;
using Modules.Atomic.Values;

namespace App.GameEngine.AI
{
    public class UnloadingResourceState : StateMachine
    {
        private readonly IAtomicVariable<ResourceStorageModel> _resourceStorageModel;
        private readonly IAtomicVariable<bool> _canUnloadResources;
        private readonly IAtomicVariable<float> _unloadingDistance;
 
        private readonly MoveToPositionData _moveData;
        private readonly UnloadResourceData _unloadResourceData;
        private readonly IState _moveState;

        private float _timer;

        public UnloadingResourceState(
            UnloadResourceData unloadResourceData,
            MoveToPositionData moveData,
            CharacterModel characterModel,
            IState moveState)
        {
            _moveData = moveData;
            _moveState = moveState;
            _unloadResourceData = unloadResourceData;
            _resourceStorageModel = characterModel.ResourceStorage;
            _canUnloadResources = characterModel.CanUnloadResources;
            _unloadingDistance = characterModel.UnloadingDistance;
        }

        public override void Enter()
        {
            base.Enter();
            _moveData.StoppingDistance = _unloadingDistance.Value;
            _unloadResourceData.IsEnable = true;
            
            DetectTargetPosition();
        }

        private void DetectTargetPosition()
        {
            if (_resourceStorageModel.Value == null)
            {
                _unloadResourceData.IsEnable = false;
                return;
            }
            
            if (_moveData.TargetPosition != _resourceStorageModel.Value.UnloadingPoint.position)
            {
                _moveData.TargetPosition = _resourceStorageModel.Value.UnloadingPoint.position;
                _moveData.IsPositionReached = false;
            }
        }

        public override void Exit()
        {
            base.Exit();
            _canUnloadResources.Value = false;
            _moveData.IsPositionReached = false;
            _unloadResourceData.IsEnable = false;
        }

        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);

            if (!_unloadResourceData.IsEnable)
            {
                return;
            }
            
            DetectTargetPosition();
            
            if (!_moveData.IsPositionReached)
            {
                _canUnloadResources.Value = false;
                SwitchState(_moveState);
                return;
            }
            
            _canUnloadResources.Value = true;
        }
    }
}