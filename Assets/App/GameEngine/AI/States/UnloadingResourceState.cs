using App.Gameplay.AI.Model;
using App.Gameplay.LevelStorage;
using Atomic;
using UnityEngine;

namespace App.Gameplay.AI.States
{
    public class UnloadingResourceState : StateMachine
    {
        private readonly AtomicVariable<BarnModel> _barnModel;
        private readonly AtomicVariable<bool> _canUnloadResources;
        private readonly AtomicVariable<float> _unloadingDistance;
 
        private readonly MoveToPositionData _moveData;
        private readonly UnloadResourceData _unloadResourceData;
        private readonly IAtomicAction _detectionBarnAction;
        private readonly IState _moveState;

        private float _timer;

        public UnloadingResourceState(
            AtomicVariable<BarnModel> barnModel,
            AtomicVariable<bool> canUnloadResources)
        {
            _barnModel = barnModel;
            _canUnloadResources = canUnloadResources;
        }

        public UnloadingResourceState(
            UnloadResourceData unloadResourceData,
            MoveToPositionData moveData,
            CharacterModel characterModel,
            IState moveState)
        {
            _moveData = moveData;
            _moveState = moveState;
            _unloadResourceData = unloadResourceData;
            _barnModel = characterModel.LevelStorage;
            _canUnloadResources = characterModel.CanUnloadResources;
            _unloadingDistance = characterModel.UnloadingDistance;
            _detectionBarnAction = characterModel.DetectionBarnAction;
        }

        public override void Enter()
        {
            base.Enter();
            _barnModel.Value = _unloadResourceData.BarnService.GetStorage();
            _moveData.StoppingDistance = _unloadingDistance.Value;
            
            DetectBarn();
        }

        private void DetectBarn()
        {
            _detectionBarnAction?.Invoke();
            
            if (_barnModel.Value != null)
            {
                _moveData.TargetPosition = _barnModel.Value.UnloadingPoint.position;
                _moveData.IsPositionReached = false;
            }
        }

        public override void Exit()
        {
            base.Exit();
            _canUnloadResources.Value = false;
            _moveData.IsPositionReached = false;
        }

        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);
            
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