using App.Gameplay.LevelStorage;
using Modules.Atomic.Actions;
using Modules.Atomic.Values;

namespace App.Gameplay.Character.Scripts.Model.Mechanics
{
    public class UnloadResourcesMechanics
    {
        private readonly IAtomicValue<ResourceStorageModel> _storage;
        private readonly IAtomicValue<bool> _canUnloadResources;
        private readonly IAtomicValue<float> _unloadDelay;
        private readonly IAtomicValue<ResourceType> _resourceType;
        private readonly IAtomicVariable<int> _amount;
        private readonly AtomicEvent<ResourceType> _resourceUnloaded;

        private float _timer;
        
        public UnloadResourcesMechanics(
            AtomicEvent<ResourceType> resourceUnloaded,
            IAtomicValue<ResourceStorageModel> storage,
            IAtomicValue<bool> canUnloadResources,
            IAtomicValue<float> unloadDelay,
            IAtomicValue<ResourceType> resourceType,
            IAtomicVariable<int> amount)
        {
            _resourceUnloaded = resourceUnloaded;
            _storage = storage;
            _canUnloadResources = canUnloadResources;
            _unloadDelay = unloadDelay;
            _resourceType = resourceType;
            _amount = amount;
        }

        public UnloadResourcesMechanics(CharacterModel characterModel)
        {
            _storage = characterModel.ResourceStorage;
            _canUnloadResources = characterModel.CanUnloadResources;
            _unloadDelay = characterModel.Delay;
            _resourceType = characterModel.ResourceType;
            _amount = characterModel.ResourceAmount;
            _resourceUnloaded = characterModel.ResourceUnloaded;
        }

        public void Update(float deltaTime)
        {
            if (!_canUnloadResources.Value)
            {
                return;
            }
            
            _timer += deltaTime;
            
            if (_timer >= _unloadDelay.Value)
            {
                ResetTimer();

                if (_amount.Value == 0)
                {
                    return;
                }

                var unloadCount = 1;
                if (_storage.Value.ResourceStorage.TryAdd(_resourceType.Value, unloadCount))
                {
                    _amount.Value -= unloadCount;
                    _resourceUnloaded?.Invoke(_resourceType.Value);
                }
            }
        }

        private void ResetTimer()
        {
            _timer = 0f;
        }
    }
}