using System.Linq;
using App.Gameplay.LevelStorage;
using Atomic;

namespace App.Gameplay
{
    public class UnloadingResourcesMechanics
    {
        private readonly AtomicVariable<LevelStorageModel> _levelStorageModel;
        private readonly AtomicVariable<bool> _canUnloadResources;
        private readonly AtomicVariable<float> _delay;
        private readonly AtomicVariable<ResourceType> _resourceType;
        private readonly AtomicVariable<int> _amount;

        private float _timer;
        
        public UnloadingResourcesMechanics(
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

        public void Update(float deltaTime)
        {
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