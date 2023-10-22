using App.Gameplay.AI.Model;
using App.Gameplay.LevelStorage;
using Atomic;

namespace App.Gameplay.AI.States
{
    public class UnloadingResourceState : StateMachine
    {
        private readonly AtomicVariable<LevelStorageModel> _levelStorageModel;
        private readonly AtomicVariable<bool> _canUnloadResources;
        private readonly AtomicVariable<float> _delay;
        private readonly AtomicVariable<ResourceType> _resourceType;
        private readonly AtomicVariable<int> _amount;

        private readonly MoveToPositionData _moveData;
        private readonly UnloadResourceData _unloadResourceData;
        private readonly IState _moveState;

        private float _timer;

        public UnloadingResourceState(
            AtomicVariable<LevelStorageModel> levelStorageModel,
            AtomicVariable<bool> canUnloadResources,
            AtomicVariable<float> delay,
            AtomicVariable<ResourceType> resourceType,
            AtomicVariable<int> amount)
        {
            _levelStorageModel = levelStorageModel;
            _canUnloadResources = canUnloadResources;
            _delay = delay;
            _resourceType = resourceType;
            _amount = amount;
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
            _levelStorageModel = characterModel.LevelStorage;
            _canUnloadResources = characterModel.CanUnloadResources;
            _delay = characterModel.Delay;
            _resourceType = characterModel.ResourceType;
            _amount = characterModel.Amount;
        }

        public override void Enter()
        {
            base.Enter();
            _levelStorageModel.Value = _unloadResourceData.LevelStorageService.GetStorage();
            _moveData.TargetPosition = _levelStorageModel.Value.transform.position;
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
            
            if (!_canUnloadResources.Value)
            {
                return;
            }
            
            _timer += deltaTime;
            
            if (_timer >= _delay.Value)
            {
                ResetTimer();

                if (_amount.Value == 0)
                {
                    return;
                }

                var unloadCount = 1;
                var resourceData = new ResourceData(_resourceType.Value, unloadCount);
                _levelStorageModel.Value.ResourceAdded?.Invoke(resourceData);
                _amount.Value -= unloadCount;
            }
        }

        private void ResetTimer()
        {
            _timer = 0f;
        }
    }
}